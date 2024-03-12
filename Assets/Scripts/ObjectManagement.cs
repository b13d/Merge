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
        // if (transform.localScale.x < 1)
        // {
        //     transform.localScale += Vector3.one * Time.deltaTime;
        // }

        if (_isTouched)
        {
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;
            transform.position = Vector3.Lerp(transform.position, targetPos, .2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.tag == "Bed");
        Debug.Log("tag: " + col.tag);
        
        if (col.tag == "Bed")
        {
            _canSwitch = true;
            col.GetComponent<Image>().sprite = _bedHoverSprite;
            _switchTo = col.transform;
        }

        if (col.tag == "Element")
        {
            if (col.gameObject.GetComponent<InfoObject>().GetLevel == gameObject.GetComponent<InfoObject>().GetLevel)
            {
                _canSwitch = false;
                _secondObject = col.gameObject;
                _canJoin = true;
                // _outlining.SetActive(true);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log("other: " + other);

        if (other.tag == "Bed")
        {
            _switchTo = null;
            other.GetComponent<Image>().sprite = _bedSprite;
        }


        if (other.tag == "Element")
        {
            if (other.gameObject.GetComponent<InfoObject>().GetLevel == gameObject.GetComponent<InfoObject>().GetLevel)
            {
                _secondObject = null;
                _canJoin = false;
                // _outlining.SetActive(false);
            }
        }
    }

    private void OnMouseDown()
    {
        _lastPos = transform.localPosition;
        // Debug.Log("Нажал");

        if (GameManager.instance.LastObject != null)
        {
            // GameManager.instance.LastObject.GetComponent<Canvas>().sortingOrder = 0;
        }


        // gameObject.transform.GetChild(0).GetComponent<Canvas>().sortingOrder = 1;
        // gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;

        _isTouched = true;
    }

    private void OnMouseUp()
    {
        if (_canJoin)
        {
            if (GameManager.instance.Objects.Count > gameObject.GetComponent<InfoObject>().GetLevel + 1)
            {
                GameObject newObject = GameManager.instance.Objects[gameObject.GetComponent<InfoObject>().GetLevel + 1];
                // newObject.transform.localScale = new Vector3(9f, 9f, 9f);
                var newElement = Instantiate(newObject, Vector3.zero, Quaternion.identity, _secondObject.transform.parent.transform);
                newElement.transform.localPosition = Vector3.zero;
            }

            Destroy(_secondObject);
            
            var newParticle = Instantiate(_particle, transform.localPosition, quaternion.identity);
            newParticle.GetComponent<JoinParticle>().PlayParticle();

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
                    secondElement.localPosition = Vector3.zero;
                    
                    transform.parent = _switchTo.GetChild(0);
                    transform.localPosition = Vector3.zero;
                }
                else
                {
                    transform.parent = _switchTo.GetChild(0);
                    transform.localPosition = Vector3.zero;
                }
            }
        }
        
        _isTouched = false;

        if (GameManager.instance.LastObject != null)
        {
            // GameManager.instance.LastObject.GetComponent<Canvas>().sortingOrder = 0;
            // GameManager.instance.LastObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }


        _returnElementOnPlace = true;
        // GameManager.instance.LastObject = transform.GetChild(0).gameObject;
    }
}