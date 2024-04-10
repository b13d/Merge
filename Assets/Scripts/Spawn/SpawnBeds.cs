using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Random = UnityEngine.Random;

public class SpawnBeds : MonoBehaviour
{
    [SerializeField] private List<GameObject> _placeBeds = new List<GameObject>();
    [SerializeField] private List<int> _placeBusy = new List<int>();
    [SerializeField] private GameObject _gift;
    [SerializeField] private Sprite _defaultSpriteBed;
    [SerializeField] private GameObject _box;

    [SerializeField] private Sprite _bedCloseSprite;

    private int _countElements = 0;

    private void Start()
    {
        InitialBeds();
    }

    public void InitialBeds()
    {
        for (int i = 0; i < _placeBeds.Count; i++)
        {
            if (i < YandexGame.savesData.countActiveBeds)
            {
                // делаю грядку активной

                _placeBeds[i].transform.parent.gameObject.layer = 0;
                _placeBeds[i].transform.parent.GetComponent<Bed>().GetIsCloseBed = false;
                _placeBeds[i].transform.parent.GetComponent<SpriteRenderer>().sprite = _defaultSpriteBed;
            }
            else
            {
                // делаю грядку НЕ активной

                _placeBeds[i].transform.parent.GetComponent<SpriteRenderer>().sprite = _bedCloseSprite;
                _placeBeds[i].transform.parent.GetComponent<Bed>().GetIsCloseBed = true;
                _placeBeds[i].transform.parent.gameObject.layer = 7;
            }
        }
    }

    #region Propetries

    public List<GameObject> PlaceBeds
    {
        get { return _placeBeds; }
    }

    public int SetPlaceBusy
    {
        set { _placeBusy[value] = 1; }
    }

    public List<int> PlaceBusy
    {
        get {return _placeBusy;}
    }

    #endregion
    
    

    public void ClearBeds()
    {
        for (int i = 0; i < _placeBeds.Count; i++)
        {
            if (_placeBeds[i].transform.parent.GetComponent<Bed>().GetIsCloseBed)
            {
                _placeBeds[i].transform.parent.GetComponent<SpriteRenderer>().sprite = _bedCloseSprite;
            }
            else
            {
                _placeBeds[i].transform.parent.GetComponent<SpriteRenderer>().sprite = _defaultSpriteBed;
            }
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
        _countElements = 0;

        List<int> _activeBeds = new List<int>();

        for (int j = 0; j < _placeBeds.Count; j++)
        {
            if (!_placeBeds[j].transform.parent.GetComponent<Bed>().GetIsCloseBed)
            {
                _activeBeds.Add(j);
            }
        }

        for (int i = 0; i < _activeBeds.Count; i++)
        {
            if (_placeBeds[_activeBeds[i]].transform.childCount == 0)
            {
                _placeBusy[i] = 0;

                if (GameManager.instance != null)
                {
                    GameManager.instance.GetFull = false;
                }
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
        List<int> clearPlace = new List<int>();
        List<int> activeBeds = new List<int>();


        CheckBedsOnVoid();

        for (int j = 0; j < _placeBeds.Count; j++)
        {
            if (!_placeBeds[j].transform.parent.GetComponent<Bed>().GetIsCloseBed)
            {
                activeBeds.Add(j);
            }
        }

        for (int i = 0; i < activeBeds.Count; i++)
        {
            if (_placeBusy[i] == 0)
            {
                clearPlace.Add(i);
            }
        }

        if (clearPlace.Count == 1)
        {
            GameManager.instance.GetFull = true;
        }

        if (clearPlace.Count == 0)
        {
            GameManager.instance.GetFull = true;

            return;
        }

        int rnd = Random.Range(0, clearPlace.Count);

        _placeBusy[clearPlace[rnd]] = 1;

        float chanceGift = Random.Range(0.0f, 1.0f);
        GameObject newBox;
        
        if (chanceGift < 0.2f)
        {
            newBox = Instantiate(_gift, transform.position, Quaternion.identity, _placeBeds[clearPlace[rnd]].transform);
        }
        else
        {
            newBox = Instantiate(_box, transform.position, Quaternion.identity, _placeBeds[clearPlace[rnd]].transform);
        }

        newBox.transform.localPosition = new Vector3(0, 0f, -2f);

        GameManager.instance.SavePositionElement();
    }
}