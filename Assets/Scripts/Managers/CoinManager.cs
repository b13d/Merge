using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using YG;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private List<int> _pricesElements = new List<int>(16);
    [SerializeField] private TextMeshProUGUI _txtCountMoney;
    [SerializeField] private TextMeshProUGUI _txtBalance;
    [SerializeField] private ElementManager _elementManager;
    [SerializeField] private TextMeshProUGUI _txtIncome;

    private float _second = 1f;
    private int sum;

    private void Start()
    {
        _txtBalance.text = YandexGame.savesData.money.ToString();
        _txtCountMoney.text = YandexGame.savesData.money.ToString();
        sum = YandexGame.savesData.money;
    }

    // private void Update()
    // {
    //     _second -= Time.deltaTime;
    //
    //     if (_second < 0)
    //     {
    //         _second = 1f;
    //         CollectionOfMoney();
    //     }
    // }

    public void CollectionOfMoney(int money)
    {
        sum = int.Parse(_txtCountMoney.text);
        // sum += GameManager.instance.ElementsManager.GetIncome;
        sum += money;
        
        _txtIncome.text = $"{GameManager.instance.ElementsManager.GetIncome}";
            
        GameManager.instance.AmountOfMoney = sum;
        _txtCountMoney.text = sum.ToString();
        _txtBalance.text = sum.ToString();

        if (sum > YandexGame.savesData.recordMoney)
        {
            YandexGame.savesData.recordMoney = sum;
        }
    }

    public int GetMoney
    {
        get { return sum; }
    }
    public void UpdateUI()
    {
        _txtBalance.text = GameManager.instance.AmountOfMoney.ToString();
        _txtCountMoney.text = GameManager.instance.AmountOfMoney.ToString();
    }
}