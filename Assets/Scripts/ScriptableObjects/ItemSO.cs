using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;     // 유닛명
    public int ap;          // 공격력
    public int hp;          // 체력
    public Sprite sprite;   // 이미지 스프라이트
    public float prevalence;// 출현률 상태치
    public float pvl_percentage; // 덱 셋팅시 가중치 확률로 계산된 퍼센테지 저장용
}

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObject/ItemSO")]
public class ItemSO : ScriptableObject
{
    public Item[] items;
}
