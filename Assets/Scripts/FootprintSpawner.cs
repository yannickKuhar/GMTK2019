using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetStateMachine))]
public class FootprintSpawner : MonoBehaviour
{
    public GameObject FootprintPrefab;
    public float SpawnDelay = 1f;

    private TargetStateMachine sm;
    private TargetState lastState;
    private float lastSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        sm = GetComponent<TargetStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(sm.State)
        {
            case TargetState.MOVEMENT:
                var timeNow = Time.time;
                if (lastState != TargetState.MOVEMENT)
                {
                    lastSpawnTime = timeNow;
                }
                else if (timeNow > lastSpawnTime + SpawnDelay)
                {
                    lastSpawnTime = timeNow;
                    var footprint = Instantiate(FootprintPrefab, transform.position, transform.rotation);
                }
                break;

            default:
                break;
        }

        lastState = sm.State;
    }
}
