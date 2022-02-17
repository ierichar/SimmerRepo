using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UISoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<AudioClip> soundList = new List<AudioClip>();
    public UnityEvent<AudioClip> onUiSoundPlay = new UnityEvent<AudioClip>();

    [SerializeField ]private AudioClip _buttonSound;

    private AudioSource _audioSource;

    public void Awake(){
        _audioSource = GetComponent<AudioSource>();
        onUiSoundPlay.AddListener(onUiSoundPlayCallBack);
    }

    private void onUiSoundPlayCallBack(AudioClip audio)
    {  
        _audioSource.clip = audio;
        _audioSource.Play();
    }

    public void PlayButtonSound(){
        onUiSoundPlayCallBack(_buttonSound);
    }

}
