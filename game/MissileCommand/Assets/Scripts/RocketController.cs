using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class RocketController : MonoBehaviour
{
    [SerializeField] private RocketSpawner spawnerScript;
    [SerializeField] private Vector2 xRocketLaunchRange;
    [SerializeField] private float yRocketLaunch;

    private List<GameObject> enemyRockets;
    private List<GameObject> playerRockets;
    private int idCounter;

    private void Awake()
    {
        enemyRockets = new List<GameObject>();
        playerRockets = new List<GameObject>();
        idCounter = 0;
    }

    public void CreateEnemyRocket(Vector3 targetCoordinates)
    {
        GameObject rocketToAdd = spawnerScript.GenerateRocket(targetCoordinates, new Vector2(Random.Range(xRocketLaunchRange.x, xRocketLaunchRange.y), yRocketLaunch), true);

        rocketToAdd.GetComponent<EnemyRocket>().InitializeRocket(idCounter);
        idCounter++;

        enemyRockets.Add(rocketToAdd);
    }

    public void CreatePlayerRocket(Vector3 startCoordinates, Vector3 targetCoordinates)
    {
        GameObject rocketToAdd = spawnerScript.GenerateRocket(targetCoordinates, startCoordinates, false);

        rocketToAdd.GetComponent<PlayerRocket>().InitializeRocket(targetCoordinates, rocketToAdd, idCounter);
        idCounter++;

        playerRockets.Add(rocketToAdd);
    }

    private void FixedUpdate()
    {
        for(int i = enemyRockets.Count - 1; i > 0; i--)
        {
            if (enemyRockets[i].transform.position.y < -10)
            {
                Destroy(enemyRockets[i]);
                enemyRockets.RemoveAt(i);
            }
        }

        for (int i = playerRockets.Count - 1; i > 0; i--)
        {
            if (playerRockets[i].transform.position.y > 10)
            {
                Destroy(playerRockets[i]);
                playerRockets.RemoveAt(i);
            }
        }
    }

    public void EnemyHitCollider(Collision2D collision, int id)
    {
        if (collision.gameObject.name != "Enemy Rocket" && collision.gameObject.name != "Player Rocket")
        {

            for (int i = 0; i < enemyRockets.Count; i++)
            {
                if (enemyRockets[i].GetComponent<EnemyRocket>().ID == id)
                {
                    Destroy(enemyRockets[i]);
                    enemyRockets.RemoveAt(i);
                }
            }
        }
    }

    public void ReachedDestination(int id)
    {
        for (int i = 0; i < playerRockets.Count; i++)
        {
            if (playerRockets[i].GetComponent<PlayerRocket>().ID == id)
            {
                Destroy(playerRockets[i]);
                playerRockets.RemoveAt(i);
            }
        }
    }
}
