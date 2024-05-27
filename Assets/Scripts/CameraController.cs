using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController playerController;
    public BoxCollider2D boundsBox;
    private float halfheight, halfWidth;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        halfheight = Camera.main.orthographicSize/2;
        halfWidth = halfheight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController != null)
        {
            transform.position = new Vector3(
                Mathf.Clamp(playerController.transform.position.x, boundsBox.bounds.min.x + halfWidth, boundsBox.bounds.max.x - halfWidth), 
                Mathf.Clamp(playerController.transform.position.y, boundsBox.bounds.min.y + halfheight, boundsBox.bounds.max.y - halfheight),
                -10);
        }
    }
}
