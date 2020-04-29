using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    [SerializeField]
    private float speed;

    private Stack<Node> path;

    private Animator myAnimator;

    public Point GridPosition { get; set; }

    private Vector3 destination;

    public bool IsActive { get; set; }

    private void Update () 
    {
        Move();
    }

    //spawns the monster in our world
    public void Spawn()
    {
        transform.position = LevelManager.Instance.BluePortal.transform.position;

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
            Destroy(gameObject);
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


    // private void Animate( Point currentPos, Point newPos ) //This is unused bc we do not have updownrightleft animations Video 7.4
    // {
    //     if ( currentPos.Y > newPos.Y ) 
    //     {
    //         //We are moving down
    //     } 
    //     else if ( currentPos.Y < newPos.Y ) 
    //     {
    //         //Then we are moving up
    //     }
    //     if ( currentPos.Y == newPos.Y ) 
    //     {
    //         if ( currentPos.X > newPos.X) 
    //         {
    //             //Move ot left
    //         }
    //         else 
    //         {
    //             //Move to right
    //         }
    //     }
    // }

    private void OnTriggerEnter2D ( Collider2D other)
    {
        if(other.tag == "RedPortal")
        {
            StartCoroutine(Scale(new Vector3(1, 1), new Vector3(0.1f, 0.1f), true));
                   
        }
    }
}
