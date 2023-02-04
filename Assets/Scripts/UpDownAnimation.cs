using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownAnimation : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 0.5f;
    [SerializeField] private float vecteurdonde = 0.5f;
   
    void Update()
    {
        transform.position = new Vector3(transform.position.x, -amplitude+amplitude * Mathf.Cos(2*Mathf.PI*(frequency*Time.time- vecteurdonde * transform.position.x)), transform.position.z);
    }
}
