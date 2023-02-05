using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamMovement : MonoBehaviour
{
    public PlayerInput input;
    public float speed = 20;

    public Camera cam;
    public float scrollingSpeed = 200;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = input.actions["Move"].ReadValue<Vector2>() * speed * Time.deltaTime;
        transform.position += new Vector3(inputVector.x , 0, inputVector.y);

        float yScrolling = input.actions["Zoom"].ReadValue<float>() * speed * Time.deltaTime;
        cam.fieldOfView += yScrolling * scrollingSpeed * Time.deltaTime * -1;
        if(cam.fieldOfView<5)
        {
            cam.fieldOfView = 5f;
        }else if(cam.fieldOfView > 80)
        {
            cam.fieldOfView = 80;
        }

    }
}
