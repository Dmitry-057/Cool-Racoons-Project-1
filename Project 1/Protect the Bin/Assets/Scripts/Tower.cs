using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField]
    private string projectileType;

    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private int damage;

    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
    }

    private SpriteRenderer mySpriteRenderer;

    private Monster target;

    public Monster Target
    {
        get { return target; }
    }

    public int Damage
    {
        get 
        { 
            Debug.Log("DAMAGE HERE " + damage);
            return damage; 
        }

    }

    private Queue<Monster> monsters = new Queue<Monster>();

    private bool canAttack = true;

    private float attackTimer;

    [SerializeField]
    private float attackCooldown;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log( "This is the damage from tower start 1 " + damage );
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log( "This is the damage from tower start 2 " + damage );
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log( "This is the damage from tower 1 " + damage );
        Attack();
        
    }

    public void Select() 
    {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
    }

    public void Attack()
    {
        Debug.Log( "This is the damage from tower 2 " + damage );
        if ( !canAttack )
        {
            Debug.Log( "This is the damage from tower 2a " + damage );
            attackTimer += Time.deltaTime;


            if ( attackTimer >= attackCooldown )
            {
                Debug.Log( "This is the damage from tower 2b " + damage );
                canAttack = true;
                attackTimer = 0;
            }
        }
        if ( target == null && monsters.Count > 0 ) 
        {
            Debug.Log( "This is the damage from tower 2bb " + damage );
            target = monsters.Dequeue();
        }
        if ( target != null && target.IsActive )
        {
            Debug.Log( "This is the damage from tower 2bbb " + damage );
            if ( canAttack )
            {
                Debug.Log( "This is the damage from tower 2bbbb " + damage );
                Shoot();

                canAttack = false;
            }
            
        }
        Debug.Log( "This is the damage from tower 2c " + damage );
    }

    private void Shoot()
    {
        Debug.Log( "This is the damage from tower 3 " + damage );
        Projectile projectile = GameManager.Instance.Pool.GetObject( projectileType ).GetComponent<Projectile>();

        projectile.transform.position = transform.position;

        projectile.Initialize(this);
        
    }

    public void OnTriggerEnter2D( Collider2D other )
    {
        if ( other.tag == "Monster" )
        {
            monsters.Enqueue( other.GetComponent<Monster>());
        } 

    }

    public void OnTriggerExit2D( Collider2D other )
    {
        if ( other.tag == "Monster" )
        {
            target =null;
        } 

    }
}
