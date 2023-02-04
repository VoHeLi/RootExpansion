using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBase : MonoBehaviour
{
    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private Vector3 offset;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float space;
    private float noiseSpacing = 5f;


    public enum TileType : int
    {
        Grass = 0,
        Field = 1
    }

    private TileType[,] tiles;

    private GameObject[,] tilesObject;

    private void Awake()
    {
        tiles = new TileType[width,height];
        tilesObject = new GameObject[width, height];
    }

    private void Start()
    {
        initMap();
    }

    private void initMap()
    {
        float randomOffset = Random.Range(-100f, 100f); 
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float noiseTileType = Mathf.PerlinNoise(randomOffset + ((float)i)/noiseSpacing , randomOffset + ((float)j) / noiseSpacing);
                if(noiseTileType > 0.7f)
                {
                    tiles[i, j] = TileType.Field;
                }
                else
                {
                    tiles[i, j] = TileType.Grass;
                }
                tilesObject[i, j] = Instantiate(tilePrefabs[(int)tiles[i, j]]);
                tilesObject[i, j].transform.position = offset + new Vector3(i * space + space / 2 * (j % 2), 0, j * space * 5.0f / 6.0f) ;
                tilesObject[i, j].transform.parent = transform;
                tilesObject[i, j].GetComponent<CaseInfo>().casePos = new Vector2Int(i, j);
            }
        }
    }

    void Update()
    {
        
    }
}
