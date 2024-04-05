using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ElementMovement : MonoBehaviour
{
    private bool _isTouched = false;

    void FixedUpdate()
    {
        if (_isTouched)
        {
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = -2f;
            transform.parent.transform.DOMove(targetPos, .2f);
        }
    }

    public bool IsTouched
    {
        get { return _isTouched; }
        set { _isTouched = value; }
    }

    private void OnMouseDown()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        GameManager.instance.GetCurrentLevelElementTaked = transform.parent.GetComponent<InfoObject>().GetLevel;
        GameManager.instance.GetHighlighting.MatchSearch(transform.parent.gameObject);

        _isTouched = true;
        transform.parent.GetComponent<ObjectManagement>().MouseDown();
    }

    private void OnMouseUp()
    {
        GameManager.instance.GetCurrentLevelElementTaked = 0;
        GameManager.instance.GetHighlighting.Reset();

        _isTouched = false;
        transform.parent.GetComponent<ObjectManagement>().MouseUp();
    }
}