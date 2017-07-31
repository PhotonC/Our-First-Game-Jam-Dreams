using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null; // makes manager global

    public int score = 0;
    public int lives = 5;

    public bool paused = false;
    public GameObject pause_menu;

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
	
	// Update is called once per frame
	void Update ()
    {
		if(paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if(Input.GetButtonDown("Start"))
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
	}

    public void unPause()
    {
        paused = false;
        pause_menu.SetActive(false);
    }
}
