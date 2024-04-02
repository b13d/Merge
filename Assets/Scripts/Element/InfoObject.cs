using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class InfoObject : MonoBehaviour
{
    [SerializeField] private int _level = 0;
    [SerializeField] private int _revenue;
    // revenue - это доход от элемента
    // incomeTime - кол-во секунд до того как монеты капнут

    [SerializeField] private bool _isLastElement;
    [SerializeField] private float _incomeTime;
    [SerializeField] private CanvasUIElement _canvasUI;

    private int _showUIVia = 0;
    
    private float _startIncomeTime;
    
    private void Start()
    {
        _revenue = GameManager.instance.ElementsManager.ElementsLevels[_level].income;
        // _revenue = YandexGame.savesData.shopData.bonusElement[_level];
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
        Debug.Log($"Прошло {_startIncomeTime} времени и принес {_revenue} денежек");
        
        GameManager.instance.GetCoinManager.CollectionOfMoney(_revenue);

        if (_showUIVia <= 0)
        {
            var ui = _canvasUI;
            ui.SetIncomeCurrent = _revenue;
            
            Instantiate(ui, transform.position, Quaternion.identity);

            _showUIVia = 3;
        }

        _showUIVia -= 1;
    }

    public bool IsLastElement
    {
        get { return _isLastElement; }
    }

    public float GetPrice()
    {
        return _revenue;
    }

    public float GetIncomeTime
    {
        get { return _incomeTime; }
    }
    
    public int GetLevel
    {
        get { return _level; }
    }

    public int SetPrice
    {
        set { _revenue = value; }
    }
}