using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IArkPage
{
    /// <summary>
    /// 上一个页面
    /// </summary>
    /// <value></value>
    public IArkPage _front { get; set; }
    
    /// <summary>
    /// 下一个页面
    /// </summary>
    /// <value></value>
    public IArkPage _next { get; set; }

    /// <summary>
    /// 进入
    /// </summary>
    public void Enter();
    /// <summary>
    /// 刷新中
    /// </summary>
    public void Updata();
    /// <summary>
    /// 退出
    /// </summary>
    public void Exit();
}
