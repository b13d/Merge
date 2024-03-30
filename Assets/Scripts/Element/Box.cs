using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Box : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _placeIndex;
    [SerializeField] private GameObject _element;
    
    [SerializeField] private AudioSource _boxDown;
    [SerializeField] private AudioSource _boxOpen;
    
    public int SetIndex
    {
        get { return _placeIndex;}
        set { _placeIndex = value; }
    }

    private void Start()
    {
        _boxDown.volume = GameManager.instance.GetVolumeAudio;
        _boxOpen.volume = GameManager.instance.GetVolumeAudio;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        _boxDown.volume = GameManager.instance.GetVolumeAudio;
        _boxOpen.volume = GameManager.instance.GetVolumeAudio;
        
        // GameManager.instance.SpawnBedsClear.ClearPlace(_placeIndex);
        GetComponent<Animator>().Play("openBox");
    }

    public void BoxDown()
    {
        _boxDown.Play();
    }

    public void BoxOpen()
    {
        _boxOpen.Play();
    }

    public void SpawnFirstElement()
    {
        var newElement = Instantiate(_element, transform.position, Quaternion.identity, transform.parent);
        newElement.transform.localPosition = new Vector3(0, 4.7f, -2f);

        GameManager.instance.ElementsManager.CheckElements();
        
        Destroy(gameObject);
    }
}
