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
    private int _sum;

    private void Start()
    {
        _txtBalance.text = YandexGame.savesData.money.ToString();
        _txtCountMoney.text = YandexGame.savesData.money.ToString();
        _sum = YandexGame.savesData.money;
    }

    public void CollectionOfMoney(int money)
    {
        _sum = int.Parse(_txtCountMoney.text);
        _sum += money;
        
        _txtIncome.text = $"{GameManager.instance.ElementsManager.GetIncome}";
            
        GameManager.instance.AmountOfMoney = _sum;
        _txtCountMoney.text = _sum.ToString();
        _txtBalance.text = _sum.ToString();

        if (_sum > YandexGame.savesData.recordMoney)
        {
            YandexGame.savesData.recordMoney = _sum;
        }
    }

    public int GetMoney
    {
        get { return _sum; }
    }
    public void UpdateUI()
    {
        _txtBalance.text = GameManager.instance.AmountOfMoney.ToString();
        _txtCountMoney.text = GameManager.instance.AmountOfMoney.ToString();
    }
}