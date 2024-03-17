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

    void Start()
    {
        SetupItemBuffer();
        PopRandomItem();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            PopRandomItem();
        }
    }

    public Item PopRandomItem() // 드로우
    {
        if (itemBuffer == null || itemBuffer.Count == 0) SetupItemBuffer();

        // 뽑은 카드 저장용 
        Item draw_item = new Item();

        // 확률 뽑기 
        // 각 itemSO의 출현률을 나타내는 Prevalence 값은 출현 상대치로 지정했음
        // 가중치 랜덤 뽑기 이용 -> 해당 확률을 가진 item이 복수인 경우 그 중에서 랜덤으로 나오게...
        int sum_prev = SumPrevalenceItems(itemBuffer);
        int cur_prev = 0;
        float pop = Random.Range(0, sum_prev);
        Debug.Log($"뽑아낸 값 : {pop}");

        foreach (var item in itemBuffer)
        {
            if (pop >= cur_prev && pop < cur_prev + item.prevalence)
            {
                draw_item = item;
                Debug.Log($"가중치 범위 : {cur_prev + 1} ~ {cur_prev + item.prevalence}");
                break;
            } 
            else cur_prev += item.prevalence;
        }

        Debug.Log($"드로우 카드 : {draw_item.name} | 출현률 가중치 : {draw_item.prevalence}");

        return draw_item;
    }

    // 세팅 및 셔플
    void SetupItemBuffer() 
    {
        // 더 간단한 세팅법
        itemBuffer = itemSO.items.ToList();
        
        // 덱 확률 정렬 후 역순 정렬 -> 내림차순 정렬 (확률 高 -> 低) -> Sort함수는 기본적으로 오름차순
        // 확률이 높은 것 부터 iterating 해서 시간 소요를 조금이라도 줄이고자 하기 위해서...
        itemBuffer.Sort((p1, p2) => p1.prevalence.CompareTo(p2.prevalence));
        itemBuffer.Reverse();
        
        for (int i = 0; i < itemBuffer.Count; i++)
        {
            Debug.Log($"정렬된 카드덱 {i+1}번째 카드 {itemBuffer[i].name}의 출현률 가중치 : {itemBuffer[i].prevalence}");
        }
    }

    int SumPrevalenceItems(List<Item> itemBuffer)
    {
        int sum_pvl = 0;
        sum_pvl = (from item in itemBuffer
                   select item.prevalence).Sum();
        Debug.Log($"덱 전체 카드의 출현률 수치 합 : {sum_pvl}");
        
        return sum_pvl;
    }
}
