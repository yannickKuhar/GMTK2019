using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootprintController : MonoBehaviour
{
    public CapsuleCollider[] Colliders;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var footprint in Footprint.Footprints)
        {
            bool isVisible = true;
            foreach (CapsuleCollider collider in Colliders)
            {
                if ((footprint.transform.position - collider.transform.position).magnitude < collider.radius)
                {
                    isVisible = false;
                    break;
                }
            } 
            footprint.SetVisible(isVisible);
        }
    }
}
