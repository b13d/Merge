using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    [SerializeField] private bool _isCloseBed = false;

    public bool GetIsCloseBed
    {
        get { return _isCloseBed; }
        set { _isCloseBed = value; }
    }
}