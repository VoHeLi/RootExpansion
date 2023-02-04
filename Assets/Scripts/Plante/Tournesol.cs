using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tournesol : Structure
{
    Tournesol()
    {
        productionStats = new int[5] { 1, 1, 2, 2, 3 };
        pvStats = new int[5] { 10, 20, 20, 35, 35 };
        dommageStats = new int[5] { 0, 0, 0, 0, 0 };
        plantRootRadius = 3;
    }

    [ContextMenu("produce")]
    public override void ProduceRessource(Player player)
    {
        player.ressources[0] += productionStats[niveau];
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

