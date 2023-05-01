using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float maxRadius;
    [SerializeField] private float explosionSpeed;

    private float radiusChange;
    private float changeBy;
    public int ID;
    private GameObject gameManager;
    public bool expanded;

    private void Awake()
    {
        radiusChange = maxRadius / explosionSpeed;

        this.transform.localScale = Vector3.zero;

        gameManager = GameObject.Find("GameManager");
    }

    public void InitializeExplosion(int id)
    {
        ID = id;
    }

    public void Start()
    {
        expanded = true;
    }
    private void Update()
    {
        changeBy = explosionSpeed * Time.deltaTime;
        this.transform.localScale = new Vector3(this.transform.localScale.x + changeBy, this.transform.localScale.y + changeBy, 1);

        if(this.transform.localScale.x >= maxRadius)
        {
            gameManager.GetComponent<RocketController>().ExplosionOver(ID);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided");
    }
}
