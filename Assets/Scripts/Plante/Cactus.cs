using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : Structure
{
    Cactus()
    {
        productionStats = new int[5] { 0, 0, 0, 0, 0 };
        pvStats = new int[5] { 15, 25, 25, 40, 40 };
        dommageStats = new int[5] { 5, 5, 10, 10, 15 };
        
    }
    public override void ProduceRessource(Player player)
    {
        throw new System.NotImplementedException();
    }

    public override void Arroser(Player player)
    {
        throw new System.NotImplementedException();
    }

    public override void Action(Player player)
    {
        throw new System.NotImplementedException();
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

