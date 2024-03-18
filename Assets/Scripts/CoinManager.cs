using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private List<int> _pricesElements = new List<int>(16);
    [SerializeField] private TextMeshProUGUI _txtCountMoney;
    [SerializeField] private TextMeshProUGUI _txtBalance;
    [SerializeField] private ElementManager _elementManager;

    [SerializeField] private TextMeshProUGUI[] _txtStatsCountMoney;

    private float _second = 1f;

    private void Update()
    {
        _second -= Time.deltaTime;

        if (_second < 0)
        {
            _second = 1f;
            CollectionOfMoney();
        }
    }

    void CollectionOfMoney()
    {
        int sum = int.Parse(_txtCountMoney.text);

        for (int i = 0; i < _pricesElements.Count; i++)
        {
            sum += _pricesElements[i] * _elementManager.ElementsLevels[i].count;

            if (i < 2)
            {
                _txtStatsCountMoney[i].text =
                    $"Элемент {i} уровня ({_elementManager.ElementsLevels[i].count}) приносит: {_pricesElements[i] * _elementManager.ElementsLevels[i].count}";
            }
        }

        GameManager.instance.AmountOfMoney = sum;
        _txtCountMoney.text = sum.ToString();
        _txtBalance.text = sum.ToString();
    }

    public void UpdateUI()
    {
        _txtBalance.text = GameManager.instance.AmountOfMoney.ToString();
        _txtCountMoney.text = GameManager.instance.AmountOfMoney.ToString();
    }
}