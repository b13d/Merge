using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objects = new List<GameObject>();
    [SerializeField] private Sprite _bedSprite;
    [SerializeField] private Sprite _bedHoverSprite;
    [SerializeField] private SpawnBeds _spawnBeds;
    [SerializeField] private ElementManager _elementManager;
    [SerializeField] private CoinManager _coinManager;
    
    private bool _isFull;
    private float amountOfMoney = 0;


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

    public static GameManager instance = null;

    private GameObject _lastObject;

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

    void Start()
    {
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
}