using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMove : MonoBehaviour
{
    [SerializeField] private AudioClip _audio;

    public AudioClip Audio
    {
        set { _audio = value; }
    }
    
    void Start()
    {
        Destroy(gameObject, 1f);
        GetComponent<AudioSource>().clip = _audio;
        GetComponent<AudioSource>().Play();
    }
}
