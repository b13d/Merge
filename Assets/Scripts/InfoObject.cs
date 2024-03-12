using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoObject : MonoBehaviour
{
    [SerializeField] private int _level = 0;

    public int GetLevel
    {
        get { return _level; }
    }
}