using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        // sets the initial camera position 
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // sets the camera to follow the ball as it moves
        transform.position = player.transform.position + offset;
    }
}
