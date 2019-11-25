using UnityEngine;
using System.Collections;

public class DoorControl : MonoBehaviour 
{
	//declare animator
    public Animator animator;

	void Start()
	{
        //assign animator
        animator = GetComponent<Animator>();
        animator.SetTrigger("OpenDoor");
        animator.SetTrigger("CloseDoor");
    }

    //on trigger enter, test if the collider is tagged "Player" and set trigger "Open"
    void OnTriggerEnter(Collider other)
    {
        // checks if the object's tag is 'Player'
       if(other.tag == "Player")
        {
            //resets any CloseDoor animation if playing
            animator.ResetTrigger("CloseDoor");
            //plays OpenDoor animation
            animator.SetTrigger("OpenDoor");
        }
    }


    //on trigger exit, test if the collider is tagged "Player" and set trigger "Close"
    private void OnTriggerExit(Collider other)
    {
        // checks if the object's tag is 'Player'
        if (other.tag == "Player")
        {
            //resets OpenDoor animation if playing
            animator.ResetTrigger("OpenDoor");
            //plays ClsoeDoor animation
            animator.SetTrigger("CloseDoor");
        }
    }


}
