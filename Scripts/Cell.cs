using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Cell : MonoBehaviour
{
    public int state, NewDamage;
    public bool isGround, buyTower, hasTower;
    public Color CurrColor, MainColor;
    public GameObject prefab, ShopPref, TowerPref, DestroyPref, SelfTower;

    void OnMouseEnter()
    {
        if (buyTower && FindObjectsOfType<Shop>().Length == 0
             && FindObjectsOfType<DestroyTowerScr>().Length == 0)
        {
            GetComponent<SpriteRenderer>().color = CurrColor;
        }

        else
        {

        }
    }
    private void OnMouseDown()
    {
        if (!isGround && !hasTower && buyTower && FindObjectsOfType<Shop>().Length == 0
            && FindObjectsOfType<DestroyTowerScr>().Length == 0)
        {
            GameObject shopObj = Instantiate(ShopPref);
            shopObj.transform.SetParent(GameObject.Find("Canvas").transform, false);
            shopObj.GetComponent<Shop>().selfCell = this;
        }
        if (hasTower && FindObjectsOfType<Shop>().Length == 0
            && FindObjectsOfType<DestroyTowerScr>().Length == 0)
        {
            GameObject towrDestr = Instantiate(DestroyPref);
            towrDestr.transform.SetParent(GameObject.Find("Canvas").transform, false);
            towrDestr.GetComponent<DestroyTowerScr>().SelfCell = this;
        }
    }
    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = MainColor;
    }

    public void BuildTower(TowerG tower)
    {
        GameObject tmpTower = Instantiate(TowerPref);
        tmpTower.transform.SetParent(transform, false);

        Vector2 towerPos = new Vector2(transform.position.x + tmpTower.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                                       transform.position.y - tmpTower.GetComponent<SpriteRenderer>().bounds.size.y / 2.3f);
        tmpTower.transform.position = towerPos;
        tmpTower.GetComponent<Tower>().selfType = (TowerType)tower.type;
        SelfTower = tmpTower;
        FindObjectOfType<Shop>().CloseShop();
        hasTower = true;
    }
    public void DestroyTower()
    {
        MoneyManager.Instance.GameMoney += (SelfTower.GetComponent<Tower>().selfTower.Price / 2);
        hasTower = false;
        Destroy(SelfTower);
    }
    public void UpgradeTower()
    {
        if (MoneyManager.Instance.GameMoney >= (SelfTower.GetComponent<Tower>().selfTower.Price + 20) && SelfTower.GetComponent<Tower>().upgr1 == false)
        {
            MoneyManager.Instance.GameMoney -= (SelfTower.GetComponent<Tower>().selfTower.Price + 20);
            SelfTower.GetComponent<Tower>().Upgrade();
            return;
        }
        if (MoneyManager.Instance.GameMoney >= (SelfTower.GetComponent<Tower>().selfTower.Price * 2) && SelfTower.GetComponent<Tower>().upgr2 == false && SelfTower.GetComponent<Tower>().upgr1 == true)
        {
            MoneyManager.Instance.GameMoney -= (SelfTower.GetComponent<Tower>().selfTower.Price * 2);
            SelfTower.GetComponent<Tower>().Upgrade2();
        }
    }
}
