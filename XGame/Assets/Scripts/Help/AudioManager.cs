using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    private AudioSource _bgmSource;
    private float _bgmSourceVolume;

    private new void Awake()
    {
        base.Awake();
        _bgmSource = GetComponent<AudioSource>();
    }

    public void SetBGM(string bgmname)
    {
        string audioStr = "Audio/BGM" + bgmname;
        _bgmSource.clip = Resources.Load<AudioClip>(audioStr);
    }

    public void PlayEffectAudio(string effectname)
    {
        GameObject effectaudio = GameObjectPool.Instance.GetFromPool("EffectAudio");
        effectaudio.GetComponent<EffectAudio>().SetEffectAudioClip(effectname);
    }
}
