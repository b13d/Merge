using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _audio;

    public void SliderAudio(Slider sliderAudio)
    {
        _audio.volume = sliderAudio.value;
    }
    
    public void SliderMusic(Slider sliderMusic)
    {
        _music.volume = sliderMusic.value;
    }
}
