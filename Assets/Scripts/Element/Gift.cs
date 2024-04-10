using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using YG;
using Random = UnityEngine.Random;

public class Gift : MonoBehaviour
{
    [SerializeField] private List<GameObject> _elements = new List<GameObject>();
    [SerializeField] private List<int> _levelElementsOnGrid = new List<int>();
    [SerializeField] private AudioSource _giftDown;
    [SerializeField] private AudioSource _giftOpen;
    [SerializeField] private ParticleSystem _particle;


    private readonly int _maxLevelElement = 3;
    private readonly float _volumeDefault = .1f;

    private void Start()
    {
        if (GameManager.instance.GetVolumeAudio == 0)
        {
            _giftDown.volume = 0;
            _giftOpen.volume = 0;
        }
        else
        {
            _giftDown.volume = _volumeDefault;
            _giftOpen.volume = _volumeDefault;
        }
    }

    private void OnMouseDown()
    {
        GetComponent<Animator>().Play("giftOpen");
        _giftOpen.Play();
        _particle.GetComponent<JoinParticle>().PlayParticle();
    }

    private void CheckElementOnGrid()
    {
        var placeBusy = GameManager.instance.SpawnBedsClear.PlaceBusy;
        var places = GameManager.instance.SpawnBedsClear.PlaceBeds;
        var count = placeBusy.Count;
        int index = 0;

        _levelElementsOnGrid.Clear();

        foreach (var place in placeBusy)
        {
            if (place == 1)
            {
                if (places[index].transform.GetChild(0).GetComponent<InfoObject>())
                {
                    Debug.Log("level: " + places[index].transform.GetChild(0).GetComponent<InfoObject>().GetLevel);

                    var level = places[index].transform.GetChild(0).GetComponent<InfoObject>().GetLevel;

                    if (!_levelElementsOnGrid.Contains(level))
                    {
                        if (level < _maxLevelElement)
                        {
                            _levelElementsOnGrid.Add(level);
                        }
                    }
                }
            }

            index++;
        }

        List<int> rndIDElements = new List<int>();

        rndIDElements.Add(0);
        
        for (int i = 1; i < 3; i++)
        {
            if (_levelElementsOnGrid.Contains(i) && YandexGame.savesData.lastAchievementID > i)
            {
                rndIDElements.Add(i);
            }
        }

        var rndID = rndIDElements[Random.Range(0, rndIDElements.Count)];

        SpawnElement(rndID);
    }

    void SpawnElement(int levelElement)
    {
        var element = GameManager.instance.GetPrefabs.GetElements[levelElement];

        Sequence firstElementSpawn = DOTween.Sequence();

        var newElement = Instantiate(element, transform.position, Quaternion.identity, transform.parent);

        firstElementSpawn.Append(newElement.transform.DOLocalMove(new Vector3(0, .5f, -2f), .2f));
        firstElementSpawn.Append(newElement.transform.DOLocalMove(new Vector3(0, -.5f, -2f), .2f));
        firstElementSpawn.Append(newElement.transform.DOLocalMove(new Vector3(0, 0f, -2f), .2f));

        GameManager.instance.ElementsManager.CheckElements();

        Destroy(gameObject);
    }


    public void BoxDown()
    {
        _giftDown.Play();
    }

    public void BoxOpen()
    {
        CheckElementOnGrid();
    }
}