using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private RocketController rocketController;
    [SerializeField] private Camera cam;
    [SerializeField] private List<GameObject> generatorList;
    [SerializeField] private List<GameObject> turretList;
    [SerializeField] private Vector2 respawnTimeRange;
    [SerializeField] private float turretRechargeTime;
    

    private float timeSinceLastSpawn;
    private float timeSinceLastShot;
    private float respawnTime;

    private void Awake()
    {
        timeSinceLastSpawn = 0;
        respawnTime = Random.Range(respawnTimeRange.x, respawnTimeRange.y);
    }
    private void Update()
    {
        if(timeSinceLastSpawn < respawnTime)
        {
            timeSinceLastSpawn += Time.deltaTime;
        }
        else
        {
            // need to select a target for a rocket before 
            int building = Mathf.FloorToInt(Random.Range(0, generatorList.Count + turretList.Count));

            if(building >= generatorList.Count)
            {
                building = building - generatorList.Count;
                rocketController.CreateEnemyRocket(turretList[building].transform.position);
            }
            else
            {
                rocketController.CreateEnemyRocket(generatorList[building].transform.position);
            }

            respawnTime = Random.Range(respawnTimeRange.x, respawnTimeRange.y);
            timeSinceLastSpawn = 0;
        }

        if(Input.GetMouseButtonUp(0) && timeSinceLastShot > turretRechargeTime)
        {
            Vector2 clickPos = new Vector2(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y);
            rocketController.CreatePlayerRocket(findClosest(turretList, clickPos).transform.GetChild(0).transform.position, clickPos);
            timeSinceLastShot = 0;
        }

        if(timeSinceLastShot < respawnTime)
        {
            timeSinceLastShot += Time.deltaTime;
        }
    }

    private GameObject findClosest(List<GameObject> list, Vector2 coordinates, int element = -1)
    {
        element += 1;
        if(element == list.Count - 1)
        {
            return list[element];
        }
        GameObject nextTop = findClosest(list, coordinates, element);

        if (findDistance(list[element].transform.position, coordinates) < findDistance(nextTop.transform.position, coordinates))
        {
            return list[element];
        }
        else
        {
            return nextTop;
        }
    }

    private float findDistance(Vector3 obj1, Vector2 obj2)
    {
        return Mathf.Sqrt(Mathf.Pow((obj1.x - obj2.x) , 2) + Mathf.Pow((obj1.y - obj2.y) , 2));
    }
}
