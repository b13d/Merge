using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DotweenTest : MonoBehaviour
{
    [SerializeField] private Transform _transformText;
    [SerializeField] private Transform _imageNewElement;

    void Start()
    {
        Sequence panelDO = DOTween.Sequence();
        // s.SetLoops(-1, LoopType.Yoyo);
        // s.Append(transform.DOLocalMoveY(transform.localPosition.y + 100, 1.5f));
        panelDO.Append(GetComponent<Image>().DOColor(new Color32(0, 0, 0, 0), 0));
        panelDO.Append(GetComponent<Image>().DOColor(new Color32(0, 0, 0, 100), 1));

        Sequence textDO = DOTween.Sequence();
        textDO.SetLoops(-1, LoopType.Yoyo);
        textDO.Append(_transformText.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 1.5f));


        ImageNewElement();
        // imageDO.Append(_imageNewElement.transform.DOLocalRotate(new Vector3(0, 0, 45), 1));
        
        Debug.Log("localScale: " + _imageNewElement.localScale);
    }

    private void ImageNewElement()
    {
        Sequence imageDO = DOTween.Sequence();
        imageDO.SetLoops(-1, LoopType.Yoyo);
        imageDO.Append(_imageNewElement.transform.DOLocalRotate(new Vector3(0, 0, -45), 2f));
        imageDO.Append(_imageNewElement.DOScale( new Vector3(1.6f, 1.6f, 1.6f), 1));
        imageDO.Append(_imageNewElement.transform.DOLocalRotate(new Vector3(0, 0, 45), 2f));
    } 
}