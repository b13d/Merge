using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    [SerializeField] private List<GameObject> _elements;
    [SerializeField] private GameObject _box;
    [SerializeField] private GameObject _gift;


    public GameObject GetGift
    {
        get { return _gift; }
    }

    public List<GameObject> GetElements
    {
        get { return _elements; }
    }

    public GameObject GetBox
    {
        get { return _box; }
    }
}