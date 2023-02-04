using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectSeed : MonoBehaviour
{
    public TileSelector tileSelector;

    public void OnValueChange(TMP_Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0:
                tileSelector.changeSelectedPlant(MapBase.StructureType.Tournesol);
                break;
        }
    }
}
