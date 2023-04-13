using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        rb.velocity = new Vector2(0, -5);
    }
}
