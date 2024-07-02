using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController playerController;
    public BoxCollider2D boundsBox;
    private float screenHalfHeight, screenHalfWidth;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        screenHalfHeight = Camera.main.orthographicSize;
        screenHalfWidth = screenHalfHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController != null)
        {
            transform.position = new Vector3(
                Mathf.Clamp(playerController.transform.position.x, boundsBox.bounds.min.x + screenHalfWidth, boundsBox.bounds.max.x - screenHalfWidth), 
                Mathf.Clamp(playerController.transform.position.y, boundsBox.bounds.min.y + screenHalfHeight, boundsBox.bounds.max.y - screenHalfHeight),
                -10);
        }
    }
}
