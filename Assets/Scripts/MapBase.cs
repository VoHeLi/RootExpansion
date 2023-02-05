using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBase : MonoBehaviour
{
    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private GameObject[] structurePrefabs;
    [SerializeField] private Vector3 offset;
    [SerializeField] public int width;
    [SerializeField] public int height;
    [SerializeField] public int onlyGrassMargin = 5;
    [SerializeField] private float space;
    [SerializeField] public RoundManager roundManager;
    [SerializeField] public Vector2Int[] startingPositions;

    [SerializeField] public Material blueTeamOutline;
    [SerializeField] public Material redTeamOutline;
    [SerializeField] public Material defaultOutline;

    private float noiseSpacing = 5f;


    public enum TileType : int
    {
        Grass = 0,
        Water = 1,
        CarnivorusField = 2,
        IvyField = 3,
        CactusField = 4
    }

    public enum StructureType : int
    {
        Pousse = 0,
        Racine = 1,
        Tournesol = 2,
        Carnivore = 3,
        Lierre = 4,
        Cactus = 5
    }

    public TileType[,] tiles;

    private GameObject[,] tilesObject;
    public CaseInfo[,] tilesInfos;

    public Structure[,] structures;

    private void Awake()
    {
        tiles = new TileType[width,height];
        tilesObject = new GameObject[width, height];
        tilesInfos = new CaseInfo[width, height];
        structures = new Structure[width, height];
    }

    private void Start()
    {
        initMap();
    }

    private void initMap()
    {
        float randomOffset = Random.Range(-100f, 100f); 
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                
                float noiseTileType = Mathf.PerlinNoise(randomOffset + ((float)i) / noiseSpacing, randomOffset + ((float)j) / noiseSpacing);
                if (noiseTileType < 0.05f)
                {
                    tiles[i, j] = TileType.CactusField;
                }
                else if (0.2f < noiseTileType && noiseTileType < 0.205f)
                {
                    tiles[i, j] = TileType.IvyField;
                }
                else if (0.4f < noiseTileType && noiseTileType < 0.405f)
                {
                    tiles[i, j] = TileType.CarnivorusField;
                }
                else if (noiseTileType > 0.7f)
                {
                    tiles[i, j] = TileType.Water;
                }
                else
                {
                    tiles[i, j] = TileType.Grass;
                }
                
                tilesObject[i, j] = Instantiate(tilePrefabs[(int)tiles[i, j]]);
                tilesObject[i, j].transform.position = offset + new Vector3(i * space + space / 2 * (j % 2), 0, j * space * 5.0f / 6.0f) ;
                tilesObject[i, j].transform.parent = transform;
                tilesInfos[i, j] = tilesObject[i, j].GetComponent<CaseInfo>();
                tilesInfos[i, j].casePos = new Vector2Int(i, j);
                tilesInfos[i, j].map = this;
            }
        }

        ReplaceStructure(Vector2Int.zero, StructureType.Pousse, roundManager.players[0]);
        ReplaceStructure(Vector2Int.right * 20, StructureType.Pousse, roundManager.players[1]);
    }

    public void AttackFromStructure(Vector2Int casePos, Player currentPlayer)
    {
        Structure structure = structures[casePos.x, casePos.y];

        structure.Action();
    }

    public void UpgradeStructure(Vector2Int casePos, Player currentPlayer)
    {
        Structure structure = structures[casePos.x, casePos.y];
        int cost = Mathf.RoundToInt(Mathf.Pow(2, structure.niveau + 1));

        currentPlayer.ressources[(int)Player.Ressources.Eau] -= cost;
        currentPlayer.updateRessources();

        structure.niveau += 1;
    }

    public void DestroyStructure(Structure structure)
    {
        Player structurePlayer = structure.player;
        structure.player.playerStructures.Remove(structure);
        tilesInfos[structure.position.x, structure.position.y].SetOutline(defaultOutline);
        structures[structure.position.x, structure.position.y] = null;

        if (structure != null)
        {
            Destroy(structure.gameObject);
        }

        
        
    }

    private Vector3 GetRealPosition(Vector2Int position)
    {
        return tilesObject[position.x, position.y].transform.position;
    }
    
    public void ReplaceStructure(Vector2Int position, StructureType type, Player player)
    {
        if(structures[position.x, position.y] != null)
        {
            Destroy(structures[position.x, position.y].gameObject);
        }

        if(type != StructureType.Racine)
        {
            PlaceRoots(position);
        }
        

        GameObject structureObject = Instantiate(structurePrefabs[(int)type]);
        structureObject.transform.position = GetRealPosition(position);
        structureObject.transform.rotation = currentRotation;
        structures[position.x, position.y] = structureObject.GetComponent<Structure>();
        structures[position.x, position.y].position = position;
        structures[position.x, position.y].type = type;
        structures[position.x, position.y].player = player;
        player.playerStructures.Add(structures[position.x, position.y]);
        tilesInfos[position.x, position.y].SetOutline(roundManager.currentPlayer == roundManager.players[0] ? blueTeamOutline : redTeamOutline);
    }

    private Structure hiddenStructure;
    private Structure previewStructure;
    private Quaternion currentRotation = Quaternion.identity;

    public void ReplaceStructureTemp(Vector2Int position, StructureType type)
    {
        if(hiddenStructure != null)
        {
            hiddenStructure.gameObject.SetActive(true);
        }

        if (structures[position.x, position.y] != null)
        {
            hiddenStructure = structures[position.x, position.y];
            hiddenStructure.gameObject.SetActive(false);
        }

        if (previewStructure == null)
        {
            GameObject structureObject = Instantiate(structurePrefabs[(int)type]);
            currentRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            structureObject.transform.rotation = currentRotation;
            previewStructure = structureObject.GetComponent<Structure>();
            previewStructure.position = position;
        }

        previewStructure.gameObject.transform.position = GetRealPosition(position);

    }

    public void ResetTempStructure()
    {
        if(hiddenStructure != null)
        {
            hiddenStructure.gameObject.SetActive(true);
        }
        if(previewStructure != null)
        {
            Destroy(previewStructure.gameObject);
            previewStructure = null;
        }
    }

    public void GetPossibleCases(List<CaseInfo> possible, List<CaseInfo> notPossible, MapBase.StructureType type, Action.ActionType actionType)
    {

       
        switch (actionType)
        {
            case Action.ActionType.PlantCactus:
            case Action.ActionType.PlantCarnivore:
            case Action.ActionType.PlantLierre:
            case Action.ActionType.PlantPousse:
            case Action.ActionType.PlantTournesol:

                bool[,] added = new bool[width, height];

                int[] plantRootRadiusArray = new int[6] { 0, 0, 2, 3, 4, 2 };
                int plantRootRadius = 0;

                switch (actionType)
                {
                    case Action.ActionType.PlantCactus:
                        plantRootRadius = plantRootRadiusArray[(int)StructureType.Cactus];
                        break;
                    case Action.ActionType.PlantCarnivore:
                        plantRootRadius = plantRootRadiusArray[(int)StructureType.Carnivore];
                        break;
                    case Action.ActionType.PlantLierre:
                        plantRootRadius = plantRootRadiusArray[(int)StructureType.Lierre];
                        break;
                    case Action.ActionType.PlantPousse:
                        plantRootRadius = plantRootRadiusArray[(int)StructureType.Pousse];
                        break;
                    case Action.ActionType.PlantTournesol:
                        plantRootRadius = plantRootRadiusArray[(int)StructureType.Tournesol];
                        break;
                }

                foreach (Structure structure in roundManager.currentPlayer.playerStructures)
                {
                    if (structure.type == StructureType.Racine)
                    {
                        continue;
                    }

                    List<Vector2Int> liste = CaseInfo.GetPossibleCases(structure.position, plantRootRadius, this, false);

                    foreach (Vector2Int tilePos in liste)
                    {
                        if (structures[tilePos.x, tilePos.y] != null) continue;
                        possible.Add(tilesInfos[tilePos.x, tilePos.y]);
                        added[tilePos.x, tilePos.y] = true;
                    }
                    
                }

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (!added[i, j])
                        {
                            notPossible.Add(tilesInfos[i, j]);
                        }
                        
                    }
                }

                return;
            case Action.ActionType.Arroser:
            case Action.ActionType.Attack:
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        (tilesInfos[i, j].IsCaseUsable(type, actionType) ? possible : notPossible).Add(tilesInfos[i, j]);
                    }
                }
                return;
        }

    }

    public List<Vector2Int> GetNeighbours(Vector2Int position)
    {
        List<Vector2Int> neightbours = new List<Vector2Int>();
        neightbours.Add(position + Vector2Int.left);
        neightbours.Add(position + Vector2Int.right);
        neightbours.Add(position + Vector2Int.up);
        neightbours.Add(position + Vector2Int.down);

        if(position.y % 2 == 0)
        {
            neightbours.Add(position + Vector2Int.up + Vector2Int.left);
            neightbours.Add(position + Vector2Int.down + Vector2Int.left);
        }
        else
        {
            neightbours.Add(position + Vector2Int.up + Vector2Int.right);
            neightbours.Add(position + Vector2Int.down + Vector2Int.right);
        }

        return neightbours;
    }

    public class TileNode
    {
        public TileNode parent;
        public Vector2Int position;
        public int length;

        public TileNode(TileNode parent, Vector2Int position, int length)
        {
            this.parent = parent;
            this.position = position;
            this.length = length;
        }
    }

    private void PlaceRoots(Vector2Int position)
    {
        bool[,] traveled = new bool[width, height];
        Queue queue = new Queue();

        TileNode node = new TileNode(null, position, 0);

        queue.Enqueue(node);



        while(queue.Count > 0)
        {
            node = (TileNode)queue.Dequeue();

            if(node.length > 10)
            {
                return;
            }

            if (structures[node.position.x, node.position.y] != null && structures[node.position.x, node.position.y].type != StructureType.Racine && structures[node.position.x, node.position.y].player == roundManager.currentPlayer)
            {
                break;

            }
            if (traveled[node.position.x, node.position.y]) continue;

            traveled[node.position.x, node.position.y] = true;

            List<Vector2Int> neighbours = GetNeighbours(node.position);

            foreach(Vector2Int neighbour in neighbours)
            {
                if ((neighbour.x  >= 0) && (neighbour.x  < height) && (neighbour.y >= 0) && (neighbour.y < width))
                {
                    if(tiles[neighbour.x, neighbour.y] == TileType.Grass)
                    {
                        if (structures[node.position.x, node.position.y] != null) {
                            if(structures[node.position.x, node.position.y].player == roundManager.currentPlayer || structures[node.position.x, node.position.y].type == StructureType.Racine)
                            {
                                queue.Enqueue(new TileNode(node, neighbour, node.length + 1));
                            }
                        }

                        else queue.Enqueue(new TileNode(node, neighbour, node.length+1));
                    }
                }
            }

        }

        if (node == null) return;
        node = node.parent;
        while (node.parent != null)
        {
            ReplaceStructure(node.position, StructureType.Racine, roundManager.currentPlayer);
            node = node.parent;
        }
    }



    public void DestroyUnlinkedStructures(Player player)
    {
        List<Vector2Int> toKeep = CaseInfo.GetLinkedCases(startingPositions[player.playerId], 100, this, player);

        bool[,] toKeepCases = new bool[width, height];

        foreach(Vector2Int position in toKeep)
        {
            toKeepCases[position.x, position.y] = true;
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (!toKeepCases[i, j])
                {
                    if(structures[i, j] != null && structures[i, j].player == player)
                    {
                        DestroyStructure(structures[i, j]);
                    }
                    
                }
            }
        }
    }
}
