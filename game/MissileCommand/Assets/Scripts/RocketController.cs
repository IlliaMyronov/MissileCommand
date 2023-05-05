using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField] private RocketSpawner spawnerScript;
    [SerializeField] private ExplosionSpawner explosionSpawner;
    [SerializeField] private Vector2 xRocketLaunchRange;
    [SerializeField] private float yRocketLaunch;
    [SerializeField] private Vector2 enemyRocketVelocities;
    [SerializeField] private float playerRocketVelocity;
    [SerializeField] private float enemyRocketExplosionRadius;
    [SerializeField] private AudioSource explosionsound;

    private List<GameObject> enemyRockets;
    private List<GameObject> playerRockets;
    private List<GameObject> explosions;
    private int idCounter;

    private void Awake()
    {
        enemyRockets = new List<GameObject>();
        playerRockets = new List<GameObject>();
        explosions = new List<GameObject>();
        idCounter = 0;
    }

    public void CreateEnemyRocket(Vector3 targetCoordinates)
    {
        GameObject rocketToAdd = spawnerScript.GenerateRocket(targetCoordinates, new Vector2(Random.Range(xRocketLaunchRange.x, xRocketLaunchRange.y), yRocketLaunch), GetRandomValue(enemyRocketVelocities), true);

        rocketToAdd.GetComponent<EnemyRocket>().InitializeRocket(idCounter, enemyRocketExplosionRadius);
        idCounter++;

        enemyRockets.Add(rocketToAdd);
    }

    public void CreatePlayerRocket(Vector3 startCoordinates, Vector3 targetCoordinates)
    {
        GameObject rocketToAdd = spawnerScript.GenerateRocket(targetCoordinates, startCoordinates, playerRocketVelocity, false);

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
                this.AddExplosion(enemyRockets, i);
            }
        }

        for (int i = playerRockets.Count - 1; i > 0; i--)
        {
            if (playerRockets[i].transform.position.y > 10)
            {
                this.AddExplosion(playerRockets, i);
            }
        }

        if(idCounter > 200)
        {
            idCounter = 0;
        }
    }

    public void HitPlayer(int id, RaycastHit2D hit)
    {
        string hitName = hit.transform.gameObject.name;

        if ((hitName.Contains("Explosion") || hitName.Contains("Cannon") || hitName.Contains("Generator")) && !hitName.Contains("Tilemap"))
        {
            for (int i = 0; i < enemyRockets.Count; i++)
            {
                if (enemyRockets[i].GetComponent<EnemyRocket>().ID == id)
                {
                    this.AddExplosion(enemyRockets, i);
                    return;
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
                this.AddExplosion(playerRockets, i);
                return;
            }
        }
    }

    private float GetRandomValue(Vector2 maxMin)
    {
        return Random.Range(maxMin.x, maxMin.y);
    }

    public void ExplosionOver(int id)
    {

        for (int i = 0; i < explosions.Count; i++)
        {
            if (explosions[i].GetComponent<Explosion>().ID == id)
            {
                Destroy(explosions[i]);
                explosions.RemoveAt(i);
            }
        }
    }

    private void AddExplosion(List<GameObject> explodedObjectList, int objectID)
    {
        explosionsound.Play();

        GameObject explosionToAdd = explosionSpawner.CreateExplosion(new Vector2(explodedObjectList[objectID].transform.position.x, explodedObjectList[objectID].transform.position.y));

        explosionToAdd.GetComponent<Explosion>().InitializeExplosion(idCounter);
        idCounter++;
        explosions.Add(explosionToAdd);

        Destroy(explodedObjectList[objectID]);
        explodedObjectList.RemoveAt(objectID);
    }

    public void EnemyCollision(int id, Collision2D collision)
    {
        for (int i = 0; i < enemyRockets.Count; i++)
        {
            if (enemyRockets[i].GetComponent<EnemyRocket>().ID == id)
            {
                this.AddExplosion(enemyRockets, i);
                return;
            }
        }   
    }

    public void PlayerCollision(int id, Collision2D collision)
    {
        for (int i = 0; i < playerRockets.Count; i++)
        {
            if (playerRockets[i].GetComponent<PlayerRocket>().ID == id)
            {
                this.AddExplosion(playerRockets, i);
                return;
            }
        }
    }
}
