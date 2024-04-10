using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header("Sprites")] [SerializeField] private Sprite _bedSprite;
    [SerializeField] private Sprite _bedHoverSprite;

    [Header("Managers")] [SerializeField] private ElementManager _elementManager;
    [SerializeField] private CoinManager _coinManager;
    [SerializeField] private LevelManager _levelManager;

    [Header("Audio")] [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _audioMoveElement;
    [SerializeField] private AudioSource _audioJoinElement;
    [SerializeField] private AudioSource _touchAudio;

    [Header("Sliders")] [SerializeField] private Slider _audioSlider;
    [SerializeField] private Slider _musicSlider;

    [Header("Others")] [SerializeField] private SpawnBeds _spawnBeds;
    [SerializeField] private Prefabs _prefabs;
    [SerializeField] private ClickBed _clickBed;
    [SerializeField] private AllocationOfBeds _allocationOfBeds;
    [SerializeField] private List<GameObject> _objects = new List<GameObject>();
    [SerializeField] private List<Sprite> _spritesElements = new List<Sprite>();
    [SerializeField] private GameObject _canvasNewElement;
    [SerializeField] private Image _imageNewElement;
    [SerializeField] private Achievements _achievements;

    private int _currentLevelElementFocus = 0;
    private bool _isFull;
    private int amountOfMoney = 0;

    public static GameManager instance = null;

    private GameObject _lastObject;

    private float _seconds = 1f;

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                var newTouch = Instantiate(_touchAudio, transform.position, Quaternion.identity);
                newTouch.Play();

                Destroy(newTouch.gameObject, 1);
            }
            else if (Input.touchCount > 0)
            {
                foreach (var touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        var newTouch = Instantiate(_touchAudio, transform.position, Quaternion.identity);
                        newTouch.Play();

                        Destroy(newTouch.gameObject, 1);
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        // RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);
        //
        // if (hit)
        // {
        //     Debug.Log(hit.collider);
        // }

        _seconds -= Time.deltaTime;

        if (_seconds < 0)
        {
            _seconds = 1f;
        }
    }


    #region Properties

    public Achievements GetAchievements
    {
        get { return _achievements; }
    }

    public List<Sprite> GetSpritesElement
    {
        get { return _spritesElements; }
    }

    public GameObject GetCanvasNewElement
    {
        get { return _canvasNewElement; }
    }

    public Image GetImageNewElement
    {
        get { return _imageNewElement; }
    }

    public int GetCurrentLevelElementTaked
    {
        set { _currentLevelElementFocus = value; }
        get { return _currentLevelElementFocus; }
    }

    public AllocationOfBeds GetHighlighting
    {
        get { return _allocationOfBeds; }
    }

    public ClickBed GetBox
    {
        get { return _clickBed; }
    }

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

    public int AmountOfMoney
    {
        get { return amountOfMoney; }
        set { amountOfMoney = value; }
    }

    public float GetVolumeAudio
    {
        get { return _audioSlider.value; }
    }

    public float GetVolumeMusic
    {
        get { return _musicSlider.value; }
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
        // YandexGame.savesData.elements.Clear();

        List<int> dictionaryElements = new List<int>()
            { 999, 999, 999, 999, 999, 999, 999, 999, 999 };

        List<GameObject> _placeBeds = SpawnBedsClear.PlaceBeds;

        for (int i = 0; i < _placeBeds.Count; i++)
        {
            if (_placeBeds[i] != null)
            {
                if (_placeBeds[i].transform.childCount > 0)
                {
                    if (_placeBeds[i].transform.GetChild(0).GetComponent<InfoObject>() != null)
                    {
                        dictionaryElements[i] =
                            _placeBeds[i].transform.GetChild(0).GetComponent<InfoObject>().GetLevel + 1;
                    }
                    else
                    {
                        if (_placeBeds[i].transform.GetChild(0).CompareTag("Box"))
                        {
                            dictionaryElements[i] = 0;
                        }
                        else if (_placeBeds[i].transform.GetChild(0).CompareTag("Gift"))
                        {
                            dictionaryElements[i] = -1;
                        }
                    }
                }
            }
        }

        YandexGame.savesData._elementsList = dictionaryElements;

        YandexGame.SaveProgress();
    }

    #endregion

    private void Awake()
    {
        // YandexGame.ResetSaveProgress();
        // YandexGame.SaveProgress();

        amountOfMoney = YandexGame.savesData.money;

        if (YandexGame.savesData._elementsList.Count > 0)
        {
            int i = 0;

            foreach (var value in YandexGame.savesData._elementsList)
            {
                if (value != 999)
                {
                    GameObject newElement;

                    if (value == 0)
                    {
                        newElement = GetPrefabs.GetBox.gameObject;
                    }
                    else if (value == -1)
                    {
                        newElement = GetPrefabs.GetGift.gameObject;
                    }
                    else
                    {
                        newElement = GetPrefabs.GetElements[value - 1].gameObject;
                    }

                    newElement.transform.localPosition = new Vector3(0, 0f, -2f);

                    Instantiate(newElement, _spawnBeds.PlaceBeds[i].transform, false);

                    _spawnBeds.SetPlaceBusy = i;
                }

                i++;
            }
        }
    }

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _audioMoveElement.volume = _audioSlider.value;
            _audioJoinElement.volume = _audioSlider.value;
            _music.volume = _musicSlider.value;

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ChangeSpeedGame(Toggle _toggle)
    {
        if (_toggle.isOn)
        {
            Time.timeScale = 5f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void ResetData()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}