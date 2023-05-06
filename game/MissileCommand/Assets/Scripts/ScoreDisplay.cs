using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;

    private void FixedUpdate()
    {
        this.gameObject.transform.GetChild(1).GetComponent<Text>().text = Mathf.RoundToInt(gameManager.GetComponent<GameLogic>().GetTime()).ToString();
        this.gameObject.transform.GetChild(3).GetComponent<Text>().text = gameManager.GetComponent<GameLogic>().GetScore().ToString();
    }
}
