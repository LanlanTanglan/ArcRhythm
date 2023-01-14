using UnityEngine;
using System.Collections;
using ArkRhythm;

public delegate void AudioCallBack();

class AudioManager : Singleton<AudioManager>
{
    private AudioSource _audioSource;
    ClipData clipdata = new ClipData();
    protected override void OnAwake()
    {
        Singleton<GameProcessManager>.Instance.StopGameEvent += StopGame;
        _audioSource = this.gameObject.AddComponent<AudioSource>();
    }

    private void StopGame(bool key)
    {
        if(key)
        {
            _audioSource.Pause();
        }else
        {
            _audioSource.Play();
        }
    }
    

    /// <summary>
    /// 播放音频 Resources/Audios/name
    /// </summary>
    /// <param name="name"></param>
    public void AudioPlay(string name)
    {
        _audioSource.clip = clipdata.audiodata(name);
        _audioSource.Play();
    }
    /// <summary>
    /// 暂停播放
    /// </summary>
    public void AudioPause()
    {
        _audioSource.Pause();
    }
    /// <summary>
    /// 暂停播放后继续播放
    /// </summary>
    public void AudioUnPause()
    {
        _audioSource.UnPause();
    }
    /// <summary>
    /// 停止播放
    /// </summary>
    public void AudioStop()
    {
        _audioSource.Stop();
    }
    /// <summary>
    /// 切换音频 Resources/Audios/name
    /// </summary>
    /// <param name="name"></param>
    public void AudioSwitch(string name)
    {
        AudioClip _clip = clipdata.audiodata(name);
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
        _audioSource.clip = _clip;
        _audioSource.Play();
    }
    /// <summary>
    /// 音频回调播放器  Resources/Audios/name callback=>音频播完后执行的方法。
    /// </summary>
    /// <param name="name"></param>
    /// <param name="callback"></param>
    public void AudioPlayer(string name, AudioCallBack callback)
    {
        _audioSource.clip = clipdata.audiodata(name);
        _audioSource.Play();
        StartCoroutine(AudioDelayedCallBack(_audioSource.clip.length, callback));
    }


    /// <summary>
    /// 音频回调播放器  Resources/Audios/name callback=>音频播完后执行的方法。
    /// </summary>
    /// <param name="name"></param>
    /// <param name="callback"></param>
    /// <param name="time"></param>
    public void AudioPlayer(string name, AudioCallBack callback, float time)
    {
        _audioSource.clip = clipdata.audiodata(name);
        _audioSource.Play();
        StartCoroutine(AudioDelayedCallBack(_audioSource.clip.length + time, callback));
    }

    //音频延迟回调
    IEnumerator AudioDelayedCallBack(float time, AudioCallBack callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }
    /// <summary>
    /// 延时播放音频 Resources/Audios/name time=>延时时间
    /// </summary>
    /// <param name="name"></param>
    /// <param name="time"></param>
    public void AudioDelayPlay(string name, float time)
    {
        _audioSource.clip = clipdata.audiodata(name);
        Invoke("AudioDelayTime", time);
    }
    private void AudioDelayTime() { _audioSource.Play(); }

    /// <summary>
    /// 生成2D音效 Resources/Audios/name 播放完毕消失
    /// </summary>
    /// <param name="name"></param>
    public void AudioInstantiate(string name)
    {
        GameObject obj = new GameObject();
        AudioSource _audio = obj.AddComponent<AudioSource>();
        _audio.name = "AudioSource";
        _audio.playOnAwake = true;
        _audio.clip = clipdata.audiodata(name);
        _audio.Play();
        StartCoroutine(AudioFinish(_audio.clip.length, obj));
    }
    //音效结束销毁AudioGameObject
    IEnumerator AudioFinish(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);
        DestroyImmediate(obj);
    }
    /// <summary>
    /// 3D音效 (只播放一次) Resources/Audios/name CameraPos:摄像机位置
    /// </summary>
    /// <param name="name"></param>
    /// <param name="CameraPos"></param>
    public void AudioAtPoint(string name, Vector3 CameraPos)
    {
        AudioSource.PlayClipAtPoint(clipdata.audiodata(name), CameraPos, 1.0f);
    }
    /// <summary>
    /// 指定AudioSource播放音频
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="name"></param>
    public void AudioSourceOther(GameObject obj, string name)
    {
        AudioSource audioSource = obj.GetComponent<AudioSource>();
        audioSource.clip = clipdata.audiodata(name);
        audioSource.Play();
    }

    public void CloseAudio()
    {
        StopAllCoroutines();
        _audioSource.clip = null;
    }

    public void Init()
    {

    }
}

/// <summary>
/// 配合AudioManagers使用  
/// </summary>
public class ClipData
{
    public AudioClip audiodata(string name)
    {
        // Debug.Log(name);
        return Resources.Load<AudioClip>("Audio/" + name);
    }

}