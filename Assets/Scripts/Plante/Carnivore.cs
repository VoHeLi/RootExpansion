using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carnivore : Structure
{
    Carnivore()
    {
        productionStats = new int[5] { 0, 0, 0, 0, 0 };
        pvStats = new int[5] { 25, 50, 50, 70, 70 };
        dommageStats = new int[5] { 20, 20, 40 , 40, 60 };
        attackRange = 1;
    }
    public override void ProduceRessource(Player player)
    {
        throw new System.NotImplementedException();
    }

    public override void Action()
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
                        if (map.structures[position.x + iOffset, position.y + jOffset].player != player)
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
        }

        List<Structure> Attacked = new();
        for (int i = 0; i < 1; i++)
        {
            Structure plant = findClosest(Attackable);
            Attacked.Add(plant);
            Attackable.Remove(plant);
        }

        foreach (Structure plant in Attacked)
        {
            plant.hurt(dommageStats[niveau]);
        }
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

