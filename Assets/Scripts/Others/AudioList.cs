using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioList : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _switchedAudio = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _moveAudio = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _joinAudio = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _otherAudio = new List<AudioClip>();

    private float _seconds = 2f;
    private int _countSpeed = 0;
    private bool _isJoinElements;

    private void Update()
    {
        if (_isJoinElements)
        {
            _seconds -= Time.deltaTime;
        }
        
        if (_seconds <= 0)
        {
            _isJoinElements = false;
            _seconds = 2f;
            _countSpeed = 0;
        }
    }

    public AudioClip GetSwitchedElementsAudio()
    {
        var rnd = Random.Range(0, _switchedAudio.Count);

        return _switchedAudio[rnd];
    }

    public AudioClip GetOtherSound(int id)
    {
        return _otherAudio[id];
    }
    
    public AudioClip GetMoveElementsAudio()
    {
        var rnd = Random.Range(0, _moveAudio.Count);

        return _moveAudio[rnd];
    }
    
    public AudioClip GetJoinElementsAudio()
    {
        _isJoinElements = true;

        _seconds = 2f;
        
        var rnd = Random.Range(0, _joinAudio.Count);

        if (_countSpeed < 5)
        {
            _countSpeed += 1;
        } 
        
        return _joinAudio[rnd];
    }

    public int GetCountSpeed
    {
        get { return _countSpeed; }
    }

}
