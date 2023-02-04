using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene(0); //� changer avec la sc�ne de jeu
    }


    public void QuitGame()
    {
        Debug.Log("Quit !");
        Application.Quit();
    }
}
