using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public float jumpForce;

    public Transform groundPoint;
    private bool isOnGround;
    public LayerMask whatIsGround;
    public Animator anim;

    public BulletController shotToFire;
    public Transform shotPoint;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // What is GetAxis vs GetAxisRaw
        // GetAxis    = Smoothened
        // GetAxisRaw = No Smoothing
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);

        if(rb.velocity.x < 0) transform.localScale = new Vector3(-1, 1, 1);
        else if(rb.velocity.x > 0) transform.localScale = new Vector3(1, 1, 1);
        
        isOnGround = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatIsGround);

        if(Input.GetButtonDown("Jump") && isOnGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).dir = new Vector2(transform.localScale.x, 0);
            anim.SetTrigger("shotFired");
        }

        anim.SetBool("isOnGround", isOnGround);
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
    }
}
