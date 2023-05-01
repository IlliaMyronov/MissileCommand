using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretReload : MonoBehaviour
{
    private float reloadTime;

    private float timeSinceShot;

    private void Update()
    {
        if(timeSinceShot < reloadTime)
        {
            timeSinceShot += Time.deltaTime;
        }
    }

    public bool IsShotReady()
    {
        if(timeSinceShot > reloadTime)
        {
            return true;
        }

        return false;
    }

    public void Shot()
    {
        timeSinceShot = 0;
    }

    public void ChangeReloadTime(float newTime)
    {
        reloadTime = newTime;
    }
}
