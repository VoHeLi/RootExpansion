using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public enum ActionType
    {
        Pass,
        Plant,
        Arroser,
        Attack
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
            case ActionType.Plant:
                map.ReplaceStructureTemp(actionTile.casePos);
                break;
        }
       
    }

    public void Execute(MapBase map)
    {
        switch (actionType)
        {
            case ActionType.Plant:
                map.ReplaceStructure(actionTile.casePos, 0);
                break;
        }
    }
}
