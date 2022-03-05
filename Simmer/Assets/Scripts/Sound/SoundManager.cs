using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Slider volumeSlider;
    void Start()
    {
      if(!PlayerPrefs.HasKey("musicVolume"))
      {
        PlayerPrefs.SetFloat("musicVolume", 0.5f);
        Load();
      }
    }

    public void ChangeVolume()
    {
      AudioListener.volume = volumeSlider.value;
    }

    private void Load()
    {
      volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }


}
