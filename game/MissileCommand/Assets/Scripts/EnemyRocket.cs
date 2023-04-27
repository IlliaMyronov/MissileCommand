using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocket : MonoBehaviour
{
    private GameObject gameManager;
    private GameObject me;
    public int ID;
    private float detonationRadius;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
    }

    public void InitializeRocket(int id, float detectionDistance)
    {
        ID = id;
        detonationRadius = detectionDistance;
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.GetChild(0).transform.position, this.transform.right, detonationRadius);
        if (hit)
        {
            gameManager.GetComponent<RocketController>().HitPlayer(ID, hit);
        }
        Debug.DrawRay(this.transform.GetChild(0).transform.position, this.transform.right, Color.red);
    }
}