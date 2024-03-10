using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        // 더 간단한 세팅법
        itemBuffer = itemSO.items.ToList();
        float sum_pvl = 0;
        // 각 itemSO의 출현률을 나타내는 Prevalence 값은 출현 상대치로 지정했음
        // 가중치 랜덤 뽑기 이용 -> 해당 확률을 가진 item이 복수인 경우 그 중에서 랜덤으로 나오게...
        sum_pvl = (from item in itemBuffer
                   select item.prevalence).Sum();

        Debug.Log($"덱 전체 카드의 출현률 수치 합 : {sum_pvl}");

        foreach (var item in itemBuffer)
        {
            item.pvl_percentage = (item.prevalence / sum_pvl);
            Debug.Log($"카드 {item.name}의 출현률 : {item.pvl_percentage}%");
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
