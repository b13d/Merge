using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Box : MonoBehaviour
{
    [SerializeField] private int _placeIndex;
    [SerializeField] private GameObject _element;

    [SerializeField] private AudioSource _boxDown;
    [SerializeField] private AudioSource _boxOpen;
    [SerializeField] private ParticleSystem _particle;

    private float _volumeDefault = .1f;

    public int SetIndex
    {
        get { return _placeIndex; }
        set { _placeIndex = value; }
    }

    private void Start()
    {
        if (GameManager.instance.GetVolumeAudio == 0)
        {
            _boxDown.volume = 0;
            _boxOpen.volume = 0;
        }
        else
        {
            _boxDown.volume = _volumeDefault;
            _boxOpen.volume = _volumeDefault;
        }

        // _boxDown.volume = GameManager.instance.GetVolumeAudio;
        // _boxOpen.volume = GameManager.instance.GetVolumeAudio;
    }


    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Нажал на коробку");

            if (GameManager.instance.GetVolumeAudio == 0)
            {
                _boxDown.volume = 0;
                _boxOpen.volume = 0;
            }
            else
            {
                _boxDown.volume = _volumeDefault;
                _boxOpen.volume = _volumeDefault;
            }

            // _boxDown.volume = GameManager.instance.GetVolumeAudio;
            // _boxOpen.volume = GameManager.instance.GetVolumeAudio;

            // GameManager.instance.SpawnBedsClear.ClearPlace(_placeIndex);
            GetComponent<Animator>().Play("openBox");
        }
    }

    public void BoxDown()
    {
        _boxDown.Play();
    }

    public void BoxOpen()
    {
        _boxOpen.Play();

        // var newParticle = Instantiate(_particle, transform.position, Quaternion.identity);
        _particle.GetComponent<JoinParticle>().PlayParticle();
    }

    public void SpawnFirstElement()
    {
        Sequence firstElementSpawn = DOTween.Sequence();


        var newElement = Instantiate(_element, transform.position, Quaternion.identity, transform.parent);
        // newElement.transform.DOLocalMove(new Vector3(0, 7, -2f), .3f);        

        firstElementSpawn.Append(newElement.transform.DOLocalMove(new Vector3(0, .5f, -2f), .2f));
        firstElementSpawn.Append(newElement.transform.DOLocalMove(new Vector3(0, -.5f, -2f), .2f));
        firstElementSpawn.Append(newElement.transform.DOLocalMove(new Vector3(0, 0f, -2f), .2f));
        // newElement.transform.localPosition = new Vector3(0, 4.7f, -2f);

        GameManager.instance.ElementsManager.CheckElements();

        Destroy(gameObject);
    }
}