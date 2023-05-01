using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocket : MonoBehaviour
{
    private GameObject gameManager;
    private Vector2 targetCoordinates;
    private GameObject me;
    public int ID;

    public void InitializeRocket(Vector2 target, GameObject thisRocket, int id)
    {
        targetCoordinates = target;
        me = thisRocket;
        ID = id;

        gameManager = GameObject.Find("GameManager");
    }

    private void FixedUpdate()
    {
        if (targetCoordinates.y < me.transform.position.y)
        {
            gameManager.GetComponent<RocketController>().ReachedDestination(ID);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.GetComponent<RocketController>().PlayerCollision(ID, collision);
    }
}
