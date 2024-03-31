using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoObject : MonoBehaviour
{
    [SerializeField] private int _level = 0;
    [SerializeField] private int _currentPrice;

    [SerializeField] private bool _isLastElement;
    [SerializeField] private float _incomeTime;
    private float _startIncomeTime;
    
    private void Start()
    {
        _currentPrice = GameManager.instance.ElementsManager.ElementsLevels[_level].income;
        _startIncomeTime = _incomeTime;
    }

    private void Update()
    {
        _incomeTime -= Time.deltaTime;

        if (_incomeTime <= 0)
        {
            _incomeTime = _startIncomeTime;

            Profit();
        }
    }

    void Profit()
    {
        Debug.Log($"Прошло {_startIncomeTime} времени и принес {_currentPrice} денежек");
        
        GameManager.instance.GetCoinManager.CollectionOfMoney(_currentPrice);
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

    public int SetPrice
    {
        set { _currentPrice = value; }
    }
}