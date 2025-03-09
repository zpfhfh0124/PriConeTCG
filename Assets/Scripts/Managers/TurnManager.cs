using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] [Tooltip("개발자 모드 한정 시작 턴 모드 지정")] 
    TurnMode turnMode;

    [Header("Properties")]
    public bool myTurn;

    void GameSetup()
    {
    
    }
}
