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
        //print("Uadio: " + _audioSource.clip.name);
        _audioSource.Play();
    }

    public void PlayButtonSound(){
        onUiSoundPlayCallBack(_buttonSound);
    }
    public void PlaySound(int i, bool loop){
        if(_audioSource.isPlaying){
            //do stuff maybe?
        }
        if(loop)
            _audioSource.loop = true;
        else
            _audioSource.loop = false;

        if(i > soundList.Count-1 || i<0){
            Debug.LogError("Int 'i' outside of list bounds: 0-"+(soundList.Count-1));
        }
        AudioClip sound = soundList[i];
        onUiSoundPlayCallBack(sound);
    }

    public AudioSource GetAudioSource(){
        return _audioSource;
    }

}
