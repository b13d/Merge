using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SpawnProducts : MonoBehaviour
{
    [SerializeField] private GameObject _prefabProduct;
    [SerializeField] private ButtonEvents _buttonEvents;
    
    private int _countProducts;

    void Start()
    {
        _countProducts = YandexGame.savesData.lastAchievementID;
        InitialProducts();
    }

    void InitialProducts()
    {
        Products._placeSpawnStatic.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        
        for (int i = 0; i <= _countProducts; i++)
        {
            Products._placeSpawnStatic.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 200);

            var newProduct = Instantiate(_prefabProduct, transform.position, Quaternion.identity, Products._placeSpawnStatic);

            newProduct.GetComponent<Product>().SetBonus = Bonus(i);
            newProduct.GetComponent<Product>().SetLevelIndex = i;
            newProduct.GetComponent<Product>().SetPrice = Price(i);
            newProduct.GetComponent<Product>().SetImage = Products._spritesElementStatic[i];
            
            newProduct.GetComponent<Product>().SetValueProduct();
        }
    }

    int Bonus(int id)
    {
            return (id + 1) * 10;
    }
    
    int Price(int id)
    {
        if (YandexGame.savesData.shopData.price[id] == 0)
        {
            return (id + 1) * 10;
        }
        else
        {
            return YandexGame.savesData.shopData.price[id];
        }
    }
    
    private GameObject GetPrefabProduct
    {
        get { return _prefabProduct; }
    }
    
    public void AddNewProduct(int productID)
    {
        Products._placeSpawnStatic.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 200);

        var newProduct = Instantiate(_prefabProduct, transform.position, Quaternion.identity, Products._placeSpawnStatic);

        newProduct.GetComponent<Product>().SetBonus = Bonus(productID);
        newProduct.GetComponent<Product>().SetLevelIndex = productID;
        newProduct.GetComponent<Product>().SetPrice = Price(productID);
        newProduct.GetComponent<Product>().SetImage = Products._spritesElementStatic[productID];
        
        newProduct.GetComponent<Product>().SetValueProduct();
    }
}