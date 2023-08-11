using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class HumanGoals : MonoBehaviour
{
    [SerializeField] private List<string> humanGoals;
    private MethodInfo method;
    private string currentGoal;
    private int currentGoalIndex;

    private void Awake()
    {
        currentGoal = string.Empty;
        currentGoalIndex = humanGoals.Count;
    }

    private void FixedUpdate()
    {
        if(currentGoal != string.Empty)
        {
            currentGoalIndex = humanGoals.IndexOf(currentGoal);
        }

        if(currentGoalIndex != 0)
        {

            for (int i = 0; i < currentGoalIndex; i++)
            {

                method = GetType().GetMethod(humanGoals[i]);
                method.Invoke(this, new object[] { });

            }

        }
    }
}
