using System;
using System.Collections.Generic;
using UnityEngine;

public class Products : MonoBehaviour
{
    public static List<Sprite> _spritesElementStatic;

     public static List<float> _incomeTimeListStatic = new List<float>()
        { 2, 1.9f, 1.8f, 1.7f, 1.6f, 1.5f, 1.4f, 1.3f, 1.2f, 1.1f };

    public static Transform _placeSpawnStatic;

    [SerializeField] private List<Sprite> _spritesElement;
    [SerializeField] private List<float> _incomeTimeList;
    [SerializeField] private Transform _placeSpawn;


    private void Awake()
    {
        _spritesElementStatic = _spritesElement;
        _incomeTimeListStatic = _incomeTimeList;
        _placeSpawnStatic = _placeSpawn;
    }
}