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

    private float _price = 0;

    private void Start()
    {
        _button.onClick.AddListener(BuyProduct);
    }






    [Serializable]
    public struct ProductStruct
    {
        public string title;
        public string currentIncome;
        public float price;
    }


    public void BuyProduct()
    {
        if (GameManager.instance.AmountOfMoney >= _price)
        {
            GameManager.instance.AmountOfMoney -= _price;
            
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
            _txtPrice.text = value.price + " $";
            _txtCurrentIncome.text = value.currentIncome;
            _txtTitle.text = value.title;

            _price = value.price;
            test = value;
        }
    }
}