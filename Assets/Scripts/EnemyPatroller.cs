using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] patorlPoints;
    private int currPoint;
    public float moveSpeed;
    public float waitTimeAtPoint;
    public float waitTimeCounter;
    public float jumpForce;

    public Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
