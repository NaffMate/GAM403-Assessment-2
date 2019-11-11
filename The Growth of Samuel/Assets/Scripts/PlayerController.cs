using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //public variables
    public float speed;
    public float sprintSpeed;
    public bool grounded;

    //private variables
    private Rigidbody rb;
    private Vector3 jump;
    private float realSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0f, 10f, 0f);
        grounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        realSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : speed;
    }

    private void FixedUpdate()
    {
        // sets the moveHorizontal and moveVertical variables as the horizontal and vertical inputs respectively
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // sets the movement in Vector3 for the ball movement
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // adds movement to the Rigidbody so the ball can move
        rb.AddForce(movement * realSpeed);
        movementJump();
    }
    private void OnCollisionStay(Collision collision)
    {
        //sets the grounded bool to true
        grounded = true;
    }

    void movementJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            grounded = false;
            rb.AddForce(jump, ForceMode.Impulse);
            print("I'm jumping");
        }
    }
}