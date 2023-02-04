using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseInfo : MonoBehaviour
{
    public Vector2Int casePos;

    private MeshRenderer meshRenderer;
    public MapBase map;

    public void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void SetOutline(Material material)
    {
        Material[] mats = meshRenderer.sharedMaterials;
        mats[1] = material;
        meshRenderer.materials = mats;
    }

    public bool IsCasePlantable(MapBase.StructureType planteID)
    {
        int[] plantRootRadiusArray = new int[6] { 0, 1, 2, 3, 4, 2};
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
                        if (map.structures[casePos.x + iOffset, casePos.y + jOffset] != null)
                        {
                            plantCount++;
                        }
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
}
