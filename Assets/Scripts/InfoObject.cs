using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoObject : MonoBehaviour
{
    [SerializeField] private int _level = 0;
    [SerializeField] private int _currentPrice;

    [SerializeField] private bool _isLastElement;

    private void Start()
    {
        _currentPrice = GameManager.instance.ElementsManager.ElementsLevels[_level].income;
    }

    public bool IsLastElement
    {
        get { return _isLastElement; }
    }

    public float GetPrice()
    {
        return _currentPrice;
    }

    public int GetLevel
    {
        get { return _level; }
    }
}