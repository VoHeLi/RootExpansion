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

    public bool IsCasePlantable()
    {
        // CETTE FONCTION DEPEND DU TYPE DE PLANTE QUE L'ON SEME ET L'ORIGINE
        Vector2Int originPos = new Vector2Int(0, 0); //A CHANGER
        int plantRootRadius = 3; //A CHANGER


        // la case doit etre a une distance de la case
        if (GetTileDistance(originPos) > plantRootRadius)
        {
            return false;
        }

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
