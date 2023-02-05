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
    public MapBase map;
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
            if(levelText != null)levelText.text = (_niveau+1).ToString();

            if(value > 0)
            {
                life += pvStats[value] - pvStats[value - 1];
            }
        }
    }

    public int life
    {
        get
        {
            return _life;
        }
        set
        {
            _life = value;
            if (lifeText != null) lifeText.text = _life.ToString() + " / " + pvStats[niveau];
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

    public abstract void Action();
    public int GetTileDistance(Vector2Int case2Pos)
    {
        int aX1 = position.x;
        int aY1 = position.y;
        int aX2 = case2Pos.x;
        int aY2 = case2Pos.y;
        int dx = aX2 - aX1;     // signed deltas
        int dy = aY2 - aY1;
        int x = Mathf.Abs(dx);  // absolute deltas
        int y = Mathf.Abs(dy);
        // special case if we start on an odd row or if we move into negative x direction
        if ((dx < 0) ^ ((aY1 & 1) == 1))
            x = Mathf.Max(0, x - (y + 1) / 2);
        else
            x = Mathf.Max(0, x - (y) / 2);
        return x + y;
    }
    public Structure findClosest(List<Structure> structures)
    {
        List<Structure> Closests = new();
        int min = 100;

        Debug.Log(structures.Count);
        foreach (Structure structure in structures)
        {
            int dist = GetTileDistance(structure.position);
            if (dist < min)
            {
                min = dist;
                Closests.Clear();
                Closests.Add(structure);
                Debug.Log("ljtyhdrtegrdkgi");
            }
            else if (dist == min)
            {
                Closests.Add(structure);
            }
        }
        if (Closests.Count == 0) 
        {
            Debug.Log("le probleme est ici");
            return null;
        }
        int rand = Random.Range(0, Closests.Count - 1);
        Debug.Log(rand);
        return Closests[rand];

    }
    
    public void hurt(int dommage)
    {
        Debug.Log(life);
        life -= dommage;
        if (life < 0) { life = 0; }
    }

    public void Start()
    {
        map = GameObject.Find("Map").GetComponent<MapBase>();

        if(pvStats != null && pvStats.Length > 0)
        {
            life = pvStats[0];
        }
    }
}
