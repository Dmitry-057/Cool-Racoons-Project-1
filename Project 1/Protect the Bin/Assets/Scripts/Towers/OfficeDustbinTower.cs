﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeBinTower : Tower
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

    public override Debuff GetDebuff() 
   {
       return new SodaDebuff( tickDamage, tickTime, DebuffDuration, Target );
   }


}