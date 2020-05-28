using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpsterTower : Tower
{


    [SerializeField]
    private float tickTime;

    [SerializeField]
    private float tickDamage;

    public float TickTime
    {
        get 
        { 
            return tickTime; 
        }

    }
    public float TickDamage
    {
        get 
        { 
            return tickDamage; 
        }

    }



    private void Start()
    {
        ElementType = Element.SODA;
    }



}
