using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class SceneChanger: MonoBehaviour
{
	public void LoadNextLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void LoadMainMenu()
	{
		SceneManager.LoadScene("Level 000");
	}
	public void LoadFirstLevel()
	{
		SceneManager.LoadScene("Level 001");
	}
	public void LoadOptions()
	{
		SceneManager.LoadScene("Level 005 OPTIONS");
	}
	public void LoadCredits()
	{
		SceneManager.LoadScene("Level 004 CREDITS");
	}
	public void LoadHighScores()
	{
		SceneManager.LoadScene("Level 003 HIGH SCORES");
	}
	public void LoadGameOver()
	{
		SceneManager.LoadScene("Level 002 GAME OVER");
	}

}
