using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretReload : MonoBehaviour
{
    [SerializeField] float reloadTime;

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
}
