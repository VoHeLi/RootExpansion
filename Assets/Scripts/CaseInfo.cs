using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseInfo : MonoBehaviour
{
    public Vector2Int casePos;

    private MeshRenderer meshRenderer;

    public void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void SetOutline(Material material)
    {
        Debug.Log(meshRenderer.ToString());

        Material[] mats = meshRenderer.sharedMaterials;
        mats[1] = material;
        meshRenderer.materials = mats;

        //meshRenderer.sharedMaterials[1] = material;
    }
}
