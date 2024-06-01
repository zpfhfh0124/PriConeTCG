using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer _card_background;
    [SerializeField] SpriteRenderer _character;
    [SerializeField] TextMeshProUGUI _text_name;
    [SerializeField] TextMeshProUGUI _text_atk;
    [SerializeField] TextMeshProUGUI _text_hp;
    [SerializeField] TextMeshProUGUI _card_front;
    [SerializeField] TextMeshProUGUI _card_back;

    public Item item;
    bool isFront;


}
