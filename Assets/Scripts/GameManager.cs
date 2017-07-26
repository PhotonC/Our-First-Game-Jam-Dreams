using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null; // makes manager global

    public int score = 0;
    public int lives = 5;

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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
