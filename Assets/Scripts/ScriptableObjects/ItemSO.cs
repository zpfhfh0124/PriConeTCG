using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;     // ���ָ�
    public int ap;          // ���ݷ�
    public int hp;          // ü��
    public Sprite sprite;   // �̹��� ��������Ʈ
    public float prevalence;// ������
}

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObject/ItemSO")]
public class ItemSO : ScriptableObject
{
    public Item[] items;
}
