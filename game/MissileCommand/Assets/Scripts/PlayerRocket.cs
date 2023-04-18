using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocket : MonoBehaviour
{
    private Vector2 targetCoordinates;
    private GameObject me;
    private Vector2 direction;
    private int ID;

    public void InitializeRocket(Vector2 target, GameObject thisRocket, int id)
    {
        targetCoordinates = target;
        me = thisRocket;
        ID = id;

        direction = me.GetComponent<Rigidbody>().velocity;
        direction = new Vector2 (direction.x / Mathf.Abs(direction.x), direction.y / Mathf.Abs(direction.y));
    }

    private void FixedUpdate()
    {
    }
}
