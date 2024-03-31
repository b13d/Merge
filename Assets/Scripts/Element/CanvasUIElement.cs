using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CanvasUIElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtIncomeElement;
    [SerializeField] private int _income;
    
    private void Start()
    {
        _txtIncomeElement.text = $"{_income} <sprite=\"coin\" name=\"coin\">";
        transform.DOLocalMove(new Vector3(transform.localPosition.x, transform.localPosition.y + .5f), 1f);
        _txtIncomeElement.DOColor(new Color(1, 1, 1, 0), 1f);

        Destroy(gameObject, 1.4f);
    }

    public int SetIncomeCurrent
    {
        set { _income = value; }
    }
}