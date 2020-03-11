using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : Singleton<LevelManager>
{


    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private CameraMovement cameraMovement;

    private Point blueSpawn;

    [SerializeField]
    private GameObject bluePortalPrefab;
    
    public Dictionary<Point, TileScript> Tiles { get; set;}

    public float TileSize /* returns physical size of tile sprite */
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
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

        Tiles = new Dictionary<Point, TileScript>();



        //We only have 3 different types of tiles
        string[] mapData = ReadLeveLText();

        //calculates the X map size
        int mapX = mapData[0].ToCharArray().Length;

        //calculates the Y map size
        int mapY = mapData.Length;

        Vector3 maxTile = Vector3.zero;

        //gets a starting position, a.k.a top left corner
        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        //these 2 for loops allow to cover screen with tiles
        for (int y = 0; y < mapY; y++)
        {

            char[] newTiles = mapData[y].ToCharArray();

            for (int x = 0; x < mapX; x++)
            {
                //call of the helper function that actually places tiles
                PlaceTile( newTiles[x].ToString(), x, y, worldStart);
            }
        }

        //gets the last tile
        maxTile = Tiles[new Point(mapX - 1, mapY -1)].transform.position;
        
        //passing maxTile cordinate (with account of Tile size) so that camera limit can be set
        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));

        SpawnPortals();
    }

    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart) //instantiates tile object and transforms its position (which results in tile being placed)
    {

        int tileIndex = int.Parse(tileType);

        //Creates a new tile and makes a reference to that tile in the new tile variable

        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();
       

        //Uses the new tile variable to change the position of the tile


        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0));
        
      

    }

    private string[] ReadLeveLText(){

        TextAsset bindData = Resources.Load("Level") as TextAsset;


        string data = bindData.text.Replace(Environment.NewLine, string.Empty );

        return data.Split('-');

    }

    //spawns portals
    private void SpawnPortals()
    {
        //spawns portal at given location
        blueSpawn = new Point(0, 0);

        Instantiate(bluePortalPrefab, Tiles[blueSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
    }
}
