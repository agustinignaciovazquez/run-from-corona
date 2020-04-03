using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
    private float xOffset;
    
    [SerializeField]private GameObject player;        
    // Initialization
    void Start () 
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        xOffset = transform.position.x - player.transform.position.x;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate ()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        var transform1 = transform;
        var position = transform1.position;
        position = new Vector3(player.transform.position.x + xOffset, position.y, position.z);
        transform1.position = position;
    }
}
