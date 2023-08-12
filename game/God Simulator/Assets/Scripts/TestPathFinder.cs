using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestPathFinder : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject pathFinder;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private Camera mainCamera;

    private Vector2Int goalCoordinates;
    private Stack<Vector2Int> goalPath;

    private void Awake()
    {
        goalPath = new Stack<Vector2Int>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            goalCoordinates = new Vector2Int(Mathf.RoundToInt(mainCamera.ScreenToWorldPoint(Input.mousePosition).x), 
                                             Mathf.RoundToInt(mainCamera.ScreenToWorldPoint(Input.mousePosition).y));
            Debug.Log(gameManager.GetComponent<WorldManager>().GetMap()[goalCoordinates.y][goalCoordinates.x].name);
            
            goalPath = pathFinder.GetComponent<PathFinder>().FindPath(new Vector2Int(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y)), 
                                                                      goalCoordinates, gameManager.GetComponent<WorldManager>().GetMap());

            Debug.Log(goalPath.Count);
        }

        if(goalPath.Count > 0)
        {
            this.MoveTowards(goalPath.Peek());
        }

    }

    private void MoveTowards(Vector2Int goal)
    {
        Vector2 direction = goal - new Vector2(this.transform.position.x, this.transform.position.y);

        if(Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2)) < speed * Time.deltaTime)
        {
            this.transform.position = new Vector3(goal.x, goal.y, this.transform.position.z);
            goalPath.Pop();
            return;
        }

        direction = direction.normalized;
        this.transform.position = new Vector3(this.transform.position.x + (direction.x * speed * Time.deltaTime), this.transform.position.y + (direction.y * speed * Time.deltaTime), this.transform.position.z);
    }
}
