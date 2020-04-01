using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public TowerButton ClickedBtn { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickTower( TowerButton towerBtn)
    {
        this.ClickedBtn = towerBtn;
    }

    public void BuyTower()
    {
        ClickedBtn = null;
    }

}
