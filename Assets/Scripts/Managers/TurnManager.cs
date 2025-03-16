using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// 턴 관리
public class TurnManager : MonoBehaviour
{
    static public TurnManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    enum TurnMode
    {
        Random,
        Mine,
        Other
    }

    [Header("Dev")]
    [SerializeField] [Tooltip("시작 턴 모드 지정")] 
    TurnMode turnMode;
    [SerializeField] [Tooltip("시작 카드 개수 결정")]
    private int startCardCount;
    [SerializeField] [Tooltip("카드 배분 배속")] 
    private bool fastMode;
    
    [Header("Properties")]
    public bool myTurn;
    // 게임 종료 -> isLoading을 true로 하면 카드와 엔티티 클릭 방지
    public bool isLoading;
    
    WaitForSeconds delay = new WaitForSeconds(0.5f);
    WaitForSeconds delay2 = new WaitForSeconds(0.7f);

    public static Action<bool> OnAddCard;

    void GameSetup()
    {
        if(fastMode) delay = new WaitForSeconds(0.1f);
        
        switch (turnMode)
        {
            case TurnMode.Mine : 
                myTurn = true;
                break;
            case TurnMode.Other :
                myTurn = false;
                break;
            case TurnMode.Random:
                myTurn = Random.Range(0, 2) == 0;
                break;
        }
    }

    public IEnumerator StartGame()
    {
        GameSetup();
        isLoading = true;

        for (int i = 0; i < startCardCount; i++)
        {
            yield return delay;
            OnAddCard?.Invoke(false);
            yield return delay;
            OnAddCard?.Invoke(true);
        }
    }

    IEnumerator StartTurn()
    {
        isLoading = true;
        
        yield return delay2;
        OnAddCard?.Invoke(myTurn);
        yield return delay2;
        
        isLoading = false;
    }
}
