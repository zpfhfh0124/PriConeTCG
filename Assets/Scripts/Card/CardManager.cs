using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }
    private void Awake() => Instance = this;

    // 아이템 스크립터블 오브젝트 (카드 객체)
    [SerializeField] ItemSO _itemSO;
    // 카드 프리팹
    [SerializeField] GameObject _cardPrefab;

    // 카드 리스트
    [SerializeField] List<Card> _myCardList;
    [SerializeField] List<Card> _enemyCardList;

    // 카드 소환 위치
    [SerializeField] Transform _cardSpawnPoint;

    // 카드 패 위치
    [SerializeField] Transform _myCardLeft;
    [SerializeField] Transform _myCardRight;
    [SerializeField] Transform _enemyCardLeft;
    [SerializeField] Transform _enemyCardRight;

    List<Item> _itemBuffer;

    void Start()
    {
        // 테스트
        SetupItemBuffer();
        TurnManager.OnAddCard += AddCard;
    }

    private void Update()
    {

    }

    void OnDestroy()
    {
        TurnManager.OnAddCard -= AddCard;
    }

    public Item PopRandomItem() // 드로우
    {
        if (_itemBuffer == null || _itemBuffer.Count == 0) SetupItemBuffer();

        // 뽑은 카드 저장용 
        Item draw_item = new Item();

        // 확률 뽑기 
        // 각 itemSO의 출현률을 나타내는 Prevalence 값은 출현 상대치로 지정했음
        // 가중치 랜덤 뽑기 이용 -> 해당 확률을 가진 item이 복수인 경우 그 중에서 랜덤으로 나오게...
        int sum_prev = SumPrevalenceItems(_itemBuffer);
        int cur_prev = 0;
        float pop = Random.Range(0, sum_prev);
        Debug.Log($"뽑아낸 값 : {pop}");

        foreach (var item in _itemBuffer)
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
        _itemBuffer = _itemSO.items.ToList();
        
        // 덱 확률 정렬 후 역순 정렬 -> 내림차순 정렬 (확률 高 -> 低) -> Sort함수는 기본적으로 오름차순
        // 확률이 높은 것 부터 iterating 해서 시간 소요를 조금이라도 줄이고자 하기 위해서...
        _itemBuffer.Sort((p1, p2) => p1.prevalence.CompareTo(p2.prevalence));
        _itemBuffer.Reverse();
        
        for (int i = 0; i < _itemBuffer.Count; i++)
        {
            Debug.Log($"정렬된 카드덱 {i+1}번째 카드 {_itemBuffer[i].name}의 출현률 가중치 : {_itemBuffer[i].prevalence}");
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

    // 카드 추가
    void AddCard(bool isMine)
    {
        var card_obj = Instantiate(_cardPrefab, _cardSpawnPoint.position, Utilities.QI);
        Card card = card_obj.GetComponent<Card>();
        card.Setup(PopRandomItem(), isMine);

        var targetCardList = isMine ? _myCardList : _enemyCardList;
        targetCardList.Add( card );

        SetOriginOrder( isMine );
        SetCardAlignment( isMine );
    }

    // 카드 순서 정렬
    void SetOriginOrder(bool isMine)
    {
        int cnt = isMine ? _myCardList.Count : _enemyCardList.Count;
        for(int i = 0; i < cnt; i++)
        {
            Card card = isMine ? _myCardList[i] : _enemyCardList[i];
            card.GetComponent<Order>().SetOriginOrder(i);
        }
    }

    // 카드 위치 조정
    void SetCardAlignment(bool isMine)
    {
        List<TransformSetter> originCardTransforms = new List<TransformSetter>();
        if(isMine) originCardTransforms = RoundAlignment(_myCardLeft, _myCardRight, _myCardList.Count, 0.5f, Vector3.one * 15f);
        else originCardTransforms = RoundAlignment(_enemyCardLeft, _enemyCardRight, _enemyCardList.Count, -0.5f, Vector3.one * 15f);

        var targetCardList = isMine ? _myCardList : _enemyCardList;
        for(int i = 0; i < targetCardList.Count; i++)
        {
            var card = targetCardList[i];

            card.originTr = originCardTransforms[i];
            card.SetTransform(card.originTr, true, 0.5f);
        }
    }

    // 카드 각도 조정
    List<TransformSetter> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale)
    {
        float[] objLerps = new float[objCount];
        List<TransformSetter> results = new List<TransformSetter>(objCount);

        switch (objCount)
        {
            case 1: objLerps = new float[] { 0.5f };
                break; 
            case 2: objLerps = new float[] { 0.27f, 0.73f };
                break;
            case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f };
                break;
            default:
                float interval = 1f / (objCount - 1);
                for(int i = 0; i < objCount; i++) { objLerps[i] = interval * i; }
                break;
        }

        for(int i = 0; i < objCount; i++)
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            var targetRot = Utilities.QI;
            // 카드가 4장 이상일 때 회전 필요
            if (objCount >= 4)
            {
                // 원의 방정식 참고
                float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
                if (height >= 0) curve *= -1; // 반지름이 음수면 아래쪽에서 회전
                targetPos.y += curve;
                targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
            }
            results.Add(new TransformSetter(targetPos, targetRot, scale));
        }
        return results;
    }
}
