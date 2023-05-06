using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private RocketController rocketController;
    [SerializeField] private Camera cam;
    [SerializeField] private List<GameObject> generatorList;
    [SerializeField] private List<GameObject> turretList;
    [SerializeField] private Vector2 respawnTimeRange;
    [SerializeField] private float maxReloadTime;
    [SerializeField] private float generatorPower;
    [SerializeField] private AudioSource playerfiresound;
    [SerializeField] private float scoreMultiplier;

    private List<GameObject> readyToShoot;
    private float timeSinceLastSpawn;
    private float respawnTime;
    private float reloadTime;
    private float timeSurvived;
    private float score;

    private void Awake()
    {
        timeSinceLastSpawn = 0;
        respawnTime = Random.Range(respawnTimeRange.x, respawnTimeRange.y);
        readyToShoot = new List<GameObject>();

        // ignore collision between enemy rockets
        Physics2D.IgnoreLayerCollision(7, 7);

        // ignore collision between player rockets
        Physics2D.IgnoreLayerCollision(8, 8);

        // ignore collision between player rockets and buildings
        Physics2D.IgnoreLayerCollision(8, 9);

        // calculate initial reloadTime
        reloadTime = maxReloadTime / (generatorList.Count * generatorPower);
        this.UpdateReloadTime();
    }
    private void Update()
    {

        timeSurvived += Time.deltaTime;

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

        if(Input.GetMouseButtonUp(0) && this.IsPossibleToShoot(turretList))
        {
            Vector2 clickPos = new Vector2(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y);

            playerfiresound.Play();

            GameObject shootFrom;

            foreach(GameObject i in turretList)
            {
                if (i.GetComponent<TurretReload>().IsShotReady())
                {
                    readyToShoot.Add(i);
                }
            }

            shootFrom = FindClosest(readyToShoot, clickPos);

            rocketController.CreatePlayerRocket(shootFrom.transform.GetChild(0).transform.position, clickPos);
            shootFrom.GetComponent<TurretReload>().Shot();
            readyToShoot.Clear();
        }

    }

    private GameObject FindClosest(List<GameObject> list, Vector2 coordinates, int element = -1)
    {
        element += 1;
        if(element == list.Count - 1)
        {
            return list[element];
        }
        GameObject nextTop = FindClosest(list, coordinates, element);

        if (FindDistance(list[element].transform.position, coordinates) < FindDistance(nextTop.transform.position, coordinates))
        {
            return list[element];
        }
        else
        {
            return nextTop;
        }
    }

    private float FindDistance(Vector3 obj1, Vector2 obj2)
    {
        return Mathf.Sqrt(Mathf.Pow((obj1.x - obj2.x) , 2) + Mathf.Pow((obj1.y - obj2.y) , 2));
    }

    private bool IsPossibleToShoot(List<GameObject> list)
    {
        foreach(GameObject i in list)
        {

            if (i.GetComponent<TurretReload>().IsShotReady())
                return true;

        }

        return false;
    }

    public void BuildingDestroyed(string name)
    {

        if (name.Contains("Cannon"))
        {

            for(int i = 0; i < turretList.Count; i++)
            {

                if (turretList[i].name == name)
                {
                    Destroy(turretList[i]);
                    turretList.RemoveAt(i);

                    if(turretList.Count == 0)
                    {
                        SceneManager.LoadScene("Level 000");
                        Debug.Log("You managed to achieve score of: " + score + " and you survived " + timeSurvived + " seconds!");
                    }
                }
            }
        }

        else
        {

            for (int i = 0; i < generatorList.Count; i++)
            {

                if (generatorList[i].name == name)
                {
                    Destroy(generatorList[i]);
                    generatorList.RemoveAt(i);

                    if(generatorList.Count > 0)
                    {
                        reloadTime = maxReloadTime / (generatorList.Count * generatorPower);
                    }

                    else
                    {
                        reloadTime = maxReloadTime;
                    }

                    this.UpdateReloadTime();
                }
            }
        }
    }

    private void UpdateReloadTime()
    {
        foreach(GameObject turret in turretList)
        {
            turret.GetComponent<TurretReload>().ChangeReloadTime(reloadTime);
        }
    }

    public void IncreaseScore()
    {
        score += scoreMultiplier;
    }

    public float GetTime()
    {
        return timeSurvived;
    }

    public float GetScore()
    {
        return score;
    }
}
