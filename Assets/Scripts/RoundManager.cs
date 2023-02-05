using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{

    [SerializeField] private int playerCount;
    [SerializeField] public int actionCount = 3;
    [SerializeField] private Vector2Int[] playerPos;
    [SerializeField] private Vector2Int[] playerNames;
    [SerializeField] private Vector2Int[] playerColor;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private MapBase map;
    [SerializeField] private GameObject[] cameras;

    public Player[] players;

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
            for (int k = 0; k < players.Length; k++)
            {
                cameras[k].SetActive(true);
                Player player = players[k];

                currentPlayer = player;

                //Ressources
                foreach (Structure structure in currentPlayer.playerStructures)
                {
                    structure.ProduceRessource(currentPlayer);
                }

                for (int i = 0; i < actionCount; i++)
                {
                    turnCount.text = "Tour " + nbTurn.ToString() + " | Action " + (i+1).ToString() + " /3 | " + currentPlayer.getName();
                    yield return player.WaitForAction();

                    if (currentPlayer.endTurn)
                    {
                        currentPlayer.endTurn = false;
                        break;
                    }
                }
                cameras[k].SetActive(false);
            }
            nbTurn++;
        }
    }


    public void EndTurn()
    {
        currentPlayer.endTurn = true;
    }
}
