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
    private Vector3 jump;
    private float realSpeed;
    public float jumpCount;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        rb = GetComponent<Rigidbody>();
        
        jump = new Vector3(0f, 10f, 0f);

        jumpPadForce = new Vector3(0f, 15f, 10f);
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

        // sets the movement in Vector3 for movement
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * realSpeed);
        movementJump();
    }
    private void OnCollisionStay(Collision collision)
    {
        //sets the grounded bool to true
        grounded = true;
        jumpCount = 0;
    }

    private void OnCollisionExit(Collision collision)
    {
        //sets the grounded bool to true
        grounded = false;
    }

    void movementJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount <= 1)
        {
            if (jumpCount == 0)
            {
                jumpCount = 1;
                grounded = false;
                rb.AddForce(jump, ForceMode.Impulse);
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        print("Hitting");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            // You died screen
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
        else if (other.gameObject.CompareTag("JumpPad"))
        {
            Vector3 explosionPos = transform.position;
            rb.AddForce(jumpPadForce, ForceMode.VelocityChange);

        }

        else if (other.gameObject.CompareTag("Ladder"))
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                rb.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime);
            }
        }
    }
}
