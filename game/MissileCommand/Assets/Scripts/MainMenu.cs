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

	public void StartGame()
	{
		select.Play();
		SceneManager.LoadScene("Level 001");
	}

	public void QuitGame()
	{
		select.Play();
		SceneManager.LoadScene("Level 004 CREDITS");
	}

	public void OpenOptions()
	{
		select.Play();
	}






}
