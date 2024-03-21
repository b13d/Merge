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
    }

    [SerializeField] private ElementsLevelStruct[] _elementsLevel;

    // private void Awake()
    // {
    //    _currentIncrementPerSecond = YandexGame.savesData.incomeMoney;
    // }

    public int GetIncome
    {
        get { return _currentIncrementPerSecond; }
    }    
    
    public ElementsLevelStruct[] ElementsLevels
    {
        get { return _elementsLevel; }
    }

    public void CheckElements(int destroyElementLevel = 999, GameObject elementDelete = null, GameObject secondsElement = null, bool isLast = false)
    {
        Debug.LogError("Проверяю элементы: " + elementDelete);

        ResetLevels();
        
        _currentIncrementPerSecond = 0;

        for (int i = 0; i < _beds.transform.childCount; i++)
        {
            if (_beds.transform.GetChild(i).gameObject.activeSelf)
            {
                var place = _beds.transform.GetChild(i).transform.GetChild(0);

                if (place.transform.childCount > 0)
                {
                    if (place.GetChild(place.transform.childCount - 1).tag == "Element")
                    {
                        if (elementDelete != null)
                        {
                            if (elementDelete == place.GetChild(place.transform.childCount - 1).gameObject)
                            {
                                continue;
                            }
                        }

                        if (secondsElement != null)
                        {
                            if (secondsElement == place.GetChild(place.transform.childCount - 1).gameObject && isLast)
                            {
                                continue;
                            }
                        }

                        var currentLevel = place.GetChild(place.transform.childCount - 1).GetComponent<InfoObject>()
                            .GetLevel;
                        _elementsLevel[currentLevel].count += 1;
                        _currentIncrementPerSecond += _elementsLevel[currentLevel].income;
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