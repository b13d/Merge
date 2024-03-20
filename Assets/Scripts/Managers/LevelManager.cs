using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Slider _sliderExp;
    [SerializeField] private TextMeshProUGUI _txtExp;
    [SerializeField] private TextMeshProUGUI _txtLevel;
    [SerializeField] private SpawnBeds _spawnBeds;

    private int _exp;
    private int _level;
    private bool _allActiveBeds;

    private void Start()
    {
        _txtLevel.text = _level.ToString();
        _txtExp.text = $"{_exp} / {_sliderExp.maxValue}";
    }

    #region Properties

    public int GetLevelPlayer
    {
        get { return _level; }
    }

    #endregion

    #region Methods
    
    public void ActiveBed()
    {
        for (int i = 0; i < _spawnBeds.PlaceBeds.Count; i++)
        {
            if (!_spawnBeds.PlaceBeds[i].transform.parent.gameObject.activeSelf)
            {
                _spawnBeds.PlaceBeds[i].transform.parent.gameObject.SetActive(true);

                _spawnBeds.CheckBedsOnVoid();

                return;
            }
        }
    }

    public void AddExp()
    {
        _exp += 1;
        _sliderExp.value = _exp;
        
        if (_exp >= _sliderExp.maxValue)
        {
            _exp = 0;
            _sliderExp.maxValue += 10;
            _sliderExp.value = 0;

            AddLevel();

            if (!_allActiveBeds)
            {
                ActiveBed();
            }
        }
        
        _txtExp.text = $"{_exp} / {_sliderExp.maxValue}";
    }

    public void AddLevel()
    {
        _level += 1;
        _txtLevel.text = _level.ToString();
    }
    

    #endregion
    

    
    

}
