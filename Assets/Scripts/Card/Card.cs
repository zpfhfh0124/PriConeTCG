using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer _card_background;
    [SerializeField] SpriteRenderer _character;
    [SerializeField] TextMeshPro _text_name;
    [SerializeField] TextMeshPro _text_atk;
    [SerializeField] TextMeshPro _text_hp;
    [SerializeField] Sprite _card_front;
    [SerializeField] Sprite _card_back;

    public Item Item;
    bool _isFront;

    // 카드 트랜스폼 조정
    public TransformSetter originTr;

    public void Setup(Item item, bool isFront)
    {
        Item = item;
        _isFront = isFront;

        // 앞면일 경우
        if (isFront)
        {
            _character.sprite = item.sprite;
            _text_name.text = item.name;
            _text_atk.text = item.ap.ToString();
            _text_hp.text = item.hp.ToString();
        }
        else
        {
            _card_background.sprite = _card_back;
            _text_name.text = string.Empty;
            _text_atk.text = string.Empty;
            _text_hp.text = string.Empty;
        }
    }

    // 트랜스폼 설정 (tween사용/비사용)
    public void SetTransform(TransformSetter setTr, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
        {
            transform.DOMove(setTr.pos, dotweenTime);
            transform.DORotateQuaternion(setTr.rot, dotweenTime);
            transform.DOScale(setTr.scale, dotweenTime);
        }
        else
        {
            transform.position = setTr.pos;
            transform.rotation = setTr.rot;
            transform.localScale = setTr.scale;
        }
    }
    
    // 카드에 마우스 포커싱
    void OnMouseOver()
    {
        if(_isFront) CardManager.Instance.CardMouseOver(this);
    }

    void OnMouseExit()
    {
        if(_isFront) CardManager.Instance.CardMouseExit(this);
    }

    public void MoveTransform()
    {
        
    }
}
