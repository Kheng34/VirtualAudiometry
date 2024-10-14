using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HighlightWhenHover : MonoBehaviour
{
    public Color OriginalColor; 
    private Renderer Renderer;
    void Start()
    {
        Renderer = GetComponent<Renderer>();
        OriginalColor = Renderer.material.color;
    }
    public void OnHoverEntered(HoverEnterEventArgs args)
    {
        Renderer.material.color = Color.red;
    }
    public void OnHoverExited(HoverExitEventArgs args)
    {
        Renderer.material.color = OriginalColor;
    }
}
