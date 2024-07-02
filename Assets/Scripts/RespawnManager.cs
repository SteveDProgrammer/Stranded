using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private Vector3 respawnPoint;
    public float waitToRespawn;

    private GameObject thePlayer;
    public GameObject deathEffect;

    void Start()
    {
        thePlayer = PlayerHealthController.instance.gameObject;
        respawnPoint = thePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpawn(Vector3 pos)
    {
        respawnPoint = pos;
    }
    
    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());    
    }

    private IEnumerator RespawnCoroutine()
    {
        thePlayer.SetActive(false);
        if(deathEffect)
        {
            Instantiate(deathEffect, thePlayer.transform.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(waitToRespawn);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        thePlayer.transform.position = respawnPoint;
        thePlayer.SetActive(true);

        PlayerHealthController.instance.FillHealth();
    }
}
