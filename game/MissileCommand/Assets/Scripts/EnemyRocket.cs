using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.GetComponent<RocketController>().EnemyCollision(ID, collision);
    }
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.GetChild(0).transform.position, this.transform.right, detonationRadius);

        if (hit)
        {
            if (hit.transform.gameObject.name.Contains("Explosion"))
            {
                if(hit.transform.gameObject.GetComponent<Explosion>().expanded)
                {
                    gameManager.GetComponent<RocketController>().HitPlayer(ID, hit);
                }
            }
            else
            {
                if (hit.transform.gameObject.name.Contains("Cannon") || hit.transform.gameObject.name.Contains("Generator"))
                {
                    gameManager.GetComponent<GameLogic>().BuildingDestroyed(hit.transform.gameObject.name);
                }
                gameManager.GetComponent<RocketController>().HitPlayer(ID, hit);
            }
        }
        
        Debug.DrawRay(this.transform.GetChild(0).transform.position, this.transform.right, Color.red);
    }
}