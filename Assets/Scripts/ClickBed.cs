using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickBed : MonoBehaviour, IPointerClickHandler
{
    private Image _image;
    private int _count = 10;
    private bool _stopClick;
    private float _oneSecond = 1f;
    
    [SerializeField] private TextMeshProUGUI _txtCount;

    private void Start()
    {
        _image = GetComponent<Image>();
        _txtCount.text = _count.ToString();
    }

    private void Update()
    {
        if (_stopClick || GameManager.instance.GetFull)
        {
            return;
        }

        if (_count == 0)
        {
            _stopClick = true;
            GameManager.instance.SpawnBedsClear.SpawnBox();
            StartCoroutine(Reset());
            return;
        }
        
        _image.fillAmount += Time.deltaTime / 8;
        
        _count = 10 - (int)(_image.fillAmount / 0.1);
        
        _txtCount.text = _count.ToString();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (_stopClick || GameManager.instance.GetFull)
        {
            return;
        }
        
        Debug.Log("Кликнул на подарок");

        _image.fillAmount += .1f;

        _count -= 1;

        _txtCount.text = _count.ToString();

        if (_count == 0)
        {
            GameManager.instance.SpawnBedsClear.SpawnBox();
            _stopClick = true;
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1f);

        _stopClick = false;
        _count = 10;
        _txtCount.text = _count.ToString();
        _image.fillAmount = 0;
    }
}
