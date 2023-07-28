using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class YourScore : MonoBehaviour
{

	private void Awake()
	{
		this.gameObject.transform.GetChild(2).GetComponent<Text>().text = GameLogic.score.ToString();
	}
}
