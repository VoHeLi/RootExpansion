using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBase : MonoBehaviour
{
    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private GameObject[] structurePrefabs;
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

    public TileType[,] tiles;

    private GameObject[,] tilesObject;
    private CaseInfo[,] tilesInfos;

    private Structure[,] structures;

    private void Awake()
    {
        tiles = new TileType[width,height];
        tilesObject = new GameObject[width, height];
        tilesInfos = new CaseInfo[width, height];
        structures = new Structure[width, height];
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
                tilesInfos[i, j] = tilesObject[i, j].GetComponent<CaseInfo>();
                tilesInfos[i, j].casePos = new Vector2Int(i, j);
                tilesInfos[i, j].map = this;
            }
        }
    }

    private Vector3 GetRealPosition(Vector2Int position)
    {
        return tilesObject[position.x, position.y].transform.position;
    }
    
    public void ReplaceStructure(Vector2Int position)
    {
        if(structures[position.x, position.y] != null)
        {
            Destroy(structures[position.x, position.y].gameObject);
        }

        GameObject structureObject = Instantiate(structurePrefabs[0]);
        structureObject.transform.position = GetRealPosition(position);
        structureObject.transform.rotation = currentRotation;
        structures[position.x, position.y] = structureObject.GetComponent<Structure>();
        structures[position.x, position.y].position = position;

    }

    private Structure hiddenStructure;
    private Structure previewStructure;
    private Quaternion currentRotation = Quaternion.identity;

    public void ReplaceStructureTemp(Vector2Int position)
    {
        if(hiddenStructure != null)
        {
            hiddenStructure.gameObject.SetActive(true);
        }

        if (structures[position.x, position.y] != null)
        {
            hiddenStructure = structures[position.x, position.y];
            hiddenStructure.gameObject.SetActive(false);
        }

        if (previewStructure == null)
        {
            GameObject structureObject = Instantiate(structurePrefabs[0]);
            currentRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            structureObject.transform.rotation = currentRotation;
            previewStructure = structureObject.GetComponent<Structure>();
            previewStructure.position = position;
        }

        previewStructure.gameObject.transform.position = GetRealPosition(position);

    }

    public void ResetTempStructure()
    {
        if(hiddenStructure != null)
        {
            hiddenStructure.gameObject.SetActive(true);
        }
        if(previewStructure != null)
        {
            Destroy(previewStructure.gameObject);
            previewStructure = null;
        }
    }

    public void GetPossibleCases(List<CaseInfo> possible, List<CaseInfo> notPossible)
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                (tilesInfos[i, j].IsCasePlantable() ? possible : notPossible).Add(tilesInfos[i, j]);
            }
        }
    }
}
