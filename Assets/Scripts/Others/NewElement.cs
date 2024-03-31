using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewElement : MonoBehaviour
{
    [SerializeField] private Transform _transformText;
    [SerializeField] private Transform _imageNewElement;
    [SerializeField] private Transform _bgLight;

    void Start()
    {
        Sequence panelDO = DOTween.Sequence();
        panelDO.Append(GetComponent<Image>().DOColor(new Color32(0, 0, 0, 0), 0));
        panelDO.Append(GetComponent<Image>().DOColor(new Color32(0, 0, 0, 100), 1));

        Sequence textDO = DOTween.Sequence();
        textDO.SetLoops(-1, LoopType.Yoyo);
        textDO.Append(_transformText.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 1.5f));


        ImageNewElement();
    }

    private void ImageNewElement()
    {
        Sequence imageDO = DOTween.Sequence();
        imageDO.SetLoops(-1, LoopType.Yoyo);
        imageDO.Append(_imageNewElement.transform.DOLocalMove(new Vector3(0, 25, 0), 1.4f));

        Sequence bgDORotate = DOTween.Sequence();
        bgDORotate.SetLoops(-1, LoopType.Incremental);
        bgDORotate.Append(_bgLight.transform.DORotate(new Vector3(0, 0, 1), .1f));


        Sequence bgDOScale = DOTween.Sequence();
        bgDOScale.SetLoops(-1, LoopType.Yoyo);
        bgDOScale.Append(_bgLight.transform.DOScale(Vector3.one * 1.5f, 1.2f));
    }
}