using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null; // makes manager global

    public bool paused = false;

    public GameObject pause_menu;

//    private int flies = 0;
    private Text flies_counter;
    private Text timer_text;
    private int mins_timer;
    private int seconds_timer;
    private float seconds_ctr;

    void Start()
    {
        //initialization
        mins_timer = 0;
        seconds_ctr = 0;
        seconds_timer = 0;

        flies_counter = GameObject.Find("HUD & UI/Flies Counter/Text").GetComponent<Text>();
        timer_text = GameObject.Find("HUD & UI/Timer Container/Timer").GetComponent<Text>();
        pause_menu = GameObject.Find("HUD & UI/Pause");
    }

    // this is called before start
    private void Awake()
    {
        // check if instance doesn't exist
        if(instance == null)
        {
            instance = this;
        }
        // if instance does exist
        else if(instance != null)
        {
            // destroy current object (we can only have one, after all)
            Destroy(gameObject);
        }

        // this will stop the manager from being destroyed on scene loads
        DontDestroyOnLoad(gameObject);
    }

    // called next?
    void OnEnable()
    {
        print("enabled call");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // scene load call?
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        print("new scene loaded");
        flies_counter = GameObject.Find("HUD & UI/Flies Counter/Text").GetComponent<Text>();
        timer_text = GameObject.Find("HUD & UI/Timer Container/Timer").GetComponent<Text>();
        pause_menu = GameObject.Find("HUD & UI/Pause Menu");

        // reinitialize for new level
        Globals.flies = 0;
        mins_timer = 0;
        seconds_ctr = 0;
        seconds_timer = 0;
    }

    // Update is called once per frame
    void Update ()
    {
        // pause stuff
		if(paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            pause_menu.SetActive(false);
        }

        if (Input.GetButtonDown("Start"))
        {
            if(paused)
            {
                paused = false;
                pause_menu.SetActive(false);
            }
            else
            {
                paused = true;
                pause_menu.SetActive(true);
            }
        }

        // text stuff
        flies_counter.text = Globals.flies.ToString() + " / 5";

        // timer text stuff
        seconds_ctr += Time.deltaTime; 
        if((int)seconds_ctr > seconds_timer)
        {
            seconds_timer++;
        }
        if((int)seconds_ctr >= 60)
        {
            mins_timer++;
            seconds_timer = 0;
            seconds_ctr -= 60.0f;
        }

        timer_text.text = mins_timer.ToString() + ":" + seconds_timer.ToString("D2");
	}

    public void changeScene(string scene_name)
    {
        SceneManager.LoadSceneAsync(scene_name);
    }

    public void unPause()
    {
        paused = false;
        pause_menu.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
