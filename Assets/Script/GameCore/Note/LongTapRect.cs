using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcRhythm;

public class LongTapRect : MonoBehaviour
{
    public float d;
    public float s;

    public SpriteRenderer spriteRenderer;
    #region 事件注册块
    public bool isStopGame = false;
    //暂停游戏
    private void StopGame(bool key)
    {
        this.isStopGame = key;
    }

    #endregion
    void Awake()
    {
        Singleton<GameProcessManager>.Instance.StopGameEvent += StopGame;
    }
    void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isStopGame)
        {
            d -= Time.deltaTime;
            spriteRenderer.size = new Vector2(1.28f, s * d);
            if (spriteRenderer.size.y <= 0)
            {
                Destroy(this.gameObject);
            }
            //根据速度缩小其长度
        }
    }

    public void Init(float d, float s)
    {
        this.d = d;
        this.s = s;
    }
}
