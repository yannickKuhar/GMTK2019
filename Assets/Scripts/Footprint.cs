﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprint : MonoBehaviour
{
	public int activeTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, activeTime);
    }
}
