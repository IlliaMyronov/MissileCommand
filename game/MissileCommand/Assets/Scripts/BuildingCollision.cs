using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("On collision");

        if(collision.transform.gameObject.layer == 10)
        {
            GameObject.Find("GameManager").GetComponent<GameLogic>().BuildingDestroyed(this.transform.gameObject.name);
        }
    }
}
