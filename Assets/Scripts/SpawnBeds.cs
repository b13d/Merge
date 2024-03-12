using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBeds : MonoBehaviour
{
    [SerializeField] private List<GameObject> _placeBeds = new List<GameObject>();
    [SerializeField] private List<int> _placeBusy = new List<int>();
    [SerializeField] private List<GameObject> _elemets = new List<GameObject>();
    [SerializeField] private GameObject _placeElements;
    
    void Start()
    {
        InitialBeds();
    }

    void InitialBeds()
    {
        for (int i = 0; i < _placeBeds.Count; i++)
        {
            Transform transformPlaceBed = _placeBeds[i].transform;

            Debug.Log("index: " + i);
            Debug.Log("localpos: " + transformPlaceBed.localPosition);
            Debug.Log("pos: " + transformPlaceBed.position);
            
            Instantiate(_elemets[0], transformPlaceBed.position, Quaternion.identity, transformPlaceBed);
            _placeBusy[i] = 1;
        }
    }
}