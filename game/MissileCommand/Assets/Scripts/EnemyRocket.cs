using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocket : MonoBehaviour
{
    private GameObject gameManager;
    private GameObject me;
    public int ID;
    private float detonationRadius;

    // variable added to eliminated errors in explosion of two rockets at the same time
    private float debugDetonationRadius;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        debugDetonationRadius = 2.5f;
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
            Debug.Log(Mathf.Pow(hit.transform.gameObject.transform.position.x - this.transform.GetChild(0).transform.position.x, 2) +
                           Mathf.Pow(hit.transform.gameObject.transform.position.y - this.transform.GetChild(0).transform.position.y, 2));
            if (Mathf.Sqrt(Mathf.Pow(hit.transform.gameObject.transform.position.x - this.transform.GetChild(0).transform.position.x, 2) +
                           Mathf.Pow(hit.transform.gameObject.transform.position.y - this.transform.GetChild(0).transform.position.y, 2)) < debugDetonationRadius)
            {
                gameManager.GetComponent<RocketController>().HitPlayer(ID, hit);
            }
        }
        
        Debug.DrawRay(this.transform.GetChild(0).transform.position, this.transform.right, Color.red);
    }
}