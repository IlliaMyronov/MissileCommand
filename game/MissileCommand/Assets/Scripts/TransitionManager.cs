using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
	public GameObject mainmenuTransition;
	public GameObject startTransition;
	public GameObject optionsTransition;
	public GameObject quitTransition;
	public GameObject highscoresTransition;
	public GameObject gameoverTransition;
	public Light light1;
	public Light light2;
	public AudioSource transitionSound1;
	public AudioSource transitionSound2;
	public AudioSource select;



	public void OpenMainMenu()
	{
		select.Play();
		transitionSound1.Play();
		mainmenuTransition.SetActive(true);
	}
	public void StartGame()
	{
		select.Play();
		transitionSound1.Play();
		//light1.enabled = true;
		startTransition.SetActive(true);
	}
	public void OpenGameOver()
	{
		transitionSound2.Play();
		gameoverTransition.SetActive(true);
	}
	public void OpenHighScore()
	{
		select.Play();
		transitionSound1.Play();
		highscoresTransition.SetActive(true);
	}
	public void OpenCredits()
	{
		select.Play();
		transitionSound1.Play();
		quitTransition.SetActive(true);
	}
	public void OpenOptions()
	{
		select.Play();
		optionsTransition.SetActive(true);
	}
	public void QuitGame()
	{
		select.Play();
		Debug.Log("QUIT!");
		Application.Quit();
	}



}
