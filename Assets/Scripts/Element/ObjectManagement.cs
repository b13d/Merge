using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YG;
using System.Linq;

public class ObjectManagement : MonoBehaviour
{
    private bool _isTouched = false;
    private bool _canJoin = false;
    private GameObject _secondObject = null;


    [SerializeField] private GameObject _particle;

    private Sprite _bedHoverSprite;
    private Sprite _bedSprite;
    private GameObject _lastBed;

    private Vector3 _lastPos;
    private bool _returnElementOnPlace = false;
    private bool _canSwitch = false;
    private Transform _switchTo;
    private Vector3 newPosElement = new Vector3(0, 0, -2);
    private GameObject _secondDeletedElement;

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
        if (!_isTouched)
        {
            // возможно тут надо расскоментировать
            
            // _canSwitch = false;
            // _switchTo = null;
            return;
        }
        
        Debug.Log($"Данный элемент был задет триггером: {col.gameObject} & isTouched {_isTouched}");
        
        if (col.tag == "Box")
        {
            _switchTo = null;
            _canSwitch = false;
            
            return;
        }

        if (col.tag == "Bed" && !col.GetComponent<Bed>().GetIsCloseBed)
        {
            if (_lastBed != null)
            {
                if (!_lastBed.GetComponent<Bed>().GetIsCloseBed)
                {
                    _lastBed.GetComponent<Image>().sprite = _bedSprite;
                }
            }
            
            var place = col.transform.GetChild(0);

            if (place.childCount > 0)
            {
                if (place.GetChild(0).tag == "Box")
                {
                    _canSwitch = false;
                    _switchTo = null;
                    return;
                }
            }



            col.GetComponent<Image>().sprite = _bedHoverSprite;
            
            _canSwitch = true;
            _switchTo = col.transform;
        } else if (col.tag == "Element")
        {
            if (col.gameObject.GetComponent<InfoObject>().GetLevel == gameObject.GetComponent<InfoObject>().GetLevel)
            {
                _canSwitch = false;
                _secondObject = col.gameObject;
                _canJoin = true;
                _switchTo = col.transform;
            }
            else
            {
                Debug.Log("Смена местами у элементов");
                
                _canSwitch = true;
                _secondObject = col.gameObject;
                _switchTo = col.transform;
                _canJoin = false;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Bed" && !other.GetComponent<Bed>().GetIsCloseBed)
        {
            other.GetComponent<Image>().sprite = _bedSprite;
            
            // if (other.transform == _switchTo)
            // {
            //
            // }
            
            // _switchTo = null;
            // _canSwitch = false; // под вопросом
            // return; 
        }


        if (other.tag == "Element")
        {
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
        if (_isTouched)
        {
            GameManager.instance.GetBox.StopSpawn();
        }
        
        // возможно нужно расскоментировать
        
        // if (!_isTouched)
        // {
        //     return;
        // }

        _isTouched = false;


        if (_canJoin)
        {
            Join();

            return;
        }


        if (_canSwitch)
        {
            Debug.Log("Смена!");
            
            Switch();
        }

        Reset();
    }

    private void Reset()
    {
        _canSwitch = false;
        _isTouched = false;

        if (!_canJoin)
        {
            GameManager.instance.ElementsManager.CheckElements();
        }

        _canJoin = false;
        
        GameManager.instance.SpawnBedsClear.ClearBeds();

        _returnElementOnPlace = true;
        
        GameManager.instance.SavePositionElement();
    }

    void Switch()
    {
        Debug.Log($"_switchTO {_switchTo}");
        
        if (_switchTo != null)
        {
            if (_switchTo.GetChild(0).transform.childCount > 0)
            {
                Debug.Log("Тута");
                
                var secondElement = _switchTo.GetChild(0).GetChild(0);
                secondElement.parent = transform.parent;
                secondElement.localPosition = newPosElement;

                transform.parent = _switchTo.GetChild(0);
                transform.localPosition = newPosElement;
            }
            else if (_switchTo.gameObject.tag == "Element")
            {
                Debug.Log("Значит Тута");
                
                // ОЧЕНЬ ВЕРОЯТНО ЧТО ТУТ Я НАМУДРИЛ!
                var oldParent = _switchTo.parent;
                
                Debug.Log($"До {_switchTo.parent}");
                
                var secondElement = _switchTo;
                secondElement.parent = transform.parent;
                secondElement.localPosition = newPosElement;

                Debug.Log($"После {_switchTo.parent}");
                
                transform.parent = oldParent;
                transform.localPosition = newPosElement;
            }
            else if (_switchTo.gameObject.tag == "Bed")
            {
                Debug.Log("Обычное перемещение");
                
                transform.parent = _switchTo.GetChild(0);
                transform.localPosition = newPosElement;
            }
        }
    }

    void Join()
    {
        GameManager.instance.GetLevelManager.AddExp();

        // тестовый код, на проверку последнего элемента зависящего от текущего уровня игрока
        
        if (GameManager.instance.GetLevelManager.GetLevelPlayer >= gameObject.GetComponent<InfoObject>().GetLevel)
        {
            if (GameManager.instance.Objects.Count > gameObject.GetComponent<InfoObject>().GetLevel + 1)
            {
                GameObject newObject = GameManager.instance.Objects[gameObject.GetComponent<InfoObject>().GetLevel + 1];
                // newObject.transform.localScale = new Vector3(9f, 9f, 9f);
                var newElement = Instantiate(newObject, Vector3.zero, Quaternion.identity,
                    _secondObject.transform.parent.transform);
                newElement.transform.localPosition = newPosElement;

                var newParticle = Instantiate(_particle, transform.position, quaternion.identity);
                newParticle.GetComponent<JoinParticle>().PlayParticle();
            }
        }
        else
        {
            Debug.LogError("Последний элемент!!");
        }

        GameManager.instance.SpawnBedsClear.ClearBeds();
        GameManager.instance.ElementsManager.CheckElements(GetComponent<InfoObject>().GetLevel, gameObject,
            _secondObject, true);


        
        Destroy(_secondObject);
        Destroy(gameObject);
        // Reset();
        return;
    }
}