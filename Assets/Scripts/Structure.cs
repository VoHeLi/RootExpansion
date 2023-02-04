using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Structure : MonoBehaviour
{
    private int _niveau;
    private int _life;

    public MapBase.StructureType type;
    public Vector2Int position;
    public Player player;
    public int[] productionStats;
    public int[] pvStats;
    public int[] dommageStats;
    public int attackRange;

    public int niveau
    {
        get
        {
            return _niveau;
        }
        set
        {
            _niveau = value;
            if(levelText != null)levelText.text = _niveau.ToString();
            
        }
    }

    public int life
    {
        get
        {
            return _niveau;
        }
        set
        {
            _life = value;
            if (lifeText != null) lifeText.text = _life.ToString();
            if (lifeBar != null) lifeBar.transform.localScale = new Vector3(_life / (float)pvStats[niveau], 1, 1);
        }
    }

    public TMPro.TextMeshProUGUI lifeText;
    public TMPro.TextMeshProUGUI levelText;
    public GameObject lifeBar;

    public abstract void ProduceRessource(Player player); 
    public void Arroser(Player player)
    {
        if ((niveau<3)&&(player.ressources[0] >= Mathf.Pow(2, niveau + 1)))
        {
            player.ressources[0] -= (int)Mathf.Pow(2, niveau + 1);
            niveau++;
        }
    }
    public abstract void Action(Player player);


}
