using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{


    [SerializeField]
    private GameObject towerPrefab;

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private int price;

    [SerializeField]
    private Text priceTxt;

    public int Price
    {
        get 
        {
            return price;
        }
    }
    public GameObject TowerPrefab
    {
        get {
            return towerPrefab;
        }

    }

    public Sprite Sprite
    {
        get {
            return sprite;
        }

    }
    // Start is called before the first frame update
    private void Start()
    {
        priceTxt.text = "$" + price;

        GameManager.Instance.Changed += new CurrencyChanged( PriceCheck );
    }

    private void PriceCheck()
    {
        if ( price <= GameManager.Instance.Currency )
        {
            GetComponent<Image>().color = Color.white;
            priceTxt.color = Color.white;
        }
        else 
        {
            GetComponent<Image>().color = Color.grey;
            priceTxt.color = Color.grey;
        }
    }

    // public void ShowInfo ( string type )
    // {

    //     string tooltip = string.Empty;

    //     switch ( type )
    //     {
    //         case "Dust Bin":
    //             OfficeBinTower bin = towerPrefab.GetComponentInChildren<OfficeBinTower>();
    //             tooltip = string.Format("<size=20><b>Dust Bin</b></size>\nDamage:{0 \nProc: {1}% \nDebuff duration: {2}sec", bin.Damage, bin.Proc, bin.DebuffDuration);
    //             break;

    //         case "Trash Can":
    //             Tower can = towerPrefab.GetComponentInChildren<Tower>();
    //             tooltip = string.Format("<size=20><b>Trash Can</b></size>\nDamage:{0 \nProc: {1}% \nDebuff duration: {2}sec");
    //             break;

    //         case "Dumpster":
    //             DumpsterTower dumpster = towerPrefab.GetComponentInChildren<DumpsterTower>();
    //             tooltip = string.Format("<size=20><b>Dumpster</b></size>\nDamage:{0 \nProc: {1}% \nDebuff duration: {2}sec");
    //             break;
    //     }

    //     GameManager.Instance.SetTooltipText( tooltip );
    //     GameManager.Instance.ShowStats();
    // }
}
