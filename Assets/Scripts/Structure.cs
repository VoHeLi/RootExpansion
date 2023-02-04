using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Structure : MonoBehaviour
{
    public MapBase.StructureType type;
    public Vector2Int position;
    public Player player;
    public int niveau;
    public int[] productionStats;
    public int[] pvStats;
    public int[] dommageStats;
    public int attackRange;

    public abstract void ProduceRessource(Player player); 
    public void Arroser(Player player)
    {
        if ((niveau<3)&&(player.ressources[0] >= Mathf.Pow(2, niveau + 1)))
        {
            player.ressources[0] -= (int)Mathf.Pow(2, niveau + 1);
            niveau++;
        }
    }
    public abstract void Action(Player player);


}
