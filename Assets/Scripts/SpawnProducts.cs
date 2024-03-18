using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProducts : MonoBehaviour
{
    [SerializeField] private GameObject _productPrefab;
    [SerializeField] private Transform _contentView;
    [SerializeField] private ButtonEvents _buttonEvents;
    
    private int _countProducts = 16;    
    
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
            
            
            newProduct.GetComponent<Product>().ProductProperty = productProperty;
        }
    }
}
