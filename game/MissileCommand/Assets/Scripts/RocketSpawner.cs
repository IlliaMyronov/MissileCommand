using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    // prefabs of rockets
    [SerializeField] private GameObject enemyRocketPrefab;
    [SerializeField] private GameObject friendlyRocketPrefab;

    // min and max velocity
    [SerializeField] private Vector2 velocityBoundaries;

    public GameObject GenerateRocket(Vector3 targetCoordinates, Vector3 spawnCoordinates, bool isEnemy)
    {
        GameObject rocket = Instantiate(choosePrefab(isEnemy)) as GameObject;
        rocket.transform.position = new Vector3(spawnCoordinates.x, spawnCoordinates.y, 0);

        Vector2 directionVector = new Vector2(targetCoordinates.x - rocket.transform.position.x, targetCoordinates.y - rocket.transform.position.y);
        directionVector = directionVector.normalized;

        float angle = this.findAngle(directionVector);
        rocket.transform.rotation = Quaternion.Euler(0, 0, angle);

        float velocity = Random.Range(velocityBoundaries.x, velocityBoundaries.y);
        rocket.GetComponent<Rigidbody2D>().velocity = directionVector * velocity;
        Debug.Log("Velocity is " + velocity + " direction vector is " + directionVector + " rocket velocity is " + rocket.GetComponent<Rigidbody2D>().velocity);

        return rocket;
    }

    private float findAngle(Vector2 direction)
    {
        // finding raw angle of direction vector

        float angle = Mathf.Atan(Mathf.Abs(direction.y) / Mathf.Abs(direction.x)) / Mathf.PI * 180;

        int quarter = this.findQuarter(direction);

        if(quarter % 2 == 0)
        {
            angle = 90 - angle;
        }

        angle += 90 * (quarter - 1);

        return angle;
    }

    private GameObject choosePrefab(bool isEnemy)
    {
        if(isEnemy)
        {
            return enemyRocketPrefab;
        }
        return friendlyRocketPrefab;
    }

    private int findQuarter (Vector2 direction)
    {
        if(direction.x > 0)
        {
            if(direction.y > 0)
            {
                return 1;
            }

            return 4;
        }

        else
        {
            if(direction.y > 0)
            {
                return 2;
            }

            return 3;
        }
    }
}
