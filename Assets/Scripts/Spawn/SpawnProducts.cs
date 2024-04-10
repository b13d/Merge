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
    }

    Product.ProductStruct SetPropetriesValue(int price, string title, string currentIncome)
    {
        var productProperty = new Product.ProductStruct();

        productProperty.price = price;
        productProperty.title = title;
        productProperty.currentIncome = currentIncome;

        return productProperty;
    }

    void InitialProducts()
    {
        _contentView.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

        for (int i = 0; i <= _countProducts; i++)
        {
            _contentView.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 200);


            var newProduct = Instantiate(_productPrefab, transform.position, Quaternion.identity, _contentView);

            var productProperties = new Product.ProductStruct();

            if (YandexGame.savesData.shopData.price.Count > 0 && YandexGame.savesData.shopData.price[0] > 0)
            {
                productProperties = SetPropetriesValue(YandexGame.savesData.shopData.price[i], $"Элемент {i}",
                    $"{YandexGame.savesData.shopData.bonusElement[i]} <sprite=\"coin\" name=\"coin\"> в {_incomeTimeList[i]} секунды");
            }
            else
            {
                productProperties = SetPropetriesValue((i + 1) * 10, $"Элемент {i}",
                    $"{((i + 1) * 10)} <sprite=\"coin\" name=\"coin\"> в {_incomeTimeList[i]} секунды");
            }


            newProduct.GetComponent<Product>().SetBonus = productProperties.price;
            newProduct.GetComponent<Product>().SetLevelIndex = i;
            newProduct.GetComponent<Product>().SetImage = _spritesElement[i];
            newProduct.GetComponent<Product>().ProductProperty = productProperties;
        }
    }

    public void ActiveNewProduct(int id)
    {
        _contentView.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 200);

        var newProduct = Instantiate(_productPrefab, transform.position, Quaternion.identity, _contentView);
        var productProperties = new Product.ProductStruct();

        if (YandexGame.savesData.shopData.price.Count > 0 && YandexGame.savesData.shopData.price[0] > 0)
        {
            productProperties = SetPropetriesValue(YandexGame.savesData.shopData.price[id], $"Элемент {id}",
                $"{YandexGame.savesData.shopData.bonusElement[id]} <sprite=\"coin\" name=\"coin\"> в {_incomeTimeList[id]} секунды");
        }
        else
        {
            productProperties = SetPropetriesValue((id + 1) * 10, $"Элемент {id}",
                $"{((id + 1) * 10)} <sprite=\"coin\" name=\"coin\"> в {_incomeTimeList[id]} секунды");
        }


        newProduct.GetComponent<Product>().SetBonus = productProperties.price;
        newProduct.GetComponent<Product>().SetLevelIndex = id;
        newProduct.GetComponent<Product>().SetImage = _spritesElement[id];
        newProduct.GetComponent<Product>().ProductProperty = productProperties;
    }
}