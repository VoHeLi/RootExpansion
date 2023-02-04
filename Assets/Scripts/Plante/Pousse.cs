using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pousse : Structure
{
    Pousse()
    {
        productionStats = new int[5] { 0, 0, 0, 0, 0 };
        pvStats = new int[5] { 10000, 10000, 10000, 10000, 10000};
        dommageStats = new int[5] { 0, 0, 0, 0, 0 };
        plantRootRadius = 0;
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
