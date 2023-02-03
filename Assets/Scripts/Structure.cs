using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Structure : MonoBehaviour
{
    [SerializeField] private Vector2Int position;


    public abstract void ProduceRessource(Player player);
}
