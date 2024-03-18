using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour
{
    [SerializeField] private GameObject _beds;
    [SerializeField] private InfoObject _prefabLevelOne; 
    
    
    [Serializable]
    public struct ElementsLevelStruct
    {
        public string level;
        public int count;
    }

    [SerializeField] private ElementsLevelStruct[] _elementsLevel;

    public ElementsLevelStruct[] ElementsLevels
    {
        get { return _elementsLevel; }
    }

    public void CheckElements(int destroyElementLevel = 999)
    {
        ResetLevels();
        
        for (int i = 0; i < _beds.transform.childCount; i++)
        {
            if (_beds.transform.GetChild(i).gameObject.activeSelf)
            {
                var place = _beds.transform.GetChild(i).transform.GetChild(0);

                if (place.transform.childCount > 0)
                {
                    if (place.GetChild(place.transform.childCount - 1).tag == "Element")
                    {
                        var currentLevel = place.GetChild(place.transform.childCount - 1).GetComponent<InfoObject>().GetLevel;
                        _elementsLevel[currentLevel].count += 1;
                    }
                }
            } 
        }

        if (destroyElementLevel != 999)
        {
            
            // ЭТО УБРАТЬ
            if (destroyElementLevel == 1)
            {
                _elementsLevel[destroyElementLevel].count -= 2;
            }
            else
            {
                _elementsLevel[destroyElementLevel].count -= 1;
            }
        }
        
        
        // Debug.LogError("Проверка элементов");
    }

    void ResetLevels()
    {
        for (int i = 0; i < _elementsLevel.Length; i++)
        {
            _elementsLevel[i].count = 0;
        }
    }
}
