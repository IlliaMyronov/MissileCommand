using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    [SerializeField] List<Sprite> growthSteps;
    [SerializeField] List<int> stepTime;

    private int currentStep;
    private int timeSinceGrown;

    private void Awake()
    {
        currentStep = 0;
        timeSinceGrown = 0;
        GetComponent<SpriteRenderer>().sprite = growthSteps[0];
    }

    private void FixedUpdate()
    {
        if(currentStep + 1 < growthSteps.Count)
        {

            if(timeSinceGrown > stepTime[currentStep])
            {
                
                currentStep++;
                GetComponent<SpriteRenderer>().sprite = growthSteps[currentStep];
                timeSinceGrown = 0;

            }

            else
            {
                timeSinceGrown++;
            }
        }
    }
}
