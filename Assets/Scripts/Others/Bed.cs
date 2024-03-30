using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    [SerializeField] private bool _isCloseBed = false;
    [SerializeField] private GameObject _bush;
    [SerializeField] private GameObject _highlighting;
    [SerializeField] private GameObject _placeElement;
    
    public GameObject GetHighlighting
    {
        get { return _highlighting; }
    }

    public GameObject GetPlaceElement
    {
        get { return _placeElement; }
    }
    
    private void Start()
    {
        if (_isCloseBed)
        {
            gameObject.layer = 7;
        }
        else
        {
            gameObject.layer = 0;
        }
    }

    public bool GetIsCloseBed
    {
        get { return _isCloseBed; }
        set
        {
            if (value)
            {
                _bush.SetActive(true);
                _isCloseBed = value;
            }
            else
            {
                _isCloseBed = value;
                _bush.SetActive(false);
            }
        }
    }
}