﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Monster target;

    private Tower parent;

    [SerializeField]
    private Element elementType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
    }

    public void Initialize ( Tower parent)
    {
        this.target = parent.Target;
        this.parent = parent;
        this.elementType = parent.ElementType;



    }

    private void MoveToTarget()
    {
        if ( target != null && target.IsActive )
        {
            transform.position = Vector3.MoveTowards( transform.position, target.transform.position, Time.deltaTime * parent.ProjectileSpeed );
        }
        else if ( !target.IsActive )
        {
            GameManager.Instance.Pool.ReleaseObject( gameObject );
        }
    }

    
    private void ApplyDebuff()
    {
        if ( target.ElementType != elementType)
        {
            float roll = Random.Range( 0, 100 );


        }

    }

    private void OnTriggerEnter2D( Collider2D other )
    {
        if ( other.tag == "Monster" ) 
        {

            if ( target.gameObject == other.gameObject )
            {
                target.TakeDamage( parent.Damage, elementType );
                GameManager.Instance.Pool.ReleaseObject( gameObject );

                ApplyDebuff();
            }
            
        }
    }

}
