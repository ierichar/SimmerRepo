using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UISoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<AudioClip> soundList = new List<AudioClip>();
    public UnityEvent<AudioClip> onUiSoundPlay = new UnityEvent<AudioClip>();
    private void onUiSoundPlayCallBack(AudioClip audio)
    {
      

    }

}
