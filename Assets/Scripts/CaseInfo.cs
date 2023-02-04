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
        return IsCasePlantable(planteID);
    }

    public bool IsCasePlantable(MapBase.StructureType planteID)
    {
        if (map.structures[casePos.x, casePos.y] != null) return false; 

        int[] plantRootRadiusArray = new int[6] { 3, 0, 2, 3, 4, 2};
        int plantRootRadius = plantRootRadiusArray[((int)planteID)];

        int plantCount = 0;
        for(int  iOffset = plantRootRadius * -2; iOffset < plantRootRadius * 2; iOffset++ )
        {
            for (int jOffset = plantRootRadius * -2; jOffset < plantRootRadius * 2; jOffset++)
            {
                if((casePos.x + iOffset >= 0) && (casePos.x + iOffset < map.height) && (casePos.y + jOffset >= 0) && (casePos.y + jOffset < map.width))
                {
                    // la case doit etre a une distance de la case
                    if (GetTileDistance(new Vector2Int(casePos.x + iOffset, casePos.y + jOffset)) <= plantRootRadius)
                    {
                        //if (pathLength(new Vector2Int(casePos.x + iOffset, casePos.y + jOffset), plantRootRadius) <= plantRootRadius)
                        //{
                            if (map.structures[casePos.x + iOffset, casePos.y + jOffset] != null && (map.structures[casePos.x + iOffset, casePos.y + jOffset].type != MapBase.StructureType.Racine) && (map.structures[casePos.x + iOffset, casePos.y + jOffset].player == map.roundManager.currentPlayer))
                            {
                                plantCount++;
                            }
                        //}
                    }
                }
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

    public bool isCaseArrosable(int water)
    {
        Structure selectedPlant = map.structures[casePos.x, casePos.y];

        if(selectedPlant == null) { return false;}
        if(selectedPlant.niveau >= 3 ) { return false;}
        if (Mathf.Pow(2, selectedPlant.niveau + 1) > water) { return false;}
        return true;
    }

    public bool isCaseAttackable(Structure attackingPlant) 
    {
        if (GetTileDistance(attackingPlant.position) > attackingPlant.attackRange) { return false; }
        return true; 
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

    class TileNode
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

    public List<Vector2Int> GetNeighbours(Vector2Int position)
    {
        List<Vector2Int> neightbours = new List<Vector2Int>();
        neightbours.Add(position + Vector2Int.left);
        neightbours.Add(position + Vector2Int.right);
        neightbours.Add(position + Vector2Int.up);
        neightbours.Add(position + Vector2Int.down);

        if (position.y % 2 == 0)
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
    public int pathLength(Vector2Int position, int radiusMax)
    {
        int pathLength = 0; 
        bool[,] traveled = new bool[map.width, map.height];
        Queue queue = new Queue();

        TileNode node = new TileNode(null, position, 0);
        queue.Enqueue(node);

        while (queue.Count > 0)
        {
            node = (TileNode)queue.Dequeue();

            if (node.length > radiusMax)
            {
                return radiusMax;
            }

            if (map.structures[node.position.x, node.position.y] != null && map.structures[node.position.x, node.position.y].type != MapBase.StructureType.Racine)
            {
                break;

            }
            if (traveled[node.position.x, node.position.y]) continue;

            traveled[node.position.x, node.position.y] = true;

            List<Vector2Int> neighbours = GetNeighbours(node.position);

            foreach (Vector2Int neighbour in neighbours)
            {
                if ((neighbour.x >= 0) && (neighbour.x < map.height) && (neighbour.y >= 0) && (neighbour.y < map.width))
                {
                    if (map.tiles[neighbour.x, neighbour.y] == MapBase.TileType.Grass)
                    {
                        queue.Enqueue(new TileNode(node, neighbour, node.length + 1));
                    }
                }
            }

        }

        if (node == null) return radiusMax;
        node = node.parent;
        if (node == null) return radiusMax;
        while (node.parent != null)
        {
            pathLength++;
            node = node.parent;
        }
        return pathLength;
    }
}










