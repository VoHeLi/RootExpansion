using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    

    [SerializeField] public List<Structure> playerStructures;

    private string playerName = "Plantar";
    private Color playerColor = Color.white;

    private Action action = null;


    public IEnumerator StartTurn()
    {
        //Ressources
        foreach(Structure structure in playerStructures){
            structure.ProduceRessource(this);
        }

        //Wait for action
        while(action == null)
        {
            yield return null;
        }


    }

    public void SetAction(Action action)
    {
        this.action = action;
    }
}
