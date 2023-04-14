using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    // prefab of an enemy rocket
    [SerializeField] private GameObject rocketPrefab;

    // starting y position of a rocket
    [SerializeField] private int yStartPosition;

    // min and max of x spawn coordinates
    [SerializeField] private Vector2 xSpawnBoundaries;

    // min and max velocity
    [SerializeField] private Vector2 velocityBoundaries;

    // list to hold all rockets in the game
    private List<GameObject> rocketList;
    private Vector2 dDistance;

    private void Awake()
    {
        // initialize list
        rocketList = new List<GameObject>();
    }
    public void GenerateRocket(Vector3 targetCoordinates)
    {
        // creating a copy of a rocket
        GameObject rocket = Instantiate(rocketPrefab) as GameObject;

        // setting random x position and given y position
        rocket.transform.position = new Vector3(Random.Range(xSpawnBoundaries.x, xSpawnBoundaries.y), yStartPosition, 0);

        // setting angle for rocket to fly at
        rocket.transform.rotation = Quaternion.Euler(0, 0, findRotation(rocket.transform.position, targetCoordinates));

        // picking random velocity
        float velocity = Random.Range(velocityBoundaries.x, velocityBoundaries.y);

        // setting velocity in x and y directions depending on the angle it is supposed to fly
        rocket.GetComponent<Rigidbody2D>().velocity = new Vector2 (Mathf.Cos((rocket.transform.rotation.eulerAngles.z + 270) / 180 * Mathf.PI) * velocity, Mathf.Sin((rocket.transform.rotation.eulerAngles.z + 270) / 180 * Mathf.PI) * velocity);

        // adding this rocket to the list
        rocketList.Add(rocket);
    }

    private void FixedUpdate()
    {

        for(int i  = 0; i < rocketList.Count; i++)
        {
            if (rocketList[i].transform.position.y < yStartPosition * (-1))
            {
                Destroy(rocketList[i]);
                rocketList.RemoveAt(i);

            }
        }
    }

    private float findRotation(Vector3 startPos, Vector3 targetPos)
    {
        dDistance = new Vector2(Mathf.Abs(startPos.x - targetPos.x), Mathf.Abs(startPos.y - targetPos.y));

        if (startPos.x < targetPos.x)
        {
            return (Mathf.Atan(dDistance.x / dDistance.y)) * 180 / Mathf.PI;
        }
        else
        {
            return (Mathf.Atan(dDistance.x / dDistance.y)) * 180 / Mathf.PI * -1;
        }
        
    }
}
