using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YG;
using System.Linq;
using DG.Tweening;

public class ObjectManagement : MonoBehaviour
{
    private bool _isTouched = false;
    private bool _canJoin = false;
    private GameObject _secondObject = null;
    private Tween tween;


    [SerializeField] private AudioList _audioList;
    [SerializeField] private GameObject _particle;
    [SerializeField] private AudioMove _audioMove;

    private Sprite _bedHoverSprite;
    private Sprite _bedSprite;
    private GameObject _lastBed;

    private Vector3 _lastPos;
    private bool _returnElementOnPlace = false;
    private bool _canSwitch = false;
    private Transform _switchTo;
    private Vector3 _newPosElement = new Vector3(0, 4.7f, -2);
    private GameObject _secondDeletedElement;

    public int interpolationFramesCount = 90; // Number of frames to completely interpolate between the 2 positions
    int elapsedFrames = 0;

    private void Start()
    {
        _bedHoverSprite = GameManager.instance.BedHoverSprite;
        _bedSprite = GameManager.instance.BedSprite;
        _audioList = FindObjectOfType<AudioList>();
    }

    private void FixedUpdate()
    {
        if (_returnElementOnPlace)
        {
            if (transform.localPosition == _newPosElement)
            {
                Debug.Log("Равны, снимаю возврат элемента");

                _returnElementOnPlace = false;
                elapsedFrames = 0;
            }
        }

        if (_isTouched)
        {
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = -2f;

            // Debug.Log(targetPos);

            transform.DOMove(targetPos, .2f);
            // transform.position = Vector3.Lerp(transform.position, targetPos, .2f);
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

        if (col.CompareTag("Box"))
        {
            _switchTo = null;
            _canSwitch = false;

            return;
        }

        if (col.CompareTag("Bed") && !col.GetComponent<Bed>().GetIsCloseBed)
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


            if (place.childCount > 0)
            {
                if (place.GetChild(0).CompareTag("Element"))
                {
                    Debug.Log("Нашел элемент!!");


                    if (place.GetChild(0).gameObject.GetComponent<InfoObject>().GetLevel ==
                        gameObject.GetComponent<InfoObject>().GetLevel)
                    {
                        _canSwitch = false;
                        _secondObject = place.GetChild(0).gameObject;
                        _canJoin = true;
                        _switchTo = place.GetChild(0);
                    }
                    else
                    {
                        Debug.Log("Смена местами у элементов");

                        _canSwitch = true;
                        _secondObject = place.GetChild(0).gameObject;
                        _switchTo = place.GetChild(0);
                        _canJoin = false;
                    }
                }
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other);

        if (other.tag == "Bed" && !other.GetComponent<Bed>().GetIsCloseBed)
        {
            other.GetComponent<Image>().sprite = _bedSprite;
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

        if (other.transform == _switchTo)
        {
            _switchTo = null;
            _canSwitch = false;
        }
    }

    private void OnMouseDown()
    {
        GameManager.instance.GetCurrentLevelElementTaked = GetComponent<InfoObject>().GetLevel;
        GameManager.instance.GetHighlighting.MatchSearch(gameObject);

        elapsedFrames = 0;
        _returnElementOnPlace = false;
        _lastPos = transform.localPosition;
        _isTouched = true;
    }

    private void OnMouseUp()
    {
        GameManager.instance.GetCurrentLevelElementTaked = 0;
        GameManager.instance.GetHighlighting.Reset();


        if (_isTouched)
        {
            GameManager.instance.GetBox.StopSpawn();
        }

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

        tween = transform.DOLocalMove(_newPosElement, .5f);

        GameManager.instance.SavePositionElement();
    }

    void Switch()
    {
        Debug.Log($"_switchTO {_switchTo}");

        if (_switchTo != null)
        {
            if (_switchTo.GetChild(0).transform.childCount > 0)
            {
                var newAudio = _audioMove;
                newAudio.GetComponent<AudioMove>().Audio = _audioList.AudioClips[0];
                Instantiate(newAudio, transform.position, Quaternion.identity, _audioList.transform);

                var secondElement = _switchTo.GetChild(0).GetChild(0);
                secondElement.parent = transform.parent;

                // _secondObject.transform.DOKill();
                // if (tween != null)
                // {
                //     tween.Complete();
                // }

                tween = secondElement.DOLocalMove(_newPosElement, .5f);

                transform.parent = _switchTo.GetChild(0);
                transform.localPosition = _newPosElement;
            }
            else if (_switchTo.gameObject.tag == "Element")
            {
                var newAudio = _audioMove;
                newAudio.GetComponent<AudioMove>().Audio = _audioList.AudioClips[0];
                Instantiate(newAudio, transform.position, Quaternion.identity, _audioList.transform);

                var oldParent = _switchTo.parent;

                var secondElement = _switchTo;
                secondElement.parent = transform.parent;
                // _secondObject.transform.DOKill();

                // if (tween != null)
                // {
                //     tween.Complete();
                // }

                tween = secondElement.DOLocalMove(_newPosElement, .5f);

                transform.parent = oldParent;
                transform.localPosition = _newPosElement;
            }
            else if (_switchTo.gameObject.tag == "Bed")
            {
                var newAudio = _audioMove;
                newAudio.GetComponent<AudioMove>().Audio = _audioList.AudioClips[1];
                Instantiate(newAudio, transform.position, Quaternion.identity, _audioList.transform);

                Debug.Log("Обычное перемещение");

                transform.parent = _switchTo.GetChild(0);
                transform.localPosition = _newPosElement;
            }
        }
    }

    void Join()
    {
        GameManager.instance.GetLevelManager.AddExp();

        var newAudio = _audioMove;
        newAudio.GetComponent<AudioMove>().Audio = _audioList.AudioClips[2];
        Instantiate(newAudio, transform.position, Quaternion.identity, _audioList.transform);

        if (GameManager.instance.GetLevelManager.GetLevelPlayer >= gameObject.GetComponent<InfoObject>().GetLevel)
        {
            if (GameManager.instance.Objects.Count > gameObject.GetComponent<InfoObject>().GetLevel + 1)
            {
                GameObject newObject = GameManager.instance.Objects[gameObject.GetComponent<InfoObject>().GetLevel + 1];
                var newElement = Instantiate(newObject, Vector3.zero, Quaternion.identity,
                    _secondObject.transform.parent.transform);
                newElement.transform.localPosition = _newPosElement;

                var newParticle = Instantiate(_particle, transform.position, Quaternion.identity);
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
        return;
    }
}