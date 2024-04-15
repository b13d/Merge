using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

// revenue - это доход от элемента
// incomeTime - кол-во секунд до того как монеты капнут

public class InfoObject : MonoBehaviour
{
    [SerializeField] private int _level = 0;
    [SerializeField] private int _revenue;

    [SerializeField] private bool _isLastElement;
    [SerializeField] private float _incomeTime;
    [SerializeField] private CanvasUIElement _canvasUI;

    private int _showUIVia = 0;
    private float _startIncomeTime;
    
    private void Start()
    {
        _revenue = YandexGame.savesData.shopData.income[_level];
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
        // Debug.Log($"revenue: {_revenue}");
        _revenue = YandexGame.savesData.shopData.income[_level];
        
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

    #region Properties

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

    public int SetRevenue
    {
        set { _revenue = value; }
    }
    
    // public int SetPrice
    // {
    //     set { _revenue = value; }
    // }

    #endregion
    
    
}