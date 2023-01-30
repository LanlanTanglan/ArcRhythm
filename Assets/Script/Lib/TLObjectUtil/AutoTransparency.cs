using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTransparency : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer;
    public bool isVisible = false;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //TODO 可优化
    void OnBecameVisible()
    {
        Debug.Log("进入相机视野~~~");
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1f);
    }

    void OnBecameInvisible()
    {
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0f);
    }
}
