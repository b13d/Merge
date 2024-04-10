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

    private void Start()
    {
        DefaultValueAudio();
    }

    private void DefaultValueAudio()
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
    }


    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            DefaultValueAudio();

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

        _particle.GetComponent<JoinParticle>().PlayParticle();
    }

    public void SpawnFirstElement()
    {
        Sequence firstElementSpawn = DOTween.Sequence();


        var newElement = Instantiate(_element, transform.position, Quaternion.identity, transform.parent);

        firstElementSpawn.Append(newElement.transform.DOLocalMove(new Vector3(0, .5f, -2f), .2f));
        firstElementSpawn.Append(newElement.transform.DOLocalMove(new Vector3(0, -.5f, -2f), .2f));
        firstElementSpawn.Append(newElement.transform.DOLocalMove(new Vector3(0, 0f, -2f), .2f));

        GameManager.instance.ElementsManager.CheckElements();

        Destroy(gameObject);
    }
}