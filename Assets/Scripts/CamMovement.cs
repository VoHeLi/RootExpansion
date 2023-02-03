using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamMovement : MonoBehaviour
{
    public PlayerInput input;
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = input.actions["Move"].ReadValue<Vector2>() * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + inputVector.x, transform.position.y, transform.position.z + inputVector.y);

    }
}
