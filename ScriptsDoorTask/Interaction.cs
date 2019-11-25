using UnityEngine;
using System.Collections;

public class Interaction : MonoBehaviour 
{
    //declare public variables for light, lightModel, particle systems, fan model
    public Light lightSource;
    public GameObject lightModel, fanModel;
    public ParticleSystem PS1, PS2;

    private bool fanSpin;
	
	// Update is called once per frame
	void Update () 
	{

        //if fan rotation is on, transform.rotate
        if (fanSpin)
        {
            fanModel.transform.Rotate(new Vector3(-5, 0, 0));
        }

        //if mouse button is down
        if (Input.GetMouseButtonDown(0))
        {
            //setup ray from camera to clicked mouseposition.
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            //if Physics.Raycast
            //https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
            // Look for Raycast with Input.mousePosition
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawRay(transform.position, transform.forward * 50, Color.white, 0.25f);
                //if ray hits game object tagged "Light"
                if (hit.collider.tag == "Light")
                {
                    if (fanSpin == true)
                    {
                        //turn on rotation (called in main Update function)
                        fanSpin = false;
                        //change light model material to green
                        lightModel.GetComponent<MeshRenderer>().material.color = Color.red;
                        //change point light color to green
                        lightSource.color = Color.red;
                        //SetActive both particle systems
                        PS1.gameObject.SetActive(false);
                        PS2.gameObject.SetActive(false);
                    }
                    else
                    {
                        //turn on rotation (called in main Update function)
                        fanSpin = true;
                        //change light model material to green
                        lightModel.GetComponent<MeshRenderer>().material.color = Color.green;
                        //change point light color to green
                        lightSource.color = Color.green;
                        //SetActive both particle systems
                        PS1.gameObject.SetActive(true);
                        PS2.gameObject.SetActive(true);
                    }
                }
            }
        }


    

        
        
       
        
    }

}
