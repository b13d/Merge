using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioList : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _audioClips = new List<AudioClip>();

    public List<AudioClip> AudioClips
    {
        get { return _audioClips; }
    }
}
