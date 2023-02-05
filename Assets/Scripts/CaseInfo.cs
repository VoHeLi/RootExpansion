using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseInfo : MonoBehaviour
{
    public Vector2Int casePos;

    private MeshRenderer meshRenderer;
    public MapBase map;

    public void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void SetOutline(Material material)
    {

        Material[] mats = meshRenderer.sharedMaterials;
        mats[1] = material;
        meshRenderer.materials = mats;
    }

    public bool IsCaseUsable(MapBase.StructureType planteID, Action.ActionType actionType)
    {
        switch (actionType)
        {
            case Action.ActionType.PlantCactus:
            case Action.ActionType.PlantCarnivore:
            case Action.ActionType.PlantLierre:
            case Action.ActionType.PlantPousse:
            case Action.ActionType.PlantTournesol:
                return IsCasePlantable(planteID);
            case Action.ActionType.Arroser:
                return isCaseArrosable();
            case Action.ActionType.Attack:
                return isCaseAttackable();

        }
        return IsCasePlantable(planteID);
    }

    public bool IsCasePlantable(MapBase.StructureType planteID)
    {
        if (map.structures[casePos.x, casePos.y] != null) return false; 

        int[] plantRootRadiusArray = new int[6] { 0, 0, 2, 3, 4, 2};
        int plantRootRadius = plantRootRadiusArray[((int)planteID)];


        List<Vector2Int> possibleCases = GetPossibleCases(casePos, plantRootRadius, map, true);

        int plantCount = 0;
        foreach(Vector2Int otherPos in possibleCases){ 
            if (map.structures[otherPos.x, otherPos.y] != null && (map.structures[otherPos.x, otherPos.y].type != MapBase.StructureType.Racine) && (map.structures[otherPos.x, otherPos.y].player == map.roundManager.currentPlayer))
            {
                plantCount++;
            }
        }
        if (plantCount <= 0) { return false; }
        // la case doit etre de type grass
        if (map.tiles[casePos.x, casePos.y] != 0)
        {
            return false;
        }

        /*if ((map.structures[casePos.x, casePos.y] != null) && (map.structures[casePos.x, casePos.y].player != map.roundManager.currentPlayer))
        {
            Debug.Log(casePos);
            return false;
        }*/


        return true;
    }



    public bool isCaseArrosable()
    {
        Structure selectedPlant = map.structures[casePos.x, casePos.y];
        int water = map.roundManager.currentPlayer.ressources[0];
        if (selectedPlant == null) { return false;}
        if(selectedPlant.niveau > 3 ) { return false;}
        if(selectedPlant.type == MapBase.StructureType.Racine) { return false; }
        if(selectedPlant.player != map.roundManager.currentPlayer) { return false; }
        if (Mathf.RoundToInt(Mathf.Pow(2, selectedPlant.niveau + 1)) > water) { return false;}
        return true;
    }

    public bool isCaseAttackable() 
    {
        if(map.structures[casePos.x, casePos.y] == null) { return false; }
        if(map.structures[casePos.x, casePos.y].player != map.roundManager.currentPlayer) { return false; }
        int attackRange = map.structures[casePos.x, casePos.y].attackRange;
        Player player = map.structures[casePos.x, casePos.y].player;
        int plantCount = 0;
        for (int iOffset = attackRange * -2; iOffset < attackRange * 2; iOffset++)
        {
            for (int jOffset = attackRange * -2; jOffset < attackRange * 2; jOffset++)
            {
                if ((casePos.x + iOffset >= 0) && (casePos.x + iOffset < map.height) && (casePos.y + jOffset >= 0) && (casePos.y + jOffset < map.width))
                {
                    // la case doit etre a une distance de la case
                    if (GetTileDistance(new Vector2Int(casePos.x + iOffset, casePos.y + jOffset)) <= attackRange)
                    {
                        if((map.structures[casePos.x + iOffset, casePos.y + jOffset] != null)&&(map.structures[casePos.x + iOffset, casePos.y + jOffset].player != player))
                            if (map.structures[casePos.x + iOffset, casePos.y + jOffset].type != MapBase.StructureType.Racine)
                            {
                            
                                plantCount++;
                            }

                    }
                }
            }
        }
        return plantCount>0; 
    }

    int GetTileDistance(Vector2Int case2Pos)
    {
        int aX1 = casePos.x;
        int aY1 = casePos.y;
        int aX2 = case2Pos.x;
        int aY2 = case2Pos.y;
        int dx = aX2 - aX1;     // signed deltas
        int dy = aY2 - aY1;
        int x = Mathf.Abs(dx);  // absolute deltas
        int y = Mathf.Abs(dy);
        // special case if we start on an odd row or if we move into negative x direction
        if ((dx < 0) ^ ((aY1 & 1) == 1))
            x = Mathf.Max(0, x - (y + 1) / 2);
        else
            x = Mathf.Max(0, x - (y) / 2);
        return x + y;
    }



    public static List<Vector2Int> GetPossibleCases(Vector2Int start, int plantRootRadius, MapBase map, bool useRoot)
    {
        List<Vector2Int> accessibleCases = new();

        bool[,] traveled = new bool[map.width, map.height];
        Queue queue = new Queue();

        MapBase.TileNode node = new MapBase.TileNode(null, start, 0);

        queue.Enqueue(node);



        while (queue.Count > 0)
        {
            node = (MapBase.TileNode)queue.Dequeue();

            if (node.length > plantRootRadius)
            {
                break;
            }

            if (traveled[node.position.x, node.position.y]) continue;

            traveled[node.position.x, node.position.y] = true;
            accessibleCases.Add(node.position);

            List<Vector2Int> neighbours = map.GetNeighbours(node.position);

            foreach (Vector2Int neighbour in neighbours)
            {
                if ((neighbour.x >= 0) && (neighbour.x < map.height) && (neighbour.y >= 0) && (neighbour.y < map.width))
                {
                    if (map.tiles[neighbour.x, neighbour.y] == MapBase.TileType.Grass)
                    {
                        if (map.structures[node.position.x, node.position.y] != null)
                        {
                            if ((map.structures[node.position.x, node.position.y].player == map.roundManager.currentPlayer || (map.structures[node.position.x, node.position.y].type == MapBase.StructureType.Racine && useRoot)) && !(!useRoot && map.structures[node.position.x, node.position.y].type != MapBase.StructureType.Racine))
                            {
                                queue.Enqueue(new MapBase.TileNode(node, neighbour, node.length + 1));
                            }
                        }

                        else
                        {
                            queue.Enqueue(new MapBase.TileNode(node, neighbour, node.length + 1));
                        }
                    }
                }
            }

        }
        return accessibleCases;
    }



    public static List<Vector2Int> GetLinkedCases(Vector2Int start, int plantRootRadius, MapBase map, Player player)
    {
        List<Vector2Int> accessibleCases = new();

        bool[,] traveled = new bool[map.width, map.height];
        Queue queue = new Queue();

        MapBase.TileNode node = new MapBase.TileNode(null, start, 0);

        queue.Enqueue(node);



        while (queue.Count > 0)
        {
            node = (MapBase.TileNode)queue.Dequeue();

            if (node.length > plantRootRadius)
            {
                break;
            }

            if (traveled[node.position.x, node.position.y]) continue;

            traveled[node.position.x, node.position.y] = true;
            accessibleCases.Add(node.position);

            List<Vector2Int> neighbours = map.GetNeighbours(node.position);

            foreach (Vector2Int neighbour in neighbours)
            {
                if ((neighbour.x >= 0) && (neighbour.x < map.height) && (neighbour.y >= 0) && (neighbour.y < map.width))
                {
                    if (map.tiles[neighbour.x, neighbour.y] == MapBase.TileType.Grass)
                    {
                        if (map.structures[node.position.x, node.position.y] != null)
                        {
                            if (map.structures[node.position.x, node.position.y].player == player)
                            {
                                queue.Enqueue(new MapBase.TileNode(node, neighbour, node.length + 1));
                            }
                        }
                    }
                }
            }

        }
        return accessibleCases;
    }
}