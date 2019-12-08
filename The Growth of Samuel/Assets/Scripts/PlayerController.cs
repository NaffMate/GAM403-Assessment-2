using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    //public variables
    public float speed;
    public float sprintSpeed;
    public bool grounded;
    public Vector3 jumpPadForce;
        
    //private variables
    private Rigidbody rb;
    private CharacterController controller;
    private Vector3 jump;
    private float realSpeed;
    public float jumpCount;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        // sets the current scene within the sceneName variable
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        // links the rb variable to the linked components Rigidbody
        rb = GetComponent<Rigidbody>();

        // links the controller variable to the linked components Character Controller
        controller = GetComponent<CharacterController>();

        // sets the Vector3 movement of the jump variable
        jump = new Vector3(0f, 10f, 0f);

        // sets the Vector3 movement of the jumpPadForce variable
        jumpPadForce = new Vector3(0f, 15f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        // sets the real speed variable as equal to either the sprintSpeed or speed variables depending on whether the Left Shift Key is pressed
        realSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : speed;
    }

    private void FixedUpdate()
    {
        // sets the moveHorizontal and moveVertical variables as the horizontal and vertical inputs respectively
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // sets the movement in Vector3 for movement
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // adds forfce to the linked component's Rigidbody
        // force is equal to the current movement Vector3 * the current realSpeed variable
        rb.AddForce(movement * realSpeed);

        // calls the movementJump function
        movementJump();
    }

    // function that checks whether the linked component is grounded
    private void OnCollisionStay(Collision collision)
    {
        //sets the grounded bool to true
        grounded = true;
        jumpCount = 0;
        if (collision.gameObject.name == "Platform")
        {
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //sets the grounded bool to true
        grounded = false;
    }

    // allows the player to jump upon press of the Space Key
    void movementJump()
    {
        // checks if the player has pressed the Space Key and whether the jumpCount variable is less than or equal to 1
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount <= 1)
        {
            // checks if the jumpCount variable is equal to 0
            if (jumpCount == 0)
            {
                // sets jumpCount to 1
                jumpCount = 1;
                // sets that the player is no longer grounded as jumping has commenced
                grounded = false;
                // adds force to the linked components Rigidbody
                // this force is equal to the jump Vector3 and is implemented via the Impulse method
                rb.AddForce(jump, ForceMode.Impulse);
            }
        }
    }

    // this does not work, not sure why
    // checks when the Controller Collider has hit anything
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // checks if the character controller is NOT grounded and if the normal of the wall hit has an angle of less than 0.1
        if (!controller.isGrounded && hit.normal.y < 0.1f)
        {
            // checks for Space Key input
            if (Input.GetKey(KeyCode.Space))
            {
                // draws a ray in red for 1.25 seconds at the hit point on the normal
                Debug.DrawRay(hit.point, hit.normal, Color.red, 1.25f);
                // adds force to the player
                rb.AddForce(jump, ForceMode.Impulse);
                // sends the player in the direction of the normal after jumping off said wall
                transform.Translate(hit.normal * speed);
            }
        }
    }
    
    // checks when a collider enters another collider
    private void OnTriggerEnter(Collider other)
    {
        // checks if the player has hit an object tagged "Obstacle"
        if (other.gameObject.CompareTag("Obstacle"))
        {
            // You died screen code goes here //

            //  checks for Space Key press by player
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // reloads the currently loaded scene
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
            // checks for Escape Key press by player
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Closes the application window
                Application.Quit();
            }
        }
        // checks if the player has collided with an object tagged "Respawn"
        // this refers to an invisible plane over drop points so the player can respawn 
        // also does not work
        else if (other.gameObject.CompareTag("Respawn"))
        {
            // reloads the current scene
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        // checks if the player has hit an object tagged "JumpPad"
        else if (other.gameObject.CompareTag("JumpPad"))
        {
            //  adds force to the linked component's Rigidbody
            // uses the Vector3 jumpPadForce and uses the Velocity Change method to deliver said force
            rb.AddForce(jumpPadForce, ForceMode.VelocityChange);

        }

        // this does not work, uncertain as to why
        // checks if the player has hit an object tagged "Ladder"
        else if (other.gameObject.CompareTag("Ladder"))
        {
            // checks whether the player has held down the W Key after colliding with an object tagged "Ladder"
            if (Input.GetKeyDown(KeyCode.W))
            {
                // Moves the player upwards at a speed of 1 unit over time
                rb.transform.Translate(new Vector3(0, 1, 0));
            }
        }
        // does not work, will not parent for some reason. May be due to not referring to the platform itself.
        // checks if the player has hit an object tagged "Platform"
        else if (other.gameObject.CompareTag("Platform"))
        {
            // sets the player object as a parent of the object tagged "Parent"
            other.transform.parent = transform;
        }
    }
}


