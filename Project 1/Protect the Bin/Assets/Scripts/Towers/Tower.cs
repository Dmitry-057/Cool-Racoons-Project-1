using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element { SODA, NONE }

public abstract class Tower : MonoBehaviour
{

    [SerializeField]
    private string projectileType;

    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private int damage;

    [SerializeField]
    private float debuffDuration;


    [SerializeField]
    private float proc;

    public Element ElementType { get; protected set; }

    public int Price { get; set; }

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
            return damage; 
        }

    }

    public float DebuffDuration
    {
        get 
        { 
            return debuffDuration; 
        }
        set
        {
            this.debuffDuration = value;
        }

    }

    public float Proc
    {
        get 
        { 
            return proc; 
        }
        set
        {
            this.proc = value;
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
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        
    }

    public void Select() 
    {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
    }

    public void Attack()
    {
        if ( !canAttack )
        {
            attackTimer += Time.deltaTime;


            if ( attackTimer >= attackCooldown )
            {
                canAttack = true;
                attackTimer = 0;
            }
        }
        if ( target == null && monsters.Count > 0 && monsters.Peek().IsActive ) 
        {
            target = monsters.Dequeue();
        }
        if ( target != null && target.IsActive )
        {
            if ( canAttack )
            {
                Shoot();

                canAttack = false;
            }
            
        }

        if ( target != null && !target.Alive || target != null && !target.IsActive )
        {
            target = null;
        }

    }

    private void Shoot()
    {
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

    public abstract Debuff GetDebuff();

    public void OnTriggerExit2D( Collider2D other )
    {
        if ( other.tag == "Monster" )
        {
            target =null;
        } 

    }
}
