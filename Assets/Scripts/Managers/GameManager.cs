using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using YG;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objects = new List<GameObject>();
    [SerializeField] private Sprite _bedSprite;
    [SerializeField] private Sprite _bedHoverSprite;
    [SerializeField] private SpawnBeds _spawnBeds;
    [SerializeField] private ElementManager _elementManager;
    [SerializeField] private CoinManager _coinManager;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private Prefabs _prefabs;

    private bool _isFull;
    private float amountOfMoney = 0;

    public static GameManager instance = null;

    private GameObject _lastObject;

    private float _seconds = 1f;

    private void FixedUpdate()
    {
        _seconds -= Time.deltaTime;

        if (_seconds < 0)
        {
            _seconds = 1f;

            // Switched();
        }
    }

    #region Properties

    public Prefabs GetPrefabs
    {
        get { return _prefabs; }
    }

    public LevelManager GetLevelManager
    {
        get { return _levelManager; }
    }

    public CoinManager GetCoinManager
    {
        get { return _coinManager; }
    }

    public float AmountOfMoney
    {
        get { return amountOfMoney; }
        set { amountOfMoney = value; }
    }

    public bool GetFull
    {
        get { return _isFull; }
        set { _isFull = value; }
    }

    public SpawnBeds SpawnBedsClear
    {
        get { return _spawnBeds; }
    }

    public Sprite BedSprite
    {
        get { return _bedSprite; }
    }

    public Sprite BedHoverSprite
    {
        get { return _bedHoverSprite; }
    }

    public List<GameObject> Objects
    {
        get { return _objects; }
    }

    public GameObject LastObject
    {
        get { return _lastObject; }
        set { _lastObject = value; }
    }

    public ElementManager ElementsManager
    {
        get { return _elementManager; }
    }

    #endregion

    #region Methods

    public void SavePositionElement()
    {
        YandexGame.savesData.elements.Clear();

        List<GameObject> dictionaryElements = new List<GameObject>()
            { null, null, null, null, null, null, null, null, null };

        List<GameObject> _placeBeds = SpawnBedsClear.PlaceBeds;

        for (int i = 0; i < _placeBeds.Count; i++)
        {
            if (_placeBeds[i] != null)
            {
                if (_placeBeds[i].transform.parent.gameObject.activeSelf)
                {
                    if (_placeBeds[i].transform.childCount > 0)
                    {
                        if (_placeBeds[i].transform.GetChild(0).GetComponent<InfoObject>() != null)
                        {
                            dictionaryElements[i] =
                                GetPrefabs.GetElements[
                                    _placeBeds[i].transform.GetChild(0).GetComponent<InfoObject>().GetLevel];
                        }
                        else
                        {
                            dictionaryElements[i] = GetPrefabs.GetBox;
                        }
                    }
                }
            }
        }

        YandexGame.savesData._elementsList = dictionaryElements;


        // foreach (var value in dictionaryElements.Values)
        // {
        //     Debug.LogError(value);
        // }

        YandexGame.SaveProgress();
    }

    #endregion

    private void Awake()
    {
        // YandexGame.ResetSaveProgress();
        // YandexGame.SaveProgress();
        
        if (YandexGame.savesData._elementsList.Count > 0)
        {
            int i = 0;

            Debug.LogError("YandexGame.savesData.levelPlayer: " + YandexGame.savesData.levelPlayer);
            
            foreach (var value in YandexGame.savesData._elementsList)
            {
                if (value != null)
                {
                    value.transform.localPosition = new Vector3(0, 0, -2f);

                    Instantiate(value, _spawnBeds.PlaceBeds[i].transform, false);
                    
                    // Instantiate(newElement, transform.position, Quaternion.identity, );

                    _spawnBeds.SetPlaceBusy = i;
                }

                i++;
            }

            // _spawnBeds.PlaceBeds = YandexGame.savesData._elementsList;
        }
    }

    void Start()
    {
        Debug.Log("count elements: " + YandexGame.savesData._elementsList.Count);


        Time.timeScale = 4f;

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SaveProgressPlayer()
    {
        YandexGame.savesData.money = _coinManager.GetMoney;
        // YandexGame.savesData.incomeMoney = ElementsManager.GetIncome;
    }
}