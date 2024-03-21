using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offest = new Vector3(0, 0, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    private void Update()
    {
        if (target != null)
        {
            // Get the current position of the camera
            Vector3 newPosition = transform.position;

            // Only update the X position of the camera to follow the player
            newPosition.x = target.position.x + offest.x;

            // Apply the new position to the camera with smoothing
            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
        }
    }
}

