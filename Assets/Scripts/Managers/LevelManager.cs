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
    
    private int _count;
    private float _second = 1f;
    private int _level;
    private bool _allActiveBeds;

    private void Start()
    {
        _txtLevel.text = _level.ToString();
    }

    private void Update()
    {
        _second -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (_second < 0)
        {
            _second = 1f;
            
            _count += 1;

            _txtExp.text = $"{_count} / {_sliderExp.maxValue}";
            
            _sliderExp.value = _count;
            
            if (_count >= _sliderExp.maxValue)
            {
                _level += 1;
                _count = 0;
                _sliderExp.maxValue += 10;
                
                _txtLevel.text = _level.ToString();

                if (!_allActiveBeds)
                {
                    ActiveBed();
                }
            }
        }
    }

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
}
