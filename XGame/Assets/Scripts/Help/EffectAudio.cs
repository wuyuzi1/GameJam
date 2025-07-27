using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAudio : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetEffectAudioClip(string effectname)
    {
        _audioSource.clip = null;
        string audioStr = "Audio/Effect/" + effectname;
        _audioSource.clip = Resources.Load<AudioClip>(audioStr);
        _audioSource.Play();
        TimerManager.Instance.GetOneTimer(_audioSource.clip.length,()=>
        {
            GameObjectPool.Instance.PushToPool(this.gameObject);
        });
    }
}
