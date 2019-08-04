using UnityEngine;

public class Fader : MonoBehaviour
{
    public bool Visible = true;
    public float Opacity = 1f;
    public float FadeSpeed = 0.2f;

    public Renderer Renderer;

    // Update is called once per frame
    void Update()
    {
        Renderer.enabled = Visible;
        
        if (Opacity > 0f)
        {
            Opacity = Mathf.Clamp01(Opacity - FadeSpeed * Time.deltaTime);
            Color color = Renderer.materials[0].GetColor("_BaseColor");
            color.a = Opacity;
            Renderer.materials[0].SetColor("_BaseColor", color);
            if (Opacity < 0.001f)
            {
                enabled = false;
                Destroy(gameObject, 1f);
            }
        }
    }
}
