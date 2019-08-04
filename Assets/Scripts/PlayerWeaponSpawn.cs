using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSpawn : MonoBehaviour
{
    public Torch torch;
    public Sling sling;

    void Awake()
    {
	    if (WeaponType.weaponType != null && WeaponType.weaponType.Equals("torch"))
	    {
			var t = Instantiate(torch, new Vector3(0, 0, 0), Quaternion.identity);
			t.transform.position = gameObject.transform.position;
            t.transform.rotation = gameObject.transform.rotation;
            t.transform.parent = gameObject.transform.parent;

		    Debug.Log("Spawning torch.");
			Destroy(gameObject);
		}
		else if (WeaponType.weaponType != null && WeaponType.weaponType.Equals("sling"))
		{
			var s = Instantiate(sling, new Vector3(0, 0, 0), Quaternion.identity);;
			s.transform.position = gameObject.transform.position;
            s.transform.rotation = gameObject.transform.rotation;
            s.transform.parent = gameObject.transform.parent;

		    Debug.Log("Spawning sling.");
			Destroy(gameObject);
		}
		else
		{
		   Debug.Log("Error spawning weapon.");
		}
	}   
}
