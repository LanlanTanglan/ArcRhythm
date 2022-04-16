using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Panel基类
/// </summary>
public class BasePanel : MonoBehaviour
{
    //是否被加载
    public bool isLoaded = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// UI进入时
    /// </summary>
    public virtual void OnEnter()
    {
        isLoaded = true;
    }

    /// <summary>
    /// UI处于最上方时
    /// </summary>
    public virtual void OnUpdate()
    {

    }

    /// <summary>
    /// UI处于离开时
    /// </summary>
    public virtual void OnExit()
    {

    }
}
