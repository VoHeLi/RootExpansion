using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public enum ActionType
    {
        Pass,
        PlantPousse,
        Arroser,
        Attack,
        PlantTournesol,
        PlantCarnivore,
        PlantLierre,
        PlantCactus
    };

    public ActionType actionType;
    public CaseInfo actionTile;

    public Action(ActionType action)
    {
        actionType = action;
    }

    public void Previsualize(MapBase map)
    {
        switch (actionType)
        {
            case ActionType.PlantPousse:
                map.ReplaceStructureTemp(actionTile.casePos, MapBase.StructureType.Pousse);
                break;
            case ActionType.PlantCactus:
                map.ReplaceStructureTemp(actionTile.casePos, MapBase.StructureType.Cactus);
                break;
            case ActionType.PlantCarnivore:
                map.ReplaceStructureTemp(actionTile.casePos, MapBase.StructureType.Carnivore);
                break;
            case ActionType.PlantLierre:
                map.ReplaceStructureTemp(actionTile.casePos, MapBase.StructureType.Lierre);
                break;
            case ActionType.PlantTournesol:
                map.ReplaceStructureTemp(actionTile.casePos, MapBase.StructureType.Tournesol);
                break;
        }
       
    }

    public void Execute(MapBase map)
    {
        switch (actionType)
        {
            case ActionType.PlantPousse:
                map.ReplaceStructure(actionTile.casePos, MapBase.StructureType.Pousse, map.roundManager.currentPlayer);
                break;
            case ActionType.PlantCactus:
                map.ReplaceStructure(actionTile.casePos, MapBase.StructureType.Cactus, map.roundManager.currentPlayer);
                break;
            case ActionType.PlantCarnivore:
                map.ReplaceStructure(actionTile.casePos, MapBase.StructureType.Carnivore, map.roundManager.currentPlayer);
                break;
            case ActionType.PlantLierre:
                map.ReplaceStructure(actionTile.casePos, MapBase.StructureType.Lierre, map.roundManager.currentPlayer);
                break;
            case ActionType.PlantTournesol:
                map.ReplaceStructure(actionTile.casePos, MapBase.StructureType.Tournesol, map.roundManager.currentPlayer);
                break;
            case ActionType.Arroser:
                map.UpgradeStructure(actionTile.casePos, map.roundManager.currentPlayer);
                break;
            case ActionType.Attack:
                map.AttackFromStructure(actionTile.casePos, map.roundManager.currentPlayer);
                break;
        }
    }
}
