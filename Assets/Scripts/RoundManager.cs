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
    [SerializeField] private TileSelector tileSelector;

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
            players[i].playerId = i;
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

                    foreach(Player otherplayer in players)
                    {
                        map.DestroyUnlinkedStructures(otherplayer);
                    }

                    if (currentPlayer.endTurn)
                    {
                        currentPlayer.endTurn = false;
                        tileSelector.ClearTileSelection();
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

    public bool EndGameConditions()
    {
        if (nbTurn > nbTurnMax){ return true; }
        if (map.structures[0,0]==null || map.structures[map.width-1, map.height-1] == null) { return true; }
        return false;
    }

    
    public Player calculateWinner()
    {
        if(map.structures[0, 0] == null) { return map.structures[map.width - 1, map.height - 1].player; }
        Player P1 = map.structures[0, 0].player;
        float pointP1 = 0;

        if (map.structures[map.width - 1, map.height - 1] == null) { return map.structures[0, 0].player; }
        Player P2 = map.structures[0, 0].player;
        float pointP2 = 0;

        if (nbTurn > nbTurnMax)
        {
            for (int i = 0; i < map.width; i++)
            {
                for (int j = 0; j < map.height; j++)
                {
                    if (map.structures[i, j] != null)
                    {
                        if (map.structures[i, j].player == P1)
                        {
                            pointP1 += map.structures[i, j].niveau;
                        }
                        else if (map.structures[i, j].player == P2)
                        {
                            pointP2 += map.structures[i, j].niveau;
                        }
                    }
                }
            }
        }

        pointP1 += P1.ressources[0];
        pointP2 += P2.ressources[0];

        if (pointP1 < pointP2)
        {
            return P2;
        }
        return P1;
    
    }
    
}
