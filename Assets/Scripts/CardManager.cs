using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }
    private void Awake() => Instance = this;

    // 아이템 스크립터블 오브젝트 (카드 객체)
    [SerializeField] ItemSO itemSO;

    List<Item> itemBuffer;

    public Item PopItem() // 드로우
    {
        if (itemBuffer == null || itemBuffer.Count == 0) SetupItemBuffer();

        Item item = itemBuffer[0];
        itemBuffer.RemoveAt(0);

        Debug.Log($"드로우 카드 : {item.name}");

        return item;
    }

    void SetupItemBuffer() // 세팅 및 셔플
    {
        itemBuffer = new List<Item>();

        for( int i = 0; i < itemSO.items.Length; i++)
        {
            Item item = itemSO.items[i];
            for (int j = 0; j < item.prevalence; j++)
                itemBuffer.Add(item);
        }

        for(int i = 0; i < itemBuffer.Count; i++)
        {
            int rand = Random.Range(i, itemBuffer.Count);
            Item temp = itemBuffer[i];
            itemBuffer[i] = itemBuffer[rand];
            itemBuffer[rand] = temp;
        }
    }

    void Start()
    {
        SetupItemBuffer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            PopItem();
        }
    }
}
