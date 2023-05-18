using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public Rigidbody bikeRb;
    public AudioSource bikeEngine, music;
    public float revSpeed;
    public Slider fxMainSlide, musicMainSlide, fxPauseSlide, musicPauseSlide;

    void Start()
    {
        if(PlayerPrefs.GetFloat("FXVolume") == 0)
        {
            PlayerPrefs.SetFloat("FXVolume", 0.5f);
        }
        if (PlayerPrefs.GetFloat("MusicVolume") == 0)
        {
            PlayerPrefs.SetFloat("MusicVolume", 0.5f);
        }

        fxMainSlide.value = PlayerPrefs.GetFloat("FXVolume");
        musicMainSlide.value = PlayerPrefs.GetFloat("MusicVolume");
        fxPauseSlide.value = PlayerPrefs.GetFloat("FXVolume");
        musicPauseSlide.value = PlayerPrefs.GetFloat("MusicVolume");
        
    }

    //Bike engine pitch to match speed.
    void Update()
    {
        bikeEngine.pitch = bikeRb.velocity.magnitude / revSpeed + 1;
        if (bikeEngine.pitch >= 3)
        {
            bikeEngine.pitch = 3;
        }
 
    }

    //Sliders effecting volume and syncing main, options and playerPrefs.
    public void mainFxSlider()
    {
        fxPauseSlide.value = fxMainSlide.value;
        bikeEngine.volume = fxMainSlide.value;
        PlayerPrefs.SetFloat("FXVolume", fxMainSlide.value);
    }
    public void mainMusicSlider()
    {
        musicPauseSlide.value = musicMainSlide.value;
        music.volume = musicMainSlide.value;
        PlayerPrefs.SetFloat("MusicVolume", musicMainSlide.value);
    }
    public void pauseFxSlider()
    {
        fxMainSlide.value = fxPauseSlide.value;
        bikeEngine.volume = fxPauseSlide.value;
        PlayerPrefs.SetFloat("FXVolume", fxPauseSlide.value);
    }
    public void pauseMusicSlider()
    {
        musicMainSlide.value = musicPauseSlide.value;
        music.volume = musicMainSlide.value;
        PlayerPrefs.SetFloat("MusicVolume", musicPauseSlide.value);
    }
}
