using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    public static List<Footprint> Footprints = new List<Footprint>();

    private Fader fader;

    // Start is called before the first frame update
    void Start()
    {
        fader = GetComponent<Fader>();
        Footprints.Add(this);
    }

    // Update is called once per frame
    void OnDestroy() {
        Footprints.Remove(this);
    }

    public void SetVisible(bool isVisible)
    {
        fader.Visible = isVisible;
    }
}
