using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RepeatBackground : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject camera;
    private float cameraRelativeOffset = 1f;
    private float backgroundSize;
    private float backgroundLimitOffset;
    
    // Start is called before the first frame update
    void Awake()
    {
        backgroundSize = GetComponent<SpriteRenderer>().size.x; 
        backgroundLimitOffset = backgroundSize + transform.position.x;
        cameraRelativeOffset = camera.transform.position.x - player.transform.position.x;
    }
    
    // Update is called once per frame
    void Update()
    {
        Transform backgroundTransform = transform;
        Vector3 playerPosition = player.transform.position;
        if (playerPosition.x - backgroundLimitOffset > cameraRelativeOffset)
        {
            RepositionBackground(backgroundTransform, playerPosition);
        }
    }

    private void RepositionBackground(Transform backgroundTransform, Vector2 playerPosition)
    {
        Vector2 groundOffset = new Vector2(backgroundSize * 2.0f,0);
        Vector2 newBackgroundPosition = (Vector2) backgroundTransform.position + groundOffset;
        backgroundTransform.position = newBackgroundPosition;
        backgroundLimitOffset = newBackgroundPosition.x + backgroundSize;
    }
}
