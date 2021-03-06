﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{

	// Use this for initialization
	void Start () {
        GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnStartGameClicked()
    {
        GameManager.Instance.ResetLevelNumber();
        SceneManager.LoadScene("GameScene");
    }

    public void OnCreditsClicked()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnExitGameClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
