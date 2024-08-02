using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ShopItem : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    TowerG selfTower;
    Cell selfCell;
    public Image icon;
    public Text TowerName, TowerPrice;
    public Color CurrColor, MainColor;

    public void SetStartData(TowerG tower, Cell cell)
    {
        selfTower = tower;
        TowerPrice.text = tower.Price.ToString();
        TowerName.text = tower.Name;
        selfCell = cell;
        icon.sprite = tower.Spr;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = CurrColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = MainColor;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(MoneyManager.Instance.GameMoney >= selfTower.Price)
        {
            selfCell.BuildTower(selfTower);
            MoneyManager.Instance.GameMoney -= selfTower.Price;
        }
    }
}
