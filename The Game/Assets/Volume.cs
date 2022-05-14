using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    private AudioSource audioClip;
    public float musicVolume;
    public Slider slider;
    //public GameObject symbol;

    // Start is called before the first frame update
    void Start()
    {
        musicVolume = PlayerPrefs.GetFloat("Volume");
        slider.value = musicVolume;
        audioClip = GetComponent<AudioSource>();
        audioClip.volume = musicVolume;
        audioClip.time = PlayerPrefs.GetFloat("Time");
    }

    // Update is called once per frame
    void Update()
    {
        audioClip.volume = slider.value;
        PlayerPrefs.SetFloat("Volume", audioClip.volume);
        PlayerPrefs.Save();
        PlayerPrefs.SetFloat("Time", audioClip.time);
        PlayerPrefs.Save();
        //if (audioClip.volume < 1e-9 || audioClip.mute==true)
        //    symbol.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Mute");
        //else
        //    symbol.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Unmute");
    }

    //public void SetVolume()
    //{
    //    musicVolume = vol;
    //}

    //public void Mute()
    //{
    //    audioClip.mute = !audioClip.mute;
    //}
}
