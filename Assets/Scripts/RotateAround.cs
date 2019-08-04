using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
	public float deltaY;
    
	void Update()
    {
		transform.Rotate(0, deltaY, 0, Space.Self);
    }
}
