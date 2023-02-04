using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowardCamera : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Camera.current == null) return;

        transform.rotation = Quaternion.Euler(Quaternion.LookRotation(-Camera.current.transform.position + transform.position,  Camera.current.transform.rotation*Vector3.up).eulerAngles.x,0,0);
    }
}
