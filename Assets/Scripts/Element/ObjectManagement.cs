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
    private bool _canJoin = false;
    private GameObject _secondObject = null;
    private Tween tween;


    [SerializeField] private AudioList _audioList;
    [SerializeField] private GameObject _particle;
    [SerializeField] private AudioMove _audioMove;
    [SerializeField] private AudioMove _audioJoin;
    [SerializeField] private ElementMovement _elementMovement;


    private Sprite _bedHoverSprite;
    private Sprite _bedSprite;
    private GameObject _lastBed;

    private Vector3 _lastPos;
    private bool _returnElementOnPlace = false;
    private bool _canSwitch = false;
    private Transform _switchTo;
    private Vector3 _newPosElement = new Vector3(0, 0f, -2);
    private GameObject _secondDeletedElement;

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
                _returnElementOnPlace = false;
            }
        }
    }

    void ResetTarget()
    {
        _switchTo = null;
        _canSwitch = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!_elementMovement.IsTouched)
        {
            return;
        }

        if (col.CompareTag("Box"))
        {
            ResetTarget();
            return;
        }

        if (col.CompareTag("Element"))
        {
            var place = col.transform.parent;
            ElementTarget(place);
        }

        if (col.CompareTag("Bed") && !col.GetComponent<Bed>().GetIsCloseBed)
        {
            if (_lastBed != null)
            {
                if (!_lastBed.GetComponent<Bed>().GetIsCloseBed)
                {
                    _lastBed.GetComponent<SpriteRenderer>().sprite = _bedSprite;
                }
            }

            var place = col.transform.GetChild(0);

            if (place.childCount > 0)
            {
                if (place.GetChild(0).CompareTag("Box"))
                {
                    ResetTarget();
                    return;
                }
            }

            col.GetComponent<SpriteRenderer>().sprite = _bedHoverSprite;

            _canSwitch = true;
            _switchTo = col.transform;


            if (place.childCount > 0)
            {
                ElementTarget(place);
            }
        }
    }

    void ElementTargetProperties(Transform switchTo, GameObject secondObject, bool canSwitch, bool canJoin)
    {
        _secondObject = secondObject;
        _switchTo = switchTo;
        _canSwitch = canSwitch;
        _canJoin = canJoin;
    }

    void ElementTarget(Transform place)
    {
        if (place.GetChild(0).CompareTag("Element"))
        {
            var element = place.GetChild(0);

            if (element.gameObject == gameObject)
            {
                return;
            }

            if (element.gameObject.GetComponent<InfoObject>().GetLevel ==
                GetComponent<InfoObject>().GetLevel)
            {
                ElementTargetProperties(element, element.gameObject, false, true);
            }
            else
            {
                ElementTargetProperties(element, element.gameObject, true, false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bed") && !other.GetComponent<Bed>().GetIsCloseBed)
        {
            var bed = gameObject.transform.parent.transform.parent;
            if (bed.gameObject != other.gameObject)
            {
                _canJoin = false;
                _canSwitch = false;
            }

            _secondObject = null;
            other.GetComponent<SpriteRenderer>().sprite = _bedSprite;
        }


        if (other.CompareTag("Element"))
        {
            if (other.gameObject.GetComponent<InfoObject>().GetLevel ==
                GetComponent<InfoObject>().GetLevel && _elementMovement.IsTouched)
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

    public void MouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            _returnElementOnPlace = false;
            _lastPos = transform.localPosition;
            _elementMovement.IsTouched = true;
        }
    }

    public void MouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            _elementMovement.IsTouched = false;

            if (_canJoin)
            {
                Join();

                return;
            }


            if (_canSwitch)
            {
                Switch();
            }

            Reset();
        }
    }


    private void Reset()
    {
        _canSwitch = false;
        _elementMovement.IsTouched = false;

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

    void SetAudio(AudioClip audio)
    {
        var newAudio = _audioMove;
        newAudio.GetComponent<AudioMove>().Audio = audio;
        Instantiate(newAudio, transform.position, Quaternion.identity, _audioList.transform);
    }

    void MoveElement(Transform secondElement)
    {
        secondElement.parent = transform.parent;

        secondElement.DOKill();
        tween = secondElement.DOLocalMove(_newPosElement, .5f);
    }

    void Switch()
    {
        if (_switchTo != null)
        {
            if (_switchTo.GetChild(0).transform.childCount > 0)
            {
                SetAudio(_audioList.GetMoveElementsAudio());

                var secondElement = _switchTo.GetChild(0).GetChild(0);
                MoveElement(secondElement);

                transform.parent = _switchTo.GetChild(0);
            }
            else if (_switchTo.gameObject.CompareTag("Element"))
            {
                SetAudio(_audioList.GetSwitchedElementsAudio());

                var oldParent = _switchTo.parent;

                var secondElement = _switchTo;
                MoveElement(secondElement);

                transform.parent = oldParent;
            }
            else if (_switchTo.gameObject.CompareTag("Bed"))
            {
                SetAudio(_audioList.GetMoveElementsAudio());

                transform.parent = _switchTo.GetChild(0);
            }

            transform.localPosition = _newPosElement;
        }
    }

    void Join()
    {
        GameManager.instance.GetLevelManager.AddExp();

        var newAudio = _audioMove;
        float percentSpeed = _audioList.GetCountSpeed * 10f / 100f;

        newAudio.GetComponent<AudioSource>().pitch = 1 + percentSpeed;
        newAudio.GetComponent<AudioMove>().Audio = _audioList.GetJoinElementsAudio();
        Instantiate(newAudio, transform.position, Quaternion.identity, _audioList.transform);

        var nextLevel = gameObject.GetComponent<InfoObject>().GetLevel + 1;

        if (GameManager.instance.Objects.Count > nextLevel &&
            nextLevel != 9)
        {
            GameObject newObject = GameManager.instance.Objects[nextLevel];
            var newElement = Instantiate(newObject, Vector3.zero, Quaternion.identity,
                _secondObject.transform.parent.transform);
            newElement.transform.localPosition = _newPosElement;

            if (YandexGame.savesData.lastNewElementLevel < nextLevel)
            {
                GameManager.instance.GetImageNewElement.sprite =
                    GameManager.instance.GetSpritesElement[nextLevel];

                newAudio = _audioMove;
                newAudio.GetComponent<AudioMove>().Audio = _audioList.GetOtherSound(0);
                Instantiate(newAudio, transform.position, Quaternion.identity, _audioList.transform);

                GameManager.instance.GetCanvasNewElement.SetActive(true);
                GameManager.instance.GetAchievements.ActiveAchievements(nextLevel);

                YandexGame.savesData.lastNewElementLevel = nextLevel;

                YandexGame.SaveProgress();
            }


            var newParticle = Instantiate(_particle, transform.position, Quaternion.identity);
            newParticle.GetComponent<JoinParticle>().PlayParticle();
        }


        GameManager.instance.SpawnBedsClear.ClearBeds();
        GameManager.instance.ElementsManager.CheckElements(GetComponent<InfoObject>().GetLevel, gameObject,
            _secondObject, true);
        
        Destroy(_secondObject);
        Destroy(gameObject);
    }
}