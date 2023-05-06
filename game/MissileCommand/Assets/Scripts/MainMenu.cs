using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private AudioSource select;
	[SerializeField] private AudioSource boop;






	public void Boop()
	{
		boop.Play();
	}

	public void OpenMainMenu()
	{
		select.Play();
		SceneManager.LoadScene("Level 000");
	}

	public void StartGame()
	{
		select.Play();
		SceneManager.LoadScene("Level 001");
	}

	public void OpenGameOver()
	{
		select.Play();
		SceneManager.LoadScene("Level 002 GAME OVER");
	}

	public void OpenHighScore()
	{
		select.Play();
		SceneManager.LoadScene("Level 003 HIGH SCORE");
	}

	public void OpenCredits()
	{
		select.Play();
		SceneManager.LoadScene("Level 004 CREDITS");
	}

	public void OpenOptions()
	{
		select.Play();
	}

	public void QuitGame()
	{
		select.Play();
		Debug.Log("QUIT!");
		Application.Quit();
	}



}
