using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectManagement : MonoBehaviour
{
    private bool _isTouched = false;
    private bool _canJoin = false;
    private GameObject _secondObject = null;


    [SerializeField] private GameObject _particle;
    
    private Sprite _bedHoverSprite;
    private Sprite _bedSprite;

    private Vector3 _lastPos;
    private bool _returnElementOnPlace = false;
    private bool _canSwitch = false;
    private Transform _switchTo;
    private Vector3 newPosElement = new Vector3(0, 0, -2);


    private void Start()
    {
        _bedHoverSprite = GameManager.instance.BedHoverSprite;
        _bedSprite = GameManager.instance.BedSprite;
    }


    
    private void FixedUpdate()
    {
        if (_returnElementOnPlace)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _lastPos, .4f);

            if (transform.localPosition == _lastPos)
            {
                _lastPos = Vector3.zero;
                _returnElementOnPlace = false;
            }
        }

        if (_isTouched)
        {
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;
            transform.position = Vector3.Lerp(transform.position, targetPos, .2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Box")
        {
            return;
        }
        
        if (col.tag == "Bed")
        {
            var place = col.transform.GetChild(0);

            if (place.childCount > 0)
            {
                if (place.GetChild(0).tag == "Box")
                {
                    return;
                }
            }
            
            if (!_isTouched)
            {
                return;
            }
            
            _canSwitch = true;
            col.GetComponent<Image>().sprite = _bedHoverSprite;
            _switchTo = col.transform;
        }

        if (col.tag == "Element")
        {
            if (col.tag == "Box")
            {
                return;
            }
            
            if (col.gameObject.GetComponent<InfoObject>().GetLevel == gameObject.GetComponent<InfoObject>().GetLevel)
            {
                _canSwitch = false;
                _secondObject = col.gameObject;
                _canJoin = true;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Bed")
        {
            if (other.transform == _switchTo)
            {
                _switchTo = null;
                other.GetComponent<Image>().sprite = _bedSprite;
            }
        }


        if (other.tag == "Element")
        {
            // Debug.LogError("Триггер вышел из зоны");

            if (other.gameObject.GetComponent<InfoObject>().GetLevel ==
                gameObject.GetComponent<InfoObject>().GetLevel && _isTouched)
            {
                _secondObject = null;
                _canJoin = false;
            }
        }
    }

    private void OnMouseDown()
    {
        _lastPos = transform.localPosition;
        _isTouched = true;
    }

    private void OnMouseUp()
    {
        _isTouched = false;
        
        Debug.Log("switchTo: " + _switchTo);
        
        if (_canJoin)
        {
            if (GameManager.instance.Objects.Count > gameObject.GetComponent<InfoObject>().GetLevel + 1)
            {
                GameObject newObject = GameManager.instance.Objects[gameObject.GetComponent<InfoObject>().GetLevel + 1];
                // newObject.transform.localScale = new Vector3(9f, 9f, 9f);
                var newElement = Instantiate(newObject, Vector3.zero, Quaternion.identity, _secondObject.transform.parent.transform);
                newElement.transform.localPosition = newPosElement;
            }

            Destroy(_secondObject);
            
            var newParticle = Instantiate(_particle, transform.position, quaternion.identity);
            newParticle.GetComponent<JoinParticle>().PlayParticle();

            GameManager.instance.SpawnBedsClear.ClearBeds();
            
            GameManager.instance.ElementsManager.CheckElements(GetComponent<InfoObject>().GetLevel);
            
            Destroy(gameObject);
        }

        if (_canSwitch)
        {
            if (_switchTo != null)
            {
                if (_switchTo.GetChild(0).transform.childCount > 0)
                {
                    var secondElement = _switchTo.GetChild(0).GetChild(0);
                    secondElement.parent = transform.parent;
                    secondElement.localPosition = newPosElement;

                    transform.parent = _switchTo.GetChild(0);
                    transform.localPosition = newPosElement;
                }
                else
                {
                    transform.parent = _switchTo.GetChild(0);
                    transform.localPosition = newPosElement;
                }
                
            }
        }
        
        _canSwitch = false;
        
        _isTouched = false;
        
        // Debug.LogError("canJoin: " + _canJoin);

        if (_canJoin)
        {
            
            GameManager.instance.ElementsManager.CheckElements(GetComponent<InfoObject>().GetLevel);
        }
        else
        {
            GameManager.instance.ElementsManager.CheckElements();
        }

        _canJoin = false;
        

        GameManager.instance.SpawnBedsClear.ClearBeds();
        _returnElementOnPlace = true;
    }
}