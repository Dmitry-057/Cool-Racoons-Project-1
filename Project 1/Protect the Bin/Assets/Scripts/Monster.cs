using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    [SerializeField]
    private float speed;

    private Stack<Node> path;

    public Point GridPosition { get; set; }

    private Vector3 destination;

    private void Update () 
    {
        Move();
    }

    //spawns the monster in our world
    public void Spawn()
    {
        transform.position = LevelManager.Instance.BluePortal.transform.position;

        StartCoroutine( Scale( new Vector3( 0.1f, 0.1f ), new Vector3( -1, 1 )));

    }

    public IEnumerator Scale ( Vector3 from, Vector3 to ) 
    {
        float progress = 0;

        while ( progress <= 1 ) 
        {
            transform.localScale = Vector3.Lerp( from, to, progress );

            progress += Time.deltaTime;

            yield return null;
        }

        transform.localScale = to;
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards( transform.position, destination, speed * Time.deltaTime);
    
        if ( transform.position == destination ) 
        {
            
        }
    
    }
}
