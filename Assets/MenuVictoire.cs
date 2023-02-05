using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuVictoire : MonoBehaviour
{
    // Start is called before the first frame update
    public void BackMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
