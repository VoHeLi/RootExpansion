using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : Structure
{
    Cactus()
    {
        productionStats = new int[5] { 0, 0, 0, 0, 0 };
        pvStats = new int[5] { 15, 25, 25, 40, 40 };
        dommageStats = new int[5] { 5, 5, 10, 10, 15 };
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
        for(int i = 0; i < 3; i++)
        {
            Structure plant = findClosest(Attackable);
            Attacked.Add(plant);
            Attackable.Remove(plant);
        }

        /// MTN JUSTE A FAIRE DES DEGATS AUX PLANTES DANS ATTACKED
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

