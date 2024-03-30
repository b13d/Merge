using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Slider _sliderExp;
    [SerializeField] private TextMeshProUGUI _txtExp;
    [SerializeField] private TextMeshProUGUI _txtLevel;
    [SerializeField] private SpawnBeds _spawnBeds;
    [SerializeField] private GameObject _canvasNewElement;
    [SerializeField] private List<Sprite> _spritesElement;
    [SerializeField] private Image _imageNewElement;
    
    private int _exp;
    private int _level;
    private int _countActiveBeds = 3;
    private bool _allActiveBeds;


    private void Awake()
    {
        _level = YandexGame.savesData.levelPlayer;
        _exp = YandexGame.savesData.countExp;
        _sliderExp.maxValue = YandexGame.savesData.maxValueSlider;
        _sliderExp.value = _exp;
        _countActiveBeds = YandexGame.savesData.countActiveBeds;

        _txtExp.text = $"{_exp} / {_sliderExp.maxValue}";
        _txtLevel.text = _level.ToString();

        for (int i = 0; i < _countActiveBeds; i++)
        {
            _spawnBeds.PlaceBeds[i].transform.parent.gameObject.SetActive(true);
        }
        
        _spawnBeds.CheckBedsOnVoid();
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
            if (_spawnBeds.PlaceBeds[i].transform.parent.GetComponent<Bed>().GetIsCloseBed)
            {
                _spawnBeds.PlaceBeds[i].transform.parent.GetComponent<Bed>().GetIsCloseBed = false;

                _spawnBeds.PlaceBeds[i].transform.parent.gameObject.layer = 0;

                _spawnBeds.PlaceBeds[i].transform.parent.GetComponent<Image>().sprite = GameManager.instance.BedSprite;
                
                _spawnBeds.CheckBedsOnVoid();

                _countActiveBeds += 1;
                
                YandexGame.savesData.countActiveBeds = _countActiveBeds;
                
                YandexGame.SaveProgress();

                return;
            }
        }
    }

    public int GetCountActiveBeds
    {
        get { return _countActiveBeds; }
    }

    public void AddExp()
    {
        _exp += 1;
        _sliderExp.value = _exp;

        if (_exp >= _sliderExp.maxValue)
        {
            _exp = 0;

            // тут вернуть надо будет
            // _sliderExp.maxValue += 10;
            _sliderExp.value = 0;

            AddLevel();

            if (!_allActiveBeds)
            {
                ActiveBed();
            }
        }

        _txtExp.text = $"{_exp} / {_sliderExp.maxValue}";

        YandexGame.savesData.countExp = _exp;
        YandexGame.savesData.maxValueSlider = _sliderExp.maxValue;

        YandexGame.SaveProgress();
    }

    public void AddLevel()
    {
        _level += 1;

        if (_spritesElement.Count > _level + 1)
        {
            // _imageNewElement.sprite = _spritesElement[_level + 1];
            // _canvasNewElement.SetActive(true);
        }


        
        _txtLevel.text = _level.ToString();

        YandexGame.savesData.levelPlayer = _level;

        YandexGame.SaveProgress();
    }

    #endregion
}