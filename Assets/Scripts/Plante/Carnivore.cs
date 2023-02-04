using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carnivore : Structure
{
    Carnivore()
    {
        productionStats = new int[5] { 0, 0, 0, 0, 0 };
        pvStats = new int[5] { 25, 50, 50, 70, 70 };
        dommageStats = new int[5] { 20, 20, 40 , 40, 60 };
        attackRange = 1;
    }
    public override void ProduceRessource(Player player)
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

