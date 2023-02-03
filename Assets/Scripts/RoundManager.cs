using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{

    [SerializeField] private int playerCount;
    [SerializeField] private Vector2Int[] playerPos;
    [SerializeField] private Vector2Int[] playerNames;
    [SerializeField] private Vector2Int[] playerColor;
    [SerializeField] private GameObject playerPrefab;

    private Player[] players;


    private Player currentPlayer;

    void Awake()
    {
        players = new Player[playerCount];
        for(int i = 0; i < playerCount; i++)
        {
            players[i] = Instantiate(playerPrefab).GetComponent<Player>();
            
        }
        currentPlayer = players[0];
    }

    void Start()
    {
        StartCoroutine(TurnPlay());
    }


    IEnumerator TurnPlay()
    {
        while (true)
        {
            foreach(Player player in players)
            {
                player.StartTurn();
            }
        }
    }
}
