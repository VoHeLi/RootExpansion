using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeName : MonoBehaviour
{

   
    void Start()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = PlayerPrefs.GetString("winner") + " won!";
    }

   
}
