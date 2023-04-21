using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocket : MonoBehaviour
{
    private GameObject gameManager;
    public int ID;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
    }

    public void InitializeRocket(int id)
    {
        ID = id;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("entered collision");
        gameManager.GetComponent<RocketController>().EnemyHitCollider(collision, ID);
    }
}