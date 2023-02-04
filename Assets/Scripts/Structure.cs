using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Structure : MonoBehaviour
{
    public Vector2Int position;
    public int joueurId;
    public int niveau;
    public int[] productionStats;
    public int[] pvStats;
    public int[] dommageStats;

    public abstract void ProduceRessource(Player player);
    public abstract void Arroser(Player player);
    public abstract void Action(Player player);


}
