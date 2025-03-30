using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class NotificationBanner : MonoBehaviour
{
    [SerializeField] TMP_Text notificationText;

    private void Start()
    {
        _ScaleZero();
    }
    
    public void Show(string msg)
    {
        notificationText.text = msg;
        Sequence sequence = DOTween.Sequence()
            .Append(transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutQuad))
            .AppendInterval(0.9f) // 대기
            .Append(this.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad));
    }

    [ContextMenu("Scale One")]
    private void _ScaleOne()
    {
        transform.localScale = Vector3.one;
    }

    [ContextMenu("Scale Zero")]
    void _ScaleZero()
    {
        transform.localScale = Vector3.zero;
    }
}
