using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTowerScr : MonoBehaviour
{
    public Cell SelfCell;

    public void Confirm()
    {
        SelfCell.DestroyTower();
        Cancel();
    }

    public void Cancel()
    {
        Destroy(gameObject);
    }
    public void Upgrade()
    {
        SelfCell.UpgradeTower();
        Cancel();
    }
}
