using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ArkRhythm;

//顶层管理器
[RequireComponent(typeof(AudioSource))]
public class CreateBMSTopController : MonoBehaviour
{
    public Transform scaleLineWarp;
    public CreateLineController createLineController;
    public Transform operatorGroupWarp;
    public Transform musicWarp;

    void Awake()
    {
        createLineController = scaleLineWarp.GetComponent<CreateLineController>();

        operatorGroupController = operatorGroupWarp.GetComponent<OperatorGroupController>();

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        InitMusicInfo();

        InitScaleLine();

        UpdateNoteWarpHeght();
        UpdateOperatorGroupWarpMin();
    }

    void Update()
    {


        //当前音乐正在播放的话的播放时间
        if (audioSource.isPlaying)
        {
            //修改音乐的时间
            currentPlayIdx = audioSource.time;
            //更新OperatorWarp的位置
            operatorGroupWarp.transform.localPosition = new Vector3(operatorGroupWarp.transform.localPosition.x, -speed * currentZoom * currentPlayIdx);
            ChangeMusicPosSliderValue(currentPlayIdx);
        }
        else
        {
            //修改音乐的时间
            float moveYLen = Math.Abs(operatorGroupController.transform.localPosition.y);
            currentPlayIdx = moveYLen / (speed * currentZoom);
            // audioSource.time = currentPlayIdx;
            ChangeMusicCurrentPlayTime(currentPlayIdx);
        }
        //当音乐进度条没有拖动
        if (!musicPosSlider.interactable)
        {
            musicPosSlider.value = currentPlayIdx;
        }


        //全局位移相关
        UpdateWarpYPos();
    }


    //更新所有相关物体的Y轴位置
    void UpdateWarpYPos()
    {
        scaleLineWarp.position = new Vector3(scaleLineWarp.position.x, operatorGroupWarp.position.y);
    }

    #region 缩放尺功能
    //初始化缩放尺
    void InitScaleLine()
    {
        currentBPM = createLineController.BPM;
        currentSpaceCount = createLineController.spaceCount;
        createLineController.musicTime = musicLen;
        createLineController.speed = speed;
    }
    #endregion

    #region setting功能区
    //缩放功能
    public Slider zoomSlider;
    public float currentZoom = 1;
    //BPM修改
    public TMP_InputField BPMInputField;
    public TMP_InputField SpaceCountInputField;
    public float currentBPM;
    public int currentSpaceCount;
    //间隔修改
    public void OnZommChange()
    {

        //更新scaleLine的缩放
        createLineController.UpdateZoom(zoomSlider.value);
        currentZoom = zoomSlider.value;
        UpdateNoteWarpHeght();
        UpdateOperatorGroupWarpMin();

        //修改音乐的时间
        float moveYLen = Math.Abs(operatorGroupController.transform.localPosition.y);
        currentPlayIdx = moveYLen / (speed * currentZoom);
        // audioSource.time = currentPlayIdx;
        ChangeMusicCurrentPlayTime(currentPlayIdx);
    }
    //BPM修改
    public void UpdateBPM()
    {
        float t;
        if (float.TryParse(BPMInputField.text, out t))
        {
            Debug.LogWarning(BPMInputField.text);
            if (currentBPM != t)
            {
                createLineController.UpdateBPM(t);
                createLineController.UpdateSpaceCount(currentSpaceCount);
                currentBPM = t;
            }
        }
    }
    //间隔修改
    public void UpdateSpaceCount()
    {
        int t;
        if (int.TryParse(SpaceCountInputField.text, out t))
        {
            if (currentSpaceCount != t)
            {
                createLineController.UpdateSpaceCount(t);
                currentSpaceCount = t;
            }
        }
    }
    #endregion

    #region BMS信息区
    public float musicLen = 300;
    public float speed = 100;//版面流速
    public BMS bms;//当前铺面

    //TODO 初始化铺面数据
    public void InitBMS()
    {
        
    }
    //TODO 设置干员信息
    public void SetOperator(int idx)
    {
        
    }
    #endregion

    #region 干员轨道管理
    public GameObject operatorWarpItem;
    public List<Transform> operatorWarpRectList;
    public OperatorGroupController operatorGroupController;
    void UpdateOperatorGroupWarpMin()
    {
        //更新移动框的可移动距离
        RectTransform r = operatorGroupWarp.GetComponent<RectTransform>();
        // Debug.LogWarning(r.rect.width);
        operatorGroupController.minX = r.rect.width - 740.48f;
        operatorGroupController.minY = -speed * musicLen * currentZoom;
    }

    void UpdateNoteWarpHeght()
    {
        for (int i = 0; i < operatorWarpRectList.Count; i++)
        {
            RectTransform r = operatorWarpRectList[i].GetChild(0).GetComponent<RectTransform>();
            // r.anchoredPosition = r.rect.position;
            r.sizeDelta = new Vector2(r.sizeDelta.x, speed * musicLen * currentZoom);
        }
    }

    //当干员列表拖动时
    public void WhenOperaorWarpDrag()
    {
        //暂停音乐
        PauseMusic();
        //修改音乐的播放位置
        //修改音乐的时间
        float moveYLen = Math.Abs(operatorGroupController.transform.localPosition.y);
        currentPlayIdx = moveYLen / (speed * currentZoom);
        ChangeMusicCurrentPlayTime(currentPlayIdx);
        // musicPosSlider.value = currentPlayIdx;
        ChangeMusicPosSliderValue(currentPlayIdx);
    }
    #endregion

    #region 音乐控制
    public AudioClip musicClip;
    public float currentPlayIdx = 0;//当前播放位置,音乐提前五秒开始
    public AudioSource audioSource;
    public Slider musicPosSlider;
    public bool isMusicPosSliderDragging = false;


    //修改音乐长度
    void InitMusicInfo()
    {
        musicClip = audioSource.clip;
        //给音乐添加5秒的静默长度
        // 获取原始音频数据
        float[] data = new float[musicClip.samples * musicClip.channels];
        musicClip.GetData(data, 0);
        // 将5秒的静默数据添加到音频剪辑的开头
        int sampleRate = musicClip.frequency;
        int numChannels = musicClip.channels;
        int numSilenceSamples = sampleRate * numChannels * 5; // 5秒的样本数
        float[] silenceData = new float[numSilenceSamples];
        for (int i = 0; i < numSilenceSamples; i++)
        {
            silenceData[i] = 0f;
        }
        float[] newData = new float[data.Length + numSilenceSamples];
        silenceData.CopyTo(newData, 0);
        data.CopyTo(newData, numSilenceSamples);
        // 创建新的音频剪辑并设置数据
        AudioClip newClip = AudioClip.Create(musicClip.name, newData.Length / numChannels, numChannels, sampleRate, false);
        newClip.SetData(newData, 0);
        musicClip = newClip;
        audioSource.clip = musicClip;
        //设置音乐的长度
        musicLen = musicClip.length;

        //设置滑轮控制初始值，以及范围
        musicPosSlider.minValue = 0;
        musicPosSlider.maxValue = musicClip.length;


    }
    //播放音乐
    public void PlayMusic()
    {
        audioSource.Play();
    }
    //暂停音乐
    public void PauseMusic()
    {
        audioSource.Pause();
    }
    //根据Silder变化修改音乐播放
    public void UpdateMusicPlayBySlider()
    {
        // audioSource.time = musicPosSlider.value;
        ChangeMusicCurrentPlayTime(musicPosSlider.value);
        currentPlayIdx = musicPosSlider.value;
        //更新OperatorWarp的位置
        operatorGroupWarp.transform.localPosition = new Vector3(operatorGroupWarp.transform.localPosition.x, -speed * currentZoom * currentPlayIdx);
    }

    //修改当前音乐播放时间
    public void ChangeMusicCurrentPlayTime(float a)
    {
        if (audioSource.clip == null)
        {
            return;
        }

        if (a < 0f)
        {
            return;
        }

        if (a >= audioSource.clip.length)
        {
            a = audioSource.clip.length - 0.0001f;
            // Debug.Log(audioSource.clip.length);
            audioSource.time = a;
            audioSource.Pause();
            return;
        }
        audioSource.time = a;
    }

    public void ChangeMusicPosSliderValue(float a)
    {
        if (a > musicPosSlider.maxValue)
        {
            musicPosSlider.value = musicPosSlider.maxValue;
            audioSource.Pause();
        }
        else
        {
            musicPosSlider.value = a;
        }
    }
    #endregion
}
