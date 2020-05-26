using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Monster target;

    private Tower parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Damage is projectile " + parent.Damage);
        MoveToTarget();
    }

    public void Initialize ( Tower parent)
    {
        Debug.Log( "This is the damage from tower 4 " + parent.Damage );
        this.target = parent.Target;
        this.parent = parent;

        Debug.Log("THEPARENTIS " + parent);
        Debug.Log( "This is the damage from tower 5 " + parent.Damage );

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

    private void OnTriggerEnter2D( Collider2D other )
    {
        if ( other.tag == "Monster") 
        {

            if ( target.gameObject == other.gameObject )
            {

                //TODO: Fix this bug figure out why it is not taking the Damage
                //parameter from the serialized field.
                Debug.Log("parent.Damage " + parent.Damage);
                target.TakeDamage( parent.Damage );
                GameManager.Instance.Pool.ReleaseObject( gameObject );
            }
            
        }
    }
}
