﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public TowerButton ClickedBtn { get; set; }



    private int currency;

    private int wave = 0;

    private int lives;

    private bool gameOver = false;

    private int health = 20;

    [SerializeField]
    private Text livesTxt;

    [SerializeField]
    private Text waveTxt;

    [SerializeField]
    private Text currencyTxt;

    [SerializeField]
    private GameObject waveBtn;

    [SerializeField]
    private GameObject gameOverMenu;

    private Tower selectedTower;

    private List<Monster> activeMonsters = new List<Monster>();

    public ObjectPool Pool { get; set; }

    public bool WaveActive
    {
        get { return activeMonsters.Count > 0; }
    }


    public int Currency 
    {
        get 
        {
            return currency;
        }

        set
        {
            this.currency = value;
            this.currencyTxt.text = "<color=lime>$</color>" + value.ToString() ;
        }
    }

    public int Lives 
    {
        get 
        {
            return lives;
        }

        set
        {
            this.lives = value;

            if ( lives <= 0 ) 
            {
                this.lives = 0;
                GameOver();
            }

            
            livesTxt.text = lives.ToString();


        }
    }

    private void Awake ()
    {
        Pool = GetComponent<ObjectPool>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Lives = 10;
        Currency = 100;
    }

    // Update is called once per frame
    void Update()
    {
        HandleEscape();
    }

    public void PickTower( TowerButton towerBtn)
    {

        if (Currency >= towerBtn.Price && !WaveActive)
        {

            //Stores the clicked button 
            this.ClickedBtn = towerBtn;

            //Activates the hover icon
            Hover.Instance.Activate(towerBtn.Sprite);
        }

    }

    public void BuyTower()
    {
        if (Currency >= ClickedBtn.Price)
        {
            Currency -= ClickedBtn.Price;
        }
        Hover.Instance.Deactivate();
    }

    public void SelectTower( Tower tower ) 
    {

        if ( selectedTower != null ) 
        {
            selectedTower.Select();
        }

        selectedTower = tower;
        selectedTower.Select();
    }

    public void DeselectTower()
    {
        if ( selectedTower != null ) 
        {
            selectedTower.Select();
        }

        selectedTower = null;
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }
    }

    public void StartWave()
    {
        wave++;

        waveTxt.text = string.Format("Wave: <color=yellow>{0}</color>", wave);

        StartCoroutine( SpawnWave());

        waveBtn.SetActive(false);
    }

    private IEnumerator SpawnWave ()
    {

        LevelManager.Instance.GeneratePath();

        for(int i =0; i < wave; i++)
        {
            int monsterIndex = 1;//Random.Range( 0, 3 );

            string type = string.Empty;

            switch (monsterIndex)
            {
                case 0:
                    type = "PoopMonster";
                    break;
                case 1:
                    type = "OrangeCrabMonster";
                    break;
                case 2:
                    type = "GreySharkMonster";
                    break;

            }

            Monster monster = Pool.GetObject(type).GetComponent<Monster>();

            monster.Spawn( health );
            Debug.Log("Health is " + health);

            if ( wave % 3 == 0 )
            {
                health += 5; 
            }

            activeMonsters.Add(monster);

            yield return new WaitForSeconds(2.5f);
        }

    }


    public void RemoveMonster(Monster monster)
    {
        activeMonsters.Remove(monster);

        if( !WaveActive && !gameOver )
        {
            waveBtn.SetActive(true);
        }
    }

    public void GameOver() 
    {
        if ( !gameOver ) 
        {
            gameOver = true;
            gameOverMenu.SetActive( true );
        }
    }


    public void Restart() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene( SceneManager.GetActiveScene().name);
    }

    public void QuitGame () 
    {
        Application.Quit();
    }

}
