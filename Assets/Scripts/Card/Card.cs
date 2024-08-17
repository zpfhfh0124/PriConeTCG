using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer _card_background;
    [SerializeField] SpriteRenderer _character;
    [SerializeField] TextMeshPro _text_name;
    [SerializeField] TextMeshPro _text_atk;
    [SerializeField] TextMeshPro _text_hp;
    [SerializeField] Sprite _card_front;
    [SerializeField] Sprite _card_back;

    public Item _item;
    bool _isFront;

    // 카드 트랜스폼 조정
    public TransformSetter _originTr;

    public void Setup(Item item, bool isFront)
    {
        _item = item;
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

    public void TweeningTransform(TransformSetter setTr, bool useDotween, float dotweenTime = 0)
    {   

    }
}
