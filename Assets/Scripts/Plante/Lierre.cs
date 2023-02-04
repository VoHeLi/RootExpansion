using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lierre : Structure
{
    Lierre()
    {
        productionStats = new int[5] { 0, 0, 0, 0, 0 };
        pvStats = new int[5] { 10, 20, 20, 35, 35 };
        dommageStats = new int[5] { 10, 10, 20, 20, 30 };
        attackRange = 4;
    }
    public override void ProduceRessource(Player player)
    {
        throw new System.NotImplementedException();
    }


    public override void Action(Player player)
    {
        List<Structure> Attackable = new();

        for (int iOffset = attackRange * -2; iOffset < attackRange * 2; iOffset++)
        {
            for (int jOffset = attackRange * -2; jOffset < attackRange * 2; jOffset++)
            {
                if ((position.x + iOffset >= 0) && (position.x + iOffset < map.height) && (position.y + jOffset >= 0) && (position.y + jOffset < map.width))
                {
                    // la case doit etre a une distance de la case
                    if (GetTileDistance(new Vector2Int(position.x + iOffset, position.y + jOffset)) <= attackRange)
                    {
                        if (map.structures[position.x + iOffset, position.y + jOffset] != null
                            && (map.structures[position.x + iOffset, position.y + jOffset].type != MapBase.StructureType.Racine)
                            && (map.structures[position.x + iOffset, position.y + jOffset].player == map.roundManager.currentPlayer))
                        {
                            Attackable.Add(map.structures[position.x + iOffset, position.y + jOffset]);
                        }

                    }
                }
            }
        }

        List<Structure> Attacked = new();
        for (int i = 0; i < 1; i++)
        {
            Structure plant = findClosest(Attackable);
            Attacked.Add(plant);
            Attackable.Remove(plant);
        }

        /// MTN JUSTE A FAIRE DES DEGATS AUX PLANTES DANS ATTACKED
    }

    Structure findClosest(List<Structure> structures)
    {
        List<Structure> Closests = new();
        int min = 100;
        foreach (Structure structure in structures)
        {
            int dist = GetTileDistance(structure.position);
            if (dist < min)
            {
                min = dist;
                Closests.Clear();
                Closests.Add(structure);
            }
            else if (dist == min)
            {
                Closests.Add(structure);
            }
        }

        return Closests[Random.Range(0, Closests.Count)];

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



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

