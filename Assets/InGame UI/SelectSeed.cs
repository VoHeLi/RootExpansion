using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectSeed : MonoBehaviour
{
    public TileSelector tileSelector;
    public TMP_Dropdown dropdown;

    public void OnValueChange()
    {
        switch (dropdown.value)
        {
            case 0:
                tileSelector.changeSelectedPlant(MapBase.StructureType.Tournesol);
                break;
            case 1:
                tileSelector.changeSelectedPlant(MapBase.StructureType.Lierre);
                break;
            case 2:
                tileSelector.changeSelectedPlant(MapBase.StructureType.Carnivore);
                break;
            case 3:
                tileSelector.changeSelectedPlant(MapBase.StructureType.Cactus);
                break;
        }
    }
}
