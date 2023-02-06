using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] public TileSelector tileSelector;

    public Player[] players;

    public TextMeshProUGUI waterCount;
    public TMP_Dropdown dropdown;
    public TextMeshProUGUI dropdownLabel;

    public TextMeshProUGUI turnCount;
    private int nbTurn = 1;
    [SerializeField] private int nbTurnMax;

    public Player currentPlayer;

    void Awake()
    {
        players = new Player[playerCount];
        for(int i = 0; i < playerCount; i++)
        {
            players[i] = Instantiate(playerPrefab).GetComponent<Player>();
            players[i].map = map;

            players[i].waterCount = waterCount;
            players[i].dropdown = dropdown;
            players[i].dropdownLabel = dropdownLabel;
            players[i].tileSelector = tileSelector;

            players[i].playerId = i;
        }
        currentPlayer = players[0];

        players[0].playerName = "Player 1";
        players[1].playerName = "Player 2";
    }

    void Start()
    {
        StartCoroutine(TurnPlay());
    }


    IEnumerator TurnPlay()
    {
        while (!EndGameConditions())
        {
            for (int k = 0; k < players.Length; k++)
            {
                cameras[k].SetActive(true);
                Player player = players[k];

                currentPlayer = player;

                player.seeds[((int)MapBase.StructureType.Tournesol) - 2]++;
                player.updateRessources();
                //Ressources
                foreach (Structure structure in currentPlayer.playerStructures)
                {
                    structure.ProduceRessource(currentPlayer);
                    structure.ProduceSeeds(currentPlayer);
                }

                for (int i = 0; i < actionCount; i++)
                {
                    turnCount.text = "Turnd " + nbTurn.ToString() + "/" + nbTurnMax.ToString() + " | Action " + (i+1).ToString() + "/3 | " + currentPlayer.getName();
                    yield return player.WaitForAction();

                    foreach(Player otherplayer in players)
                    {
                        map.DestroyUnlinkedStructures(otherplayer);
                    }

                    if (currentPlayer.endTurn)
                    {
                        currentPlayer.endTurn = false;
                        tileSelector.ClearTileSelection();
                        tileSelector.pendingAction = null;
                        break;
                    }
                }
                cameras[k].SetActive(false);
            }
            nbTurn++;
        }

        string winnerName = calculateWinner().playerName;

        PlayerPrefs.SetString("winner", winnerName);

        SceneManager.LoadScene("Victoire");
    }


    public void EndTurn()
    {
        currentPlayer.endTurn = true;
    }

    public bool EndGameConditions()
    {
        if (nbTurn < 2) return false;

        if (nbTurn > nbTurnMax){ return true; }
        if (map.structures[map.startingPositions[0].x, map.startingPositions[0].y] ==null || map.structures[map.startingPositions[1].x, map.startingPositions[1].y] == null) { return true; }
        return false;
    }

    
    public Player calculateWinner()
    {
        if(map.structures[map.startingPositions[0].x, map.startingPositions[0].y] == null) { return map.structures[map.startingPositions[1].x, map.startingPositions[1].y].player; }
        Player P1 = map.structures[map.startingPositions[0].x, map.startingPositions[0].y].player;
        float pointP1 = 0;

        if (map.structures[map.startingPositions[1].x, map.startingPositions[1].y] == null) { return map.structures[map.startingPositions[0].x, map.startingPositions[0].y].player; }
        Player P2 = map.structures[map.startingPositions[1].x, map.startingPositions[1].y].player;
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
