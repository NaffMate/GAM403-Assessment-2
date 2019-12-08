using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformMovement : MonoBehaviour
{
    // Transforms to act as start and end markers for the journey.
    public Transform startMarker;
    public Transform endMarker;

    // Movement speed in units per second.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // used to determine which way the platform is moving
    private bool firstRoute = true;

    // Total distance between the markers.
    private float journeyLength;

    void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    // Move to the target end position.
    void Update()
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // checks if the bool firstRoute is true
        if (firstRoute)
        {
            // Set platform position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);
            
            // checks if the platform is at the endMarker.position, within 0.1 unit(s)
            if (Vector3.Distance(transform.position, endMarker.position) <= 0.1f)
            {
                // resets the startTime variable
                startTime = Time.time;
                // sets firstRoute to false, allowing the platform to travel back
                firstRoute = false;
            }

        }

        // checks if the firstRoute bool is NOT true
        else if (!firstRoute)
        {
            // Set platform position as a fraction of the distance between the markers
            transform.position = Vector3.Lerp(endMarker.position, startMarker.position, fractionOfJourney);

            // checks if the platform is at the startMarker.position, within 0.1 unit(s)
            if (Vector3.Distance(transform.position, startMarker.position) <= 0.1f)
            {
                // resets the startTime variable
                startTime = Time.time;
                // sets firstRoute to true, allowing the platform to travel back
                firstRoute = true;
            }
        }
    }
}