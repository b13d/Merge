using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SpawnProducts : MonoBehaviour
{
    [SerializeField] private GameObject _productPrefab;
    [SerializeField] private Transform _contentView;
    [SerializeField] private ButtonEvents _buttonEvents;

    [SerializeField] private List<Sprite> _spritesElement;

    private List<float> _incomeTimeList = new List<float>() { 2, 1.9f, 1.8f, 1.7f, 1.6f, 1.5f, 1.4f, 1.3f, 1.2f, 1.1f };
    private int _countProducts;

    void Start()
    {
        _countProducts = YandexGame.savesData.lastAchievementID;
        InitialProducts();

        Debug.Log("lastAchievementsID: " + YandexGame.savesData.lastAchievementID);
    }

    void InitialProducts()
    {
        _contentView.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        
        for (int i = 0; i <= _countProducts; i++)
        {
            _contentView.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 200);

            
            var newProduct = Instantiate(_productPrefab, transform.position, Quaternion.identity, _contentView);
            var productProperty = new Product.ProductStruct();

            if (YandexGame.savesData.shopData.price.Count > 0 && YandexGame.savesData.shopData.price[0] > 0)
            {
                productProperty.price = YandexGame.savesData.shopData.price[i];
                productProperty.title = $"Элемент {i}";
                productProperty.currentIncome =
                    $"{YandexGame.savesData.shopData.bonusElement[i]} <sprite=\"coin\" name=\"coin\"> в {_incomeTimeList[i]} секунды";
            }
            else
            {
                productProperty.price = (i + 1) * 10;
                productProperty.title = $"Элемент {i}";
                productProperty.currentIncome =
                    $"{((i + 1) * 10)} <sprite=\"coin\" name=\"coin\"> в {_incomeTimeList[i]} секунды";
            }



            newProduct.GetComponent<Product>().SetBonus = productProperty.price;
            newProduct.GetComponent<Product>().SetLevelIndex = i;
            newProduct.GetComponent<Product>().SetImage = _spritesElement[i];
            newProduct.GetComponent<Product>().ProductProperty = productProperty;
        }
    }

    public void ActiveNewProduct(int id)
    {
        _contentView.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 200);
        
        var newProduct = Instantiate(_productPrefab, transform.position, Quaternion.identity, _contentView);
        var productProperty = new Product.ProductStruct();

        if (YandexGame.savesData.shopData.price.Count > 0 && YandexGame.savesData.shopData.price[0] > 0)
        {
            productProperty.price = YandexGame.savesData.shopData.price[id];
            productProperty.title = $"Элемент {id}";
            productProperty.currentIncome =
                $"{YandexGame.savesData.shopData.bonusElement[id]} <sprite=\"coin\" name=\"coin\"> в {_incomeTimeList[id]} секунды";
        }
        else
        {
            productProperty.price = (id + 1) * 10;
            productProperty.title = $"Элемент {id}";
            productProperty.currentIncome =
                $"{((id + 1) * 10)} <sprite=\"coin\" name=\"coin\"> в {_incomeTimeList[id]} секунды";
        }



        newProduct.GetComponent<Product>().SetBonus = productProperty.price;
        newProduct.GetComponent<Product>().SetLevelIndex = id;
        newProduct.GetComponent<Product>().SetImage = _spritesElement[id];
        newProduct.GetComponent<Product>().ProductProperty = productProperty;
    }

}