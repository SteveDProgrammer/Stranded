using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public string levelToLoad;

    public Animator anim;
    public float distanceToOpen;

    private PlayerController thePlayer;
    private bool playerExiting;

    public Transform exitPoint;
    public float movePlayerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = PlayerHealthController.instance.GetComponent<PlayerController>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, thePlayer.transform.position) < distanceToOpen)
        {
            anim.SetBool("isDoorOpen", true);
        }
        else
        {
            anim.SetBool("isDoorOpen", false);
        }

        if(playerExiting)
        {
            thePlayer.transform.position = Vector3.MoveTowards(thePlayer.transform.position, exitPoint.position, movePlayerSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !playerExiting)
        {
            thePlayer.canMove = false;
            StartCoroutine(UseDoorCoroutine());
        }
    }

    IEnumerator UseDoorCoroutine()
    {
        playerExiting = true;
        
        thePlayer.standingAnimator.enabled = false;
        UIController.instance.StartFade();

        yield return new WaitForSeconds(1.5f);
        
        RespawnManager.instance.SetSpawn(exitPoint.position);
        thePlayer.canMove = true;
        thePlayer.standingAnimator.enabled = true;

        UIController.instance.EndFade();
        SceneManager.LoadScene(levelToLoad);
    }
}
