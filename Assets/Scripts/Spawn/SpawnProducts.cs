using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnProducts : MonoBehaviour
{
    [SerializeField] private GameObject _productPrefab;
    [SerializeField] private Transform _contentView;
    [SerializeField] private ButtonEvents _buttonEvents;

    [SerializeField] private List<Sprite> _spritesElement; 
    
    private int _countProducts = 8;    
    
    void Start()
    {
        InitialProducts();
    }

    void InitialProducts()
    {
        for (int i = 0; i < _countProducts; i++)
        {
            var newProduct = Instantiate(_productPrefab, transform.position, Quaternion.identity, _contentView);
            var productProperty = new Product.ProductStruct();
            productProperty.price = (i + 1) * 10;
            productProperty.title = $"Элемент {i}";
            productProperty.currentIncome = ((i + 1) * 10) + " в секунду";

            newProduct.GetComponent<Product>().SetBonus = (i + 1) * 10;
            newProduct.GetComponent<Product>().SetLevelIndex = i;
            newProduct.GetComponent<Product>().SetImage = _spritesElement[i];
            newProduct.GetComponent<Product>().ProductProperty = productProperty;
        }
    }
}
