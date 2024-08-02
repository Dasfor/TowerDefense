using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject ItemPref;
    public Transform ItemGrid;
    GameController gcs;
    public Cell selfCell;

    void Start()
    {
        gcs = FindObjectOfType<GameController>();

        foreach(TowerG tower in gcs.AllTowers)
        {
            GameObject tmpItem = Instantiate(ItemPref);
            tmpItem.transform.SetParent(ItemGrid, false);
            tmpItem.GetComponent<ShopItem>().SetStartData(tower, selfCell);
        }
    }
    public void CloseShop()
    {
        Destroy(gameObject);
    }
}
