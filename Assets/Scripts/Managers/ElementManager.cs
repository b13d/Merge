using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class ElementManager : MonoBehaviour
{
    [SerializeField] private GameObject _beds;
    [SerializeField] private InfoObject _prefabLevelOne;
    [SerializeField] private int _currentIncrementPerSecond = 0;

    [Serializable]
    public struct ElementsLevelStruct
    {
        public string level;
        public int count;
        public int income;
        public int price;
    }

    [SerializeField] private ElementsLevelStruct[] _elementsLevel;

    private void Start()
    {
        CheckElements();
        InitialElementsData();
    }

    void InitialElementsData()
    {
        if (YandexGame.savesData.shopData.price.Count > 0 && YandexGame.savesData.shopData.price[0] > 0)
        {
            for (int i = 0; i < _elementsLevel.Length; i++)
            {
                _elementsLevel[i].price = YandexGame.savesData.shopData.price[i];
                _elementsLevel[i].income = YandexGame.savesData.shopData.bonusElement[i];
            }
            
            CheckElements();
        }
    }

    public int GetIncome
    {
        get { return _currentIncrementPerSecond; }
    }

    public ElementsLevelStruct[] ElementsLevels
    {
        get { return _elementsLevel; }
    }

    public void CheckElements(int destroyElementLevel = 999, GameObject elementDelete = null,
        GameObject secondsElement = null, bool isLast = false)
    {
        ResetLevels();

        _currentIncrementPerSecond = 0;

        for (int i = 0; i < _beds.transform.childCount; i++)
        {
            if (_beds.transform.GetChild(i).gameObject.activeSelf)
            {
                var place = _beds.transform.GetChild(i).transform.GetChild(0);

                if (place.transform.childCount > 0)
                {
                    if (place.GetChild(place.transform.childCount - 1).CompareTag("Element"))
                    {
                        var element = place.GetChild(place.transform.childCount - 1);

                        if (elementDelete != null)
                        {
                            if (elementDelete == element.gameObject)
                            {
                                continue;
                            }
                        }

                        if (secondsElement != null)
                        {
                            if (secondsElement == element.gameObject && isLast)
                            {
                                continue;
                            }
                        }

                        var currentLevel = element.GetComponent<InfoObject>().GetLevel;
                        // element.GetComponent<InfoObject>().SetPrice = _elementsLevel[currentLevel].income;
                        // element.GetComponent<InfoObject>().SetPrice = YandexGame.savesData.shopData.price[currentLevel];
                
                        _elementsLevel[currentLevel].count += 1;
                        _currentIncrementPerSecond += YandexGame.savesData.shopData.income[currentLevel];

                        // _currentIncrementPerSecond += _elementsLevel[currentLevel].income;

                    }
                }
            }
        }
    }

    void ResetLevels()
    {
        for (int i = 0; i < _elementsLevel.Length; i++)
        {
            _elementsLevel[i].count = 0;
        }
    }
}