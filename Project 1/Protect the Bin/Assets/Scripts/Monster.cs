

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    [SerializeField]
    private float speed;

    private Stack<Node> path;

    private List<Debuff> debuffs = new List<Debuff>();

    private List<Debuff> debuffsToRemove = new List<Debuff>();

    private List<Debuff> newDebuffs = new List<Debuff>();

    [SerializeField]
    private Element elementType;

    private SpriteRenderer spriteRenderer;

    private int invulnerability = 2;

    private Animator myAnimator;

    [SerializeField]
    private Stat health;

    public bool Alive
    {
        get { return health.CurrentVal > 0; }
    }

    public Point GridPosition { get; set; }

    private Vector3 destination;

    public bool IsActive { get; set; }

    public Element ElementType
    {
        get 
        { 
            return elementType; 
            
        }
    }


    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        health.Initialize();

    }

    private void Update () 
    {
        HandleDebuffs();
        Move();
    }

    //spawns the monster in our world
    public void Spawn ( int health )
    {

        transform.position = LevelManager.Instance.BluePortal.transform.position;

        this.health.Bar.Reset();
        this.health.MaxVal = health;

        this.health.CurrentVal = this.health.MaxVal;

        //This is unused bc we do not have updownrightleft animations Video 7.4
        myAnimator = GetComponent<Animator>();

        StartCoroutine( Scale( new Vector3( 0.1f, 0.1f ), new Vector3( -1, 1 ), false));

        SetPath(LevelManager.Instance.Path);
    }

    public IEnumerator Scale ( Vector3 from, Vector3 to, bool remove) 
    {
        //IsActive = false;     not needed for despawning
        
        float progress = 0;

        while ( progress <= 1 ) 
        {
            transform.localScale = Vector3.Lerp( from, to, progress );

            progress += Time.deltaTime;

            yield return null;
        }

        transform.localScale = to;
        IsActive = true;

        if(remove)
        {
            Release();
        }
    }

    private void Move()
    {
        if (IsActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if (transform.position == destination)//bug found here transform.position was path
            {
                if ( path != null && path.Count > 0) 
                {
                    GridPosition = path.Peek().GridPosition;
                    destination = path.Pop().WorldPosition;
                }
            }
        }
    }

    private void SetPath(Stack<Node> newPath)
    {
        if (newPath != null)
        {
            this.path = newPath;

            GridPosition = path.Peek().GridPosition;
            destination = path.Pop().WorldPosition;
        }
    }

    private void OnTriggerEnter2D ( Collider2D other)
    {
        if(other.tag == "RedPortal")
        {
            StartCoroutine(Scale(new Vector3(1, 1), new Vector3(0.1f, 0.1f), true));
                   
            GameManager.Instance.Lives--;
        }
        if ( other.tag == "Tile" )
        {
            spriteRenderer.sortingOrder = other.GetComponent<TileScript>().GridPosition.Y;
        }
    }

    public void Release()
    {

        debuffs.Clear();

        IsActive = false;
        GridPosition = LevelManager.Instance.BlueSpawn;
        GameManager.Instance.Pool.ReleaseObject(gameObject);
        GameManager.Instance.RemoveMonster(this);
    }

    public void TakeDamage( float damage, Element dmgSource )
    {
        if ( IsActive )
        {
            if ( dmgSource == elementType )
            {
                damage = damage / invulnerability;

                invulnerability++;
            }


            health.CurrentVal = health.CurrentVal - damage;
            if ( health.CurrentVal <= 0)
            {
                GameManager.Instance.Currency += 2;
                Release();
            }
        }
        
    }


    public void AddDebuff( Debuff debuff )
    {
        if ( !debuffs.Exists( x => x.GetType() == debuff.GetType() ))
        {
            newDebuffs.Add( debuff );
        }
        
    }

    public void RemoveDebuff( Debuff debuff )
    {

        debuffsToRemove.Add( debuff );
        
    }


    private void HandleDebuffs()
    {

        if ( newDebuffs.Count > 0)
        {
            debuffs.AddRange( newDebuffs );

            newDebuffs.Clear();
        }

        foreach ( Debuff debuff in debuffsToRemove )
        {
            debuffs.Remove( debuff );
        }

        debuffsToRemove.Clear();

        foreach ( Debuff debuff in debuffs )
        {
            debuff.Update();
        }
    }
}
