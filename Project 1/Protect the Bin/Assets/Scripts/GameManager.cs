using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    public TowerButton ClickedBtn { get; set; }



    private int currency;

    private int wave = 0;

    [SerializeField]
    private Text waveTxt;

    [SerializeField]
    private Text currencyTxt;

    [SerializeField]
    private GameObject waveBtn;

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

            monster.Spawn();

            activeMonsters.Add(monster);

            yield return new WaitForSeconds(2.5f);
        }

    }


    public void RemoveMonster(Monster monster)
    {
        activeMonsters.Remove(monster);

        if(!WaveActive)
        {
            waveBtn.SetActive(true);
        }
    }
}
