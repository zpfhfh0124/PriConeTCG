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
        
    }

    private void Update()
    {
        
    }
}
