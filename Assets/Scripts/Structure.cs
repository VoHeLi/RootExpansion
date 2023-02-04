using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Structure : MonoBehaviour
{
    public Vector2Int position;

    public abstract void ProduceRessource(Player player);


}
