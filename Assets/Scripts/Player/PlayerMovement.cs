using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float horizontalInfluence = 1.0f;
    public float jumpCooldownMAX = .3f;
    public float maxWalljumpFallSpeed = -5.0f;
    private float jumpCooldown = 0.0f;


    private CharacterController characterController;
    private Collider collider;

    private Vector3 forward;
    private Vector3 right;
    private Vector3 moveDirection;

    private Transform cameraTransform;
    public Camera camera;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = camera.transform;
        collider = GetComponent<BoxCollider>();

        forward = transform.forward;
        right = transform.right;


        //Debug.Log(forward);
        //Debug.Log(right);
    }

    // Update is called once per frame
    void Update()
    {

        if (characterController.isGrounded && camera.GetComponent<CameraControls>().isDetached())
        {
            moveDirection = new Vector3(0, moveDirection.y, 0);
        }
        

        forward = new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z);
        right = new Vector3 (camera.transform.right.x, 0, camera.transform.right.z);


        if (characterController.isGrounded && !camera.GetComponent<CameraControls>().isDetached()) 
        {
            float directHoriz = Input.GetAxis("Horizontal");
            float directVert = Input.GetAxis("Vertical");

            moveDirection = (forward * directVert) + (right * directHoriz);
            moveDirection.Normalize();
            moveDirection *= speed;

            if (Input.GetButton("Jump") && jumpCooldown <= 0.0f)
            {
                moveDirection.y = jumpSpeed;
                jumpCooldown = jumpCooldownMAX;
            }
        }
        if (jumpCooldown > 0.0f)
        {
            jumpCooldown -= 1 * Time.deltaTime;
        }


        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    }
}
