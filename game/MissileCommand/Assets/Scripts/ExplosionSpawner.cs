using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    public GameObject CreateExplosion(Vector2 coordinates)
    {
        GameObject explosion = Instantiate(explosionPrefab) as GameObject;

        explosion.transform.position = coordinates;

        return explosion;
    }
}
