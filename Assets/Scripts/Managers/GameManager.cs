using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 상태 관리
public class GameManager : MonoBehaviour
{
    static public GameManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
#if UNITY_EDITOR 
        CheatInput();
#endif
    }

    void CheatInput()
    { 
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            TurnManager.OnAddCard?.Invoke(true);
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            TurnManager.OnAddCard?.Invoke(false);
        }
    }

    public void StartGame()
    {
        StartCoroutine(TurnManager.Instance.StartGame());
    }
}
