using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Box : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _placeIndex;
    [SerializeField] private GameObject _element;
    
    public int SetIndex
    {
        get { return _placeIndex;}
        set { _placeIndex = value; }
    }
    
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // Debug.Log("Нажимаю на подарочек чтобы сломать");
        
        // GameManager.instance.SpawnBedsClear.ClearPlace(_placeIndex);

        var newElement = Instantiate(_element, transform.position, Quaternion.identity, transform.parent);
        newElement.transform.localPosition = new Vector3(0, 0, -2f);

        GameManager.instance.ElementsManager.CheckElements();
        
        Destroy(gameObject);
    }
}
