using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public Vector3 offset;
    public GameObject player;
    private bool detached;
    private CharacterController characterController;
    public float speed;

    public float horizontalTurnSpeed = 2.0f;
    public float verticalTurnSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        detached = false;
        characterController = GetComponent<CharacterController>();

    }

    public bool isDetached()
    {
        return detached;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!detached)
            {
                detached = true;
                Debug.Log("detached");
            }
            
            else
            {
                detached = false;
                Debug.Log("attached");
            }
        }

    }

    // LateUpdate is called once per frame after every other Update()
    void LateUpdate()
    {
        if (!detached)
        {
            transform.position = player.transform.position + offset;
            transform.LookAt(player.transform);
        }
        
        else
        {
            float h = Input.GetAxis("Mouse X") * horizontalTurnSpeed;
            float v = Input.GetAxis("Mouse Y") * verticalTurnSpeed;
            transform.Rotate(0, h, 0, Space.World);

            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalMovement = Input.GetAxis("Vertical");

            Vector3 forward = new Vector3(transform.forward.x, 0, transform.forward.z) * verticalMovement;
            Debug.Log("Forward vector" + forward);
            Vector3 right = transform.right * horizontalMovement;

            Vector3 movementDirection = forward + right;
            movementDirection.Normalize();
            Debug.Log("moving");
            characterController.Move(movementDirection * speed);

        }



    }
}
