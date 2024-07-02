using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.UI;

public enum FadeState
{
    STARTING, PAUSED, ENDING
}

public class UIController : MonoBehaviour
{
    public static UIController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Slider healthSlider;

    public Image fadeScreen;
    public float fadeSpeed = 2f;
    private FadeState _fadeState = FadeState.PAUSED;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Color c = fadeScreen.color;
        if(_fadeState == FadeState.STARTING)
        {
            fadeScreen.color = new Color(c.r, c.g, c.b, Mathf.MoveTowards(c.a, 1f, Time.deltaTime * fadeSpeed));
        }
        else if(_fadeState == FadeState.ENDING)
        {
            fadeScreen.color = new Color(c.r, c.g, c.b, Mathf.MoveTowards(c.a, 0f, Time.deltaTime * fadeSpeed));
        }

        if(fadeScreen.color.a == 1f || fadeScreen.color.a == 0f) 
            _fadeState = FadeState.PAUSED;
    }

    public void UpdateHealth(int currHelath, int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.minValue = 0;
        healthSlider.value = currHelath;
    }

    public void StartFade()
    {
        _fadeState = FadeState.STARTING;  
    }

    public void EndFade()
    {
        _fadeState = FadeState.ENDING;
    }
}
