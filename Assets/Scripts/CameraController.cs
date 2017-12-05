using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector2 maxCameraPos;

    private GameObject player;
    private Vector3 offset;
    private Vector2 minCameraPos;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
        minCameraPos = transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;

        float clampedX = Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x);
        float clampedY = Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y);

        Vector3 clampedCameraPos = new Vector3(clampedX, clampedY, transform.position.z);
        transform.position = clampedCameraPos; 
    }
}
