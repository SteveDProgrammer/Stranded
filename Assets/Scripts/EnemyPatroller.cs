using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currPoint;
    public float moveSpeed;
    public float waitTimeAtPoint;
    public float waitTimeCounter;
    public float jumpForce;

    public Rigidbody2D rb2d;
    public Animator anim;

// Start is called before the first frame update
    void Start()
    {
        currPoint = 0;
        waitTimeCounter = waitTimeAtPoint;
        foreach (Transform p in patrolPoints)
        {
            p.SetParent(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.x - patrolPoints[currPoint].position.x) > .2f)
        {
            if(transform.position.x < patrolPoints[currPoint].position.x) // To the left of point
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            }
            else
            {
                transform.localScale = Vector3.one;
                rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
            }

            if((transform.position.y - patrolPoints[currPoint].position.y) < -0.5f && rb2d.velocity.y < 0.1f)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            }
        }
        else
        {
            rb2d.velocity = new Vector2(0f, rb2d.velocity.y);
            waitTimeCounter -= Time.deltaTime;
            if(waitTimeCounter <= 0)
            {
                waitTimeCounter = waitTimeAtPoint;
                
                currPoint++;
                currPoint %= patrolPoints.Length;
            }
        }
        anim.SetFloat("speed", rb2d.velocity.x);
    }
}
