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

    public void Execute()
    {
        switch (actionType)
        {
            case ActionType.Plant:
                Plant();
                break;
        }
    }

    private void Plant()
    {

    }
}
