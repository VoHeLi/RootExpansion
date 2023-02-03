using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    

    [SerializeField] public List<Structure> playerStructures;

    private string playerName = "Plantar";
    private Color playerColor = Color.white;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void StartTurn()
    {
        //Ressources
        foreach(Structure structure in playerStructures){
            structure.ProduceRessource(this);
        }

        //
    }
}
