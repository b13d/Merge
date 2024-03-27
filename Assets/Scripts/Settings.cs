using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _audio;
    // [SerializeField] private Slider _audioSlider;
    // [SerializeField] private Slider _musicSlider;
    
    void Start()
    {
        // _audio.volume = _audioSlider.value;
        // _music.volume = _musicSlider.value;
    }

    public void SliderAudio(Slider sliderAudio)
    {
        _audio.volume = sliderAudio.value;
    }
    
    public void SliderMusic(Slider sliderMusic)
    {
        _music.volume = sliderMusic.value;
    }
}
