using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{
    public GameObject infoActionBackgroud;
    public GameObject planterText;
    public GameObject arroserText;
    public GameObject attaquerText;
    public GameObject waterInfo;

    void Start() 
    {
        infoActionBackgroud.SetActive(false);
        planterText.SetActive(false);
        arroserText.SetActive(false);
        attaquerText.SetActive(false);
        waterInfo.SetActive(false);
    }

    // Planter
    public void showInfoPlanter()
    {
        infoActionBackgroud.SetActive(true);
        planterText.SetActive(true);
    }

    public void hideInfoPlanter()
    {
        infoActionBackgroud.SetActive(false);
        planterText.SetActive(false);
    }

    // Arroser
    public void showInfoArroser()
    {
        infoActionBackgroud.SetActive(true);
        arroserText.SetActive(true);
    }

    public void hideInfoArroser()
    {
        infoActionBackgroud.SetActive(false);
        arroserText.SetActive(false);
    }

    // Attaquer
    public void showInfoAttaquer()
    {
        infoActionBackgroud.SetActive(true);
        attaquerText.SetActive(true);
    }

    public void hideInfoAttaquer()
    {
        infoActionBackgroud.SetActive(false);
        attaquerText.SetActive(false);
    }

    // Eau
    public void showInfoWater()
    {
        UnityEngine.Debug.Log("test");
        waterInfo.SetActive(true);
    }

    public void hideInfoWater()
    {
        waterInfo.SetActive(false);
    }
}
