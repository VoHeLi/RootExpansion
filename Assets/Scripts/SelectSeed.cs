using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectSeed : MonoBehaviour
{
    public TileSelector tileSelector;
    public TMP_Dropdown dropDownStart;

    void start()
    {
        tileSelector.changeSelectedSeed(dropDownStart.value);
    }

    public void OnDropDownChanged(TMP_Dropdown dropDown)
    {
        UnityEngine.Debug.Log(dropDown.value);
        tileSelector.changeSelectedSeed(dropDown.value);
    }
}
