using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TL_OutlineEffect : MonoBehaviour
{
    private Material material;
    private Color originalColor;
    private Color outlineColor = Color.white;
    private float outlineWidth = 0.005f;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
        originalColor = material.color;
    }

    private void OnMouseEnter()
    {
        material.SetColor("_OutlineColor", outlineColor);
        material.SetFloat("_OutlineWidth", outlineWidth);
    }

    private void OnMouseExit()
    {
        material.SetColor("_OutlineColor", originalColor);
        material.SetFloat("_OutlineWidth", 0);
    }
}
