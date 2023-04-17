using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField] private RocketSpawner spawnerScript;
    [SerializeField] private Vector2 xRocketLaunchRange;
    [SerializeField] private float yRocketLaunch;

    private List<GameObject> enemyRockets;
    private List<GameObject> playerRockets;

    private void Awake()
    {
        enemyRockets = new List<GameObject>();
        playerRockets = new List<GameObject>();
    }

    public void CreateEnemyRocket(Vector3 targetCoordinates)
    {
        enemyRockets.Add(spawnerScript.GenerateRocket(targetCoordinates, new Vector2(Random.Range(xRocketLaunchRange.x, xRocketLaunchRange.y), yRocketLaunch), true));
    }

    public void CreatePlayerRocket(Vector3 startCoordinates, Vector3 targetCoordinates)
    {
        playerRockets.Add(spawnerScript.GenerateRocket(targetCoordinates, startCoordinates, false));
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
}
