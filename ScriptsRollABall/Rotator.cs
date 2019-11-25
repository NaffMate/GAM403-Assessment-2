using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // rotates the pick ups by 15, 30 and 45 in the x, y and z axis respectively over time
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
