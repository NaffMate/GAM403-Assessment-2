using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // public variables 
    public float speed;
    public float winOffset;
    public bool grounded;
    public Text countText;
    public Text winText;
    public Text timerText;

    // private variables
    private Vector3 jump;
    private Rigidbody rb;
    private int count;
    private float time;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        //sets the currentScene variable as the active scene
        Scene currentScene = SceneManager.GetActiveScene();
        //sets the sceneName variable as the active scene
        sceneName = currentScene.name;
        //declares the rb variable as the rigidbody of the player
        rb = GetComponent<Rigidbody>();
        //declares the jump motion
        jump = new Vector3(0.0f, 5f, 0.0f);
        // declares the initial count to 0 at the start of the level
        count = 0;
        // calls the SetCountText function
        SetCountText();
        // sets the winText text box to display nothing
        winText.text = "";
        // sets the time to 0 
        time = 0f;
        
            
    }

    // Update is called once per frame
    void Update()
    {
        // calls the set time function
        setTime();
    }

    // Update is called before Physics calculations
    void FixedUpdate()
    {
        // sets the moveHorizontal and moveVertical variables as the horizontal and vertical inputs respectively
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // sets the movement in Vector3 for the ball movement
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // adds movement to the Rigidbody so the ball can move
        rb.AddForce(movement * speed);
        // calls the jump function
        movementJump();
    }

    private void OnTriggerEnter(Collider other)
    {
        // checks if the entity the ball collided with has a 'Pick Up' 
        if (other.gameObject.CompareTag("Pick Up"))
        {
            // deactivates the pick up
            other.gameObject.SetActive(false);
            // increases counter by 1
            count = count + 1;
            SetCountText();
        }
        // checks if the entity the ball collided with has a tag 'Boost Pick Up' 
        else if (other.gameObject.CompareTag("Boost Pick Up"))
        {
            // deactivates the pick up
            other.gameObject.SetActive(false);
            // increases movement in a given direction
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            rb.AddForce(movement * 500);
        }
        else if (other.gameObject.CompareTag("Bounds"))
        {
            // resets the level if the player jumps over the boundaries
            SceneManager.LoadScene("Level2");
        }
    }

    // checks whether the player is grounded as to allow them to jump
    private void OnCollisionStay(Collision collision)
    {
        //sets the grounded bool to true
        grounded = true;
    }

    // sets the countText text box when the player collects 12 pick ups
    void SetCountText()
    { 
        countText.text = "Count: " + count.ToString();
        // displays text if the loaded scene is NOT level 2
        if ((count >= 12) && sceneName != "Level2")
        {
            winText.text = "You Win!";
        }
        // displays text if the loaded scene is level 2
        else if ((count >= 12) && sceneName == "Level2")
        {
            winText.text = "You Win! Press the Escape Key to Exit";
        }
    }
   
    //  Function used for keeping track of game time
    void setTime()
    {
        if (count < 12)
        {
            // begins tracking time once the level has loaded
            time = Time.timeSinceLevelLoad;
            // rounds the time counter to the nearest 2 decimal places
            time = Mathf.Round(time * 100) / 100;
            // sets the timerText text box to display the time
            timerText.text = "Timer: " + time;
        }
        // checks if the current level time is greater than the time + winOffset variable
        else if (Time.time > time + winOffset)
        {
            // checks if the current level is level 2
            if (sceneName == "Level2")
            {
                // calls the exitGame function
                exitGame();
            }
            // loads the second level if the current level is not level 2
            else SceneManager.LoadScene("Level2");
        }
    }
    // function that allows the player to jump
    void movementJump()
    {
        // checks for inpput of the space key and whether the grounded bool is true
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            // applies force to the player model in an upwards motion, allowing a jump
            rb.AddForce(jump, ForceMode.Impulse);
            // sets the grounded bool to false, stopping the player from jumping again
            grounded = false;
        }
    }
    // exit game function, allows the player to exit the game
    void exitGame()
    {
        // closes the game on input of the escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}