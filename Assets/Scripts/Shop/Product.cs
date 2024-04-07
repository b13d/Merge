using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Product : MonoBehaviour
{
    private ProductStruct test;

    [SerializeField] private TextMeshProUGUI _txtTitle;
    [SerializeField] private TextMeshProUGUI _txtCurrentIncome;
    [SerializeField] private TextMeshProUGUI _txtPrice;
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private int _levelIndex;
    [SerializeField] private int _bonus;

    private int _price = 0;

    private void Start()
    {
        _button.onClick.AddListener(BuyProduct);
    }

    public int SetLevelIndex
    {
        set { _levelIndex = value; }
    }

    public Sprite SetImage
    {
        set { _image.sprite = value; }
    }

    public int SetBonus
    {
        set { _bonus = value; }
    }


    [Serializable]
    public struct ProductStruct
    {
        public string title;
        public string currentIncome;
        public int price;
    }


    public void BuyProduct()
    {
        if (GameManager.instance.AmountOfMoney >= _price)
        {
            GameManager.instance.AmountOfMoney -= _price;

            _price += Mathf.FloorToInt((float)_price / 100 * 50);

            _txtPrice.text = $"{_price} <sprite=\"coin\" name=\"coin\"> ";

            GameManager.instance.ElementsManager.ElementsLevels[_levelIndex].price = _price;

            GameManager.instance.ElementsManager.ElementsLevels[_levelIndex].income += _bonus;

            _txtCurrentIncome.text =
                $"{GameManager.instance.ElementsManager.ElementsLevels[_levelIndex].income} <sprite=\"coin\" name=\"coin\"> в {GameManager.instance.ElementsManager.ElementsLevels[_levelIndex].incomeTime} секунды";

            GameManager.instance.ElementsManager.CheckElements();

            GameManager.instance.GetCoinManager.UpdateUI();
        }
        else
        {
            Debug.LogError($"Не хватает + {_price - GameManager.instance.AmountOfMoney}");
        }
    }


    public ProductStruct ProductProperty
    {
        get { return test; }
        set
        {
            _txtPrice.text = value.price + " <sprite=\"coin\" name=\"coin\">";
            _txtCurrentIncome.text = value.currentIncome;
            _txtTitle.text = value.title;

            _price = value.price;
            test = value;
        }
    }
}