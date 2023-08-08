using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureStats : MonoBehaviour
{
    private float myHealth;
    private float myHunger;
    [SerializeField] private int maxHealth;
    [SerializeField] private int maxHunger;
    [SerializeField] private float hungerRate;

    private void Awake()
    {
        myHealth = maxHealth;
        myHunger = maxHunger;
    }
    public void TakeDamage(float dmg)
    {
        myHealth -= dmg;
    }
}
