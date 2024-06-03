using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float timeToExplode = 0.5f;
    public GameObject explosion;

    public float blastRange;
    public LayerMask destructibleLayerMask;

    // Start is called before the first frame update
    void Start()
    {    
    }

    // Update is called once per frame
    void Update()
    {
        timeToExplode -= Time.deltaTime;
        if(timeToExplode <= 0)
        {
            if(explosion) Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);

            List<Collider2D> colliderList = Physics2D.OverlapCircleAll(transform.position, blastRange, destructibleLayerMask).ToList();
            colliderList.ForEach(coll => {
                Destroy(coll.gameObject);
            });
        }
    }
}
