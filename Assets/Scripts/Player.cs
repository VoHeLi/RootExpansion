using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

    [SerializeField] public List<Structure> playerStructures;

    private string playerName = "Plantar";
    private Color playerColor = Color.white;

    private Action action = null;
    public MapBase map;
    public int[] seeds = new int[4];
    public int playerId = 0;

    public TextMeshProUGUI waterCount;
    public TMP_Dropdown dropdown;

    public enum Ressources : int
    {
        Eau = 0
    }

    public int[] ressources = new int[1];

    public bool endTurn;

    void Start()
    {
        endTurn = false;
        this.updateRessources();
    }

    public IEnumerator WaitForAction()
    {
        

        //Wait for action
        while (action == null)
        {
            if (endTurn)
            {
                action = new Action(Action.ActionType.Pass);
            }
            yield return new WaitForSeconds(0.1f);
        }

        map.ResetTempStructure();
        action.Execute(map);
        
        action = null;

        
    }

    

    public void SetAction(Action action)
    {
        this.action = action;
    }

    public string getName()
    {
        return this.playerName;
    }

    public void updateRessources()
    {
        UnityEngine.Debug.Log(waterCount);
        waterCount.text = this.ressources[(int)Player.Ressources.Eau].ToString();

        //dropdown.options.Clear();
        //dropdown.options.Add(new Dropdown.OptionData() { text = "your stuff" });
    }
}
