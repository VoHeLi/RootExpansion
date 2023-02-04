using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{
    public GameObject plantInfo;
    public GameObject sprayInfo;
    public GameObject attackInfo;
    public GameObject waterInfo;
    public GameObject seedInfo;
    public GameObject settingsInfo;

    void Start() 
    {
        plantInfo.SetActive(false);
        sprayInfo.SetActive(false);
        attackInfo.SetActive(false);
        waterInfo.SetActive(false);
        settingsInfo.SetActive(false);
        seedInfo.SetActive(false);
    }

    // Planter
    public void showInfoPlanter()
    {
        plantInfo.SetActive(true);
    }

    public void hideInfoPlanter()
    {
        plantInfo.SetActive(false);
    }

    // Arroser
    public void showInfoArroser()
    {
        sprayInfo.SetActive(true);
    }

    public void hideInfoArroser()
    {
        sprayInfo.SetActive(false);
    }

    // Attaquer
    public void showInfoAttaquer()
    {
        attackInfo.SetActive(true);
    }

    public void hideInfoAttaquer()
    {
        attackInfo.SetActive(false);
    }

    // Eau
    public void showInfoWater()
    {
        waterInfo.SetActive(true);
    }

    public void hideInfoWater()
    {
        waterInfo.SetActive(false);
    }

    // Graines
    public void showInfoSeed()
    {
        seedInfo.SetActive(true);
    }

    public void hideInfoSeed()
    {
        seedInfo.SetActive(false);
    }

    // Settings
    public void showInfoSettings()
    {
        settingsInfo.SetActive(true);
    }

    public void hideInfoSettings()
    {
        settingsInfo.SetActive(false);
    }
}
