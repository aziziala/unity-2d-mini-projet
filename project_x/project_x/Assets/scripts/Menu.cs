﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Jouer ()
    {
        SceneManager.LoadScene("levels");
    }

    public void Level1()
    {
        SceneManager.LoadScene("sc1");
    }
}
