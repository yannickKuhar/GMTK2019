using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button3D : MonoBehaviour
{	
	public SceneSwitch sm;

	void OnMouseDown()
	{
		sm.SelectTorch();
		sm.LoadLevel();
	}
}
