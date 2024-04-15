using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

class ProductValue
{
    public int price;
    public int income;
}

public class Product : MonoBehaviour
{
    // private ProductStruct test;

    [SerializeField] private TextMeshProUGUI _txtTitle;
    [SerializeField] private TextMeshProUGUI _txtCurrentIncome;
    [SerializeField] private TextMeshProUGUI _txtPrice;

    [SerializeField] private Button _button;
    [SerializeField] private Image _image;

    [SerializeField] private int _levelIndex;
    [SerializeField] private int _bonus;

    private ProductValue _productValue;

    private int _price = 0;

    private void Start()
    {
        _button.onClick.AddListener(BuyProduct);
    }

    public void SetValueProduct()
    {
        _txtPrice.text = $"{_price} <sprite=\"coin\" name=\"coin\"> ";

        _txtCurrentIncome.text =
            $"{YandexGame.savesData.shopData.income[_levelIndex]} <sprite=\"coin\" name=\"coin\"> в {YandexGame.savesData.shopData.timeIncome[_levelIndex]} секунды";

        GameManager.instance.ElementsManager.CheckElements();

        GameManager.instance.GetCoinManager.UpdateUI();


        // _productValue.income = bonus;
        // _productValue.price = price;
    }

    #region Properties

    public int SetPrice
    {
        set { _price = value; }
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

    #endregion


    public void BuyProduct()
    {
        if (GameManager.instance.AmountOfMoney >= _price)
        {
            GameManager.instance.AmountOfMoney -= _price;

            _price += Mathf.FloorToInt((float)_price / 100 * 50);

            _txtPrice.text = $"{_price} <sprite=\"coin\" name=\"coin\"> ";

            // GameManager.instance.ElementsManager.ElementsLevels[_levelIndex].price = _price;
            YandexGame.savesData.shopData.price[_levelIndex] = _price;
            YandexGame.savesData.shopData.income[_levelIndex] += _bonus;
            // GameManager.instance.ElementsManager.ElementsLevels[_levelIndex].income += _bonus;

            _txtCurrentIncome.text =
                $"{YandexGame.savesData.shopData.income[_levelIndex]} <sprite=\"coin\" name=\"coin\"> в {YandexGame.savesData.shopData.timeIncome[_levelIndex]} секунды";

            GameManager.instance.ElementsManager.CheckElements();

            GameManager.instance.GetCoinManager.UpdateUI();
        }
        else
        {
            Debug.LogError($"Не хватает + {_price - GameManager.instance.AmountOfMoney}");
        }
    }


    // public ProductStruct ProductProperty
    // {
    //     get { return test; }
    //     set
    //     {
    //         _txtPrice.text = value.price + " <sprite=\"coin\" name=\"coin\">";
    //         _txtCurrentIncome.text = value.currentIncome.ToString();
    //         _txtTitle.text = value.title;
    //
    //         _price = value.price;
    //         test = value;
    //     }
    // }
}