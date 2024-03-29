using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Player : MonoBehaviour
{

    [SerializeField] public List<Structure> playerStructures;

    public string playerName = "Plantar";
    private Color playerColor = Color.white;

    private Action action = null;
    public MapBase map;
    public int[] seeds = new int[4];
    public int playerId = 0;

    public TextMeshProUGUI waterCount;
    public TMP_Dropdown dropdown;
    public TextMeshProUGUI dropdownLabel;
    public TileSelector tileSelector;

    public enum Ressources : int
    {
        Eau = 0
    }

    public int[] ressources = new int[1];

    public bool endTurn;

    void Start()
    {
        endTurn = false;
        this.seeds[0] = 3;
        
        this.updateRessources();

        //TO REMOVE
        seeds[0] = 3;
        /*
        seeds[1] = 10;
        seeds[2] = 10;
        seeds[3] = 10;
        */
    }

    public IEnumerator WaitForAction()
    {
        updateRessources();

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

        dropdown.options.Clear();
        string[] plantList = new string[6] {"", "", "Sunflower", "Carnivorous Plant", "Ivy", "Cactus" };

        for (int i = 0; i < plantList.Length-2; i++)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(plantList[i+2] + " : " + this.seeds[i]));
        }

        dropdownLabel.text = plantList[(int)tileSelector.planteID] + " : " + seeds[Mathf.Max(0,(int)tileSelector.planteID-2)];
    }
}