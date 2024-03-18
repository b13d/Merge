using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    void Start()
    {
        InitialBeds();
    }

    void InitialBeds()
    {
        // for (int i = 0; i < _placeBeds.Count; i++)
        // {
        //     Transform transformPlaceBed = _placeBeds[i].transform;
        //
        //     var newElement = Instantiate(_elemets[0], transformPlaceBed.position, Quaternion.identity, transformPlaceBed);
        //     newElement.transform.localPosition = new Vector3(0, 0, -2);
        //     _placeBusy[i] = 1;
        // }
    }

    // public void ClearPlace(int index)
    // {
    //     _placeBusy[index] = 0;
    //
    //     GameManager.instance.GetFull = false;
    // }
    
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
        for (int i = 0; i < _placeBeds.Count; i++)
        {
            if (_placeBeds[i].transform.childCount == 0)
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

        if (_countElements == _placeBeds.Count)
        {
            GameManager.instance.GetFull = true;
        }
    }

    public void SpawnBox()
    {
        List<int> _clearPlace = new List<int>();
        
        for (int i = 0; i < _placeBusy.Count; i++)
        {
            if (_placeBusy[i] == 0)
            {
                _clearPlace.Add(i);
            }
        }

        if (_clearPlace.Count == 1)
        {
            GameManager.instance.GetFull = true;
            
            // Debug.LogError("Full");
        }
        
        int rnd = Random.Range(0, _clearPlace.Count);

        _placeBusy[_clearPlace[rnd]] = 1;
        var newBox = Instantiate(_box, transform.position, Quaternion.identity, _placeBeds[_clearPlace[rnd]].transform);
        newBox.transform.localPosition = new Vector3(0, 0, 2f);
        newBox.GetComponent<Box>().SetIndex = _clearPlace[rnd];

    }
}