using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objects = new List<GameObject>();
    [SerializeField] private Sprite _bedSprite;
    [SerializeField] private Sprite _bedHoverSprite;


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