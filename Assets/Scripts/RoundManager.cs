using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{

    [SerializeField] private int playerCount;
    [SerializeField] private Vector2Int[] playerPos;
    [SerializeField] private Vector2Int[] playerNames;
    [SerializeField] private Vector2Int[] playerColor;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private MapBase map;

    private Player[] players;

    public TextMeshProUGUI waterCount;

    public TextMeshProUGUI turnCount;
    private int nbTurn = 1;
    private int nbTurnMax;

    public Player currentPlayer;

    void Awake()
    {
        players = new Player[playerCount];
        for(int i = 0; i < playerCount; i++)
        {
            players[i] = Instantiate(playerPrefab).GetComponent<Player>();
            players[i].map = map;
            players[i].waterCount = waterCount;
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
            foreach (Player player in players)
            {
                currentPlayer = player;
                turnCount.text = "Tour " + nbTurn.ToString() + " | " + currentPlayer.getName();
                yield return player.StartTurn();
            }
            nbTurn++;
        }
    }
}
