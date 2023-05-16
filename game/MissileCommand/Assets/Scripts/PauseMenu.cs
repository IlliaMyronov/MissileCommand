using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public GameObject pauseMenu;
	public AudioSource select;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
												//
												// FIND A WAY TO TURN THE MUSIC DOWN
												// WHILE THE GAME IS PAUSED!!!!!!!!!
												//
			select.Play();
			PauseUnpause();
		}
	}

	public void PauseUnpause()
	{
		if (!pauseMenu.activeInHierarchy)
		{
			pauseMenu.SetActive(true);
			Time.timeScale = 0f;
		}
		else
		{
			pauseMenu.SetActive(false);
			Time.timeScale = 1f;
		}
	}











}
