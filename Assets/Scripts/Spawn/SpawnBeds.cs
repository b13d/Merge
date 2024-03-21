using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SpawnBeds : MonoBehaviour
{
    [SerializeField] private List<GameObject> _placeBeds = new List<GameObject>();
    [SerializeField] private List<int> _placeBusy = new List<int>();
    [SerializeField] private List<GameObject> _elemets = new List<GameObject>();
    [SerializeField] private GameObject _placeElements;
    [SerializeField] private Sprite _defaultSpriteBed;
    [SerializeField] private GameObject _box;

    private int _countElements = 0;

    // Тут еще надо добавить лист который или что-то типо того, который
    // показывает сколько скрыто, чтобы ориентироваться только на видимые
    // грядки

    private void Update()
    {
        Debug.Log("isFull: " + GameManager.instance.GetFull);
    }

    public List<GameObject> PlaceBeds
    {
        get { return _placeBeds; }
    }

    public int SetPlaceBusy
    {
        set { _placeBusy[value] = 1; }
    }
    
    
    public void ClearBeds()
    {
        for (int i = 0; i < _placeBeds.Count; i++)
        {
            _placeBeds[i].transform.parent.GetComponent<Image>().sprite = _defaultSpriteBed;
        }

        StopCoroutine(WaitClearBeds());
        StartCoroutine(WaitClearBeds());
    }

    IEnumerator WaitClearBeds()
    {
        yield return new WaitForSeconds(.5f);

        CheckBedsOnVoid();
    }

    public void CheckBedsOnVoid()
    {
        List<int> _activeBeds = new List<int>();

        for (int j = 0; j < _placeBeds.Count; j++)
        {
            if (_placeBeds[j].transform.parent.gameObject.activeSelf)
            {
                _activeBeds.Add(j);
            }
        }
        
        
        
        for (int i = 0; i < _activeBeds.Count; i++)
        {
            if (_placeBeds[_activeBeds[i]].transform.childCount == 0)
            {
                _placeBusy[i] = 0;

                GameManager.instance.GetFull = false;
            }
            else
            {
                _countElements += 1;
                _placeBusy[i] = 1;
            }
        }

        if (_countElements == _activeBeds.Count)
        {
            GameManager.instance.GetFull = true;
        }
    }

    public void SpawnBox()
    {
        List<int> _clearPlace = new List<int>();
        List<int> _activeBeds = new List<int>();

        for (int j = 0; j < _placeBeds.Count; j++)
        {
            if (_placeBeds[j].transform.parent.gameObject.activeSelf)
            {
                _activeBeds.Add(j);
            }
        }

        for (int i = 0; i < _activeBeds.Count; i++)
        {
            if (_placeBusy[i] == 0)
            {
                _clearPlace.Add(i);
            }
        }

        if (_clearPlace.Count == 1)
        {
            GameManager.instance.GetFull = true;
        }

        if (_clearPlace.Count == 0)
        {
            GameManager.instance.GetFull = true;

            return;
        }

        int rnd = Random.Range(0, _clearPlace.Count);

        _placeBusy[_clearPlace[rnd]] = 1;
        var newBox = Instantiate(_box, transform.position, Quaternion.identity, _placeBeds[_clearPlace[rnd]].transform);
        newBox.transform.localPosition = new Vector3(0, 0, 2f);
        newBox.GetComponent<Box>().SetIndex = _clearPlace[rnd];
        
        GameManager.instance.SavePositionElement();
    }
}