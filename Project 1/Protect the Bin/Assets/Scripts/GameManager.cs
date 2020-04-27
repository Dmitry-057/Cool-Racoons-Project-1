using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    public TowerButton ClickedBtn { get; set; }



    private int currency;

    [SerializeField]
    private Text currencyTxt;

    public ObjectPool Pool { get; set; }


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

    private void Awake ()
    {
        Pool = GetComponent<ObjectPool>();
    }

    // Start is called before the first frame update
    void Start()
    {

        Currency = 100;
    }

    // Update is called once per frame
    void Update()
    {
        HandleEscape();
    }

    public void PickTower( TowerButton towerBtn)
    {

        if (Currency >= towerBtn.Price)
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

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }
    }

    public void StartWave()
    {
        StartCoroutine( SpawnWave());
    }

    private IEnumerator SpawnWave ()
    {

        LevelManager.Instance.GeneratePath();

        int monsterIndex = 1;//Random.Range( 0, 3 );

        string type = string.Empty;

        switch ( monsterIndex)
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

        Monster monster = Pool.GetObject( type).GetComponent<Monster>();

        monster.Spawn();


        yield return new WaitForSeconds( 2.5f );
    }

}
