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
}
