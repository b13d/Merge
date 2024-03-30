using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllocationOfBeds : MonoBehaviour
{
    [SerializeField] private GameObject _parentBeds;
    [SerializeField] private List<GameObject> _highlightingElements;

    void Start()
    {
        InitialHighlighting();
    }

    void InitialHighlighting()
    {
        for (int i = 0; i < _parentBeds.transform.childCount; i++)
        {
            _highlightingElements.Add(_parentBeds.transform.GetChild(i).GetComponent<Bed>().GetHighlighting);
        }
    }


    public void Reset()
    {
        foreach (var highlighting in _highlightingElements)
        {
            highlighting.SetActive(false);
        }
    }

    public void MatchSearch(GameObject targetElement)
    {
        for (int i = 0; i < _parentBeds.transform.childCount; i++)
        {
            var place = _parentBeds.transform.GetChild(i).GetComponent<Bed>().GetPlaceElement;
            
            if (place.transform.childCount > 0)
            {
                if (place.transform.GetChild(0).tag == "Element")
                {
                    var element = place.transform.GetChild(0);

                    if (targetElement == element.gameObject)
                    {
                        continue;
                    }
                    
                    if (GameManager.instance.GetCurrentLevelElementTaked == element.GetComponent<InfoObject>().GetLevel)
                    {
                        _highlightingElements[i].SetActive(true);
                    }
                    else
                    {
                        _highlightingElements[i].SetActive(false);
                    }
                }
                else
                {
                    _highlightingElements[i].SetActive(false);
                }
            }
        }
    }
}