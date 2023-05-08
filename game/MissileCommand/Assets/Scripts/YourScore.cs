using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class YourScore : MonoBehaviour
{
	[SerializeField] private GameObject gameManager;

	private void FixedUpdate()
	{
		this.gameObject.transform.GetChild(3).GetComponent<Text>().text = gameManager.GetComponent<GameLogic>().GetScore().ToString();
	}
}
