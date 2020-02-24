using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tile;

    
    public float TileSize /* returns physical size of tile sprite */
    {
        get { return tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateLevel()
    {
        //gets a starting position, a.k.a top left corner
        Vector3 startPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        //these 2 for loops allow to cover all screen with tiles
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                //call of the helper function that actually places tiles
                PlaceTile(x, y, startPosition);
            }
        }
    }

    private void PlaceTile(int x, int y, Vector3 startPosition) //instantiates tile object and transforms its position (which results in tile being placed)
    {
        GameObject newTile = Instantiate(tile);
        newTile.transform.position = new Vector3(startPosition.x +(TileSize * x), startPosition.y - (TileSize * y), 0);
    }
}
