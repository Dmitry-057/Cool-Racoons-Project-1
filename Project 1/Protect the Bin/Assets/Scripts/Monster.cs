using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    [SerializeField]
    private float speed;

    private Stack<Node> path;

    private Animator myAnimator;

    [SerializeField]
    private Stat health;

    public Point GridPosition { get; set; }

    private Vector3 destination;

    public bool IsActive { get; set; }


    public void Awake()
    {
        health.Initialize();
    }

    private void Update () 
    {
        Move();
    }

    //spawns the monster in our world
    public void Spawn ( int health )
    {
        transform.position = LevelManager.Instance.BluePortal.transform.position;

        this.health.MaxVal = health;

        Debug.Log("this.health.MaxVal " + this.health.MaxVal);
        this.health.CurrentVal = this.health.MaxVal;
        Debug.Log("this.health.CurrentVal " + this.health.CurrentVal);

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
    }

    private void Release()
    {
        IsActive = false;
        GridPosition = LevelManager.Instance.BlueSpawn;
        GameManager.Instance.Pool.ReleaseObject(gameObject);
        GameManager.Instance.RemoveMonster(this);
    }

    public void TakeDamage( int damage )
    {
        Debug.Log( "ISACTIVE");
        if ( IsActive )
        {
            Debug.Log( "health.CurrentValbefore " + health.CurrentVal);
            health.CurrentVal -= damage;
            Debug.Log( "health.CurrentValafter " + health.CurrentVal);
        }
        
    }
}
