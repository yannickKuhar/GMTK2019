using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Path {
    public GameObject[] Nodes;
}

[RequireComponent(typeof(TargetStateMachine))]
public class TargetController : MonoBehaviour
{
    public float MoveSpeed = 3f;
    public float RotateSpeed = 8f;
    public float ShortWaitDelay = 2f;

    public float FlightHeight = 4f;
    public float AscendSpeed = 2f;
    
    public GameObject HeightControl;

    public GameObject[] Path1;
    public GameObject[] Path2;
    public GameObject[] Path3;
    public GameObject[] Path4;
    public GameObject[] Path5;
    public GameObject[] Path6;
    public GameObject[] Path7;

    private List<Path> Paths = new List<Path>();

    private TargetStateMachine sm;
    private Coroutine activeCoroutine;
    private Path currentPath;
    private int nodeIndex;
    private GameObject nextNode;

    private float desiredHeight;

    // Start is called before the first frame update
    void Start()
    {
        sm = GetComponent<TargetStateMachine>();
        sm.State = TargetState.START;

        desiredHeight = 0f;

        foreach (var nodes in new[] { Path1, Path2, Path3, Path4, Path5, Path6, Path7 })
        {
            if (nodes != null && nodes.Length > 0)
            {
                var reverseNodes = new GameObject[nodes.Length];
                for (int i = 0; i < nodes.Length; i++)
                {
                    reverseNodes[i] = nodes[nodes.Length - 1 - i];
                }
                Paths.Add(new Path { Nodes = nodes });
                Paths.Add(new Path { Nodes = reverseNodes });
            }
        }

        Debug.Log("Paths: " + Paths.Count);

        nodeIndex = 0;
        currentPath = Paths[Random.Range(0, Paths.Count - 1)];
        nextNode = currentPath.Nodes[nodeIndex];

        transform.position = nextNode.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch(sm.State)
        {
            case TargetState.START:
                sm.State = TargetState.WAIT_SHORT;
                break;
            case TargetState.IDLE:
                // do nothing
                break;
            case TargetState.WAIT_SHORT:
                sm.State = TargetState.IDLE;
                activeCoroutine = StartCoroutine("WaitShort");
                break;
            case TargetState.CHOOSE_DESTINATION:
                sm.State = TargetState.MOVEMENT_GROUND;
                ChooseNextNode();
                break;
            case TargetState.MOVEMENT_GROUND:
            case TargetState.MOVEMENT_AIR:
                Vector3 destination = nextNode.transform.position;                
                if (IsPositionAt(destination))
                {
                    transform.position = destination;
                    if (IsAtEndEndOfPath())
                    {
                        sm.State = TargetState.WAIT_SHORT;
                    }
                    else
                    {
                        ChooseNextNode();
                    }
                }
                else
                {
                    MoveTowards(destination);
                }
                break;
            default:
                Debug.LogError("Invalid TargetState");
                break;
        }
    }

    private bool IsPositionAt(Vector3 destination)
    {
        return Vector3.Distance(transform.position, destination) < MoveSpeed * 0.01f;
    }

    private bool IsAtEndEndOfPath()
    {
        return currentPath != null && nodeIndex == currentPath.Nodes.Length - 1;
    }

    private void MoveTowards(Vector3 destination)
    {    
        Vector3 position = transform.position;
        Vector3 moveDirection = Vector3.Normalize(destination - position);
        position = position + moveDirection * MoveSpeed * Time.deltaTime;
        transform.position = position;

        Quaternion targetRotation = transform.rotation;
        targetRotation.SetLookRotation(moveDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);

        Vector3 heightPosition = HeightControl.transform.position;
        heightPosition.y = Mathf.Lerp(heightPosition.y, desiredHeight, AscendSpeed * Time.deltaTime);
        HeightControl.transform.position = heightPosition;
    }

    private void ChooseNextNode()
    {
        if (IsAtEndEndOfPath())
        {
            nodeIndex = 0;
            var pathCandidates = Paths.FindAll(path => path.Nodes[0] == nextNode);
            currentPath = pathCandidates[Random.Range(0, pathCandidates.Count - 1)];
        }
        else 
        {
            if (nextNode.name.StartsWith("S_"))
            {
                if (desiredHeight > 0f)
                {
                    desiredHeight = 0f;
                    sm.State = TargetState.MOVEMENT_GROUND;
                }
                else
                {
                    desiredHeight = FlightHeight;
                    sm.State = TargetState.MOVEMENT_AIR;
                }
            }
            nodeIndex++;
        }

        if (currentPath != null)
        {
            nextNode = currentPath.Nodes[nodeIndex];
            // Debug.Log("Choose the next node named " + nextNode.name);
        }
    }

    private IEnumerator WaitShort()
    {
        yield return new WaitForSeconds(ShortWaitDelay);
        sm.State = TargetState.CHOOSE_DESTINATION;
    }
}
