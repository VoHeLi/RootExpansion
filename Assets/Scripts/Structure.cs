using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Structure : MonoBehaviour
{
    public Vector2Int position;
    public int joueurId;

    public abstract void ProduceRessource(Player player);


}
