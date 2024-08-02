using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject projectail;
    public TowerG selfTower;
    public TowerProjectailG selfProjectail;
    public TowerType selfType;
    public bool upgr1 = false;
    public bool upgr2 = false;
    GameController gcs;

    private void Start()
    {
        gcs = FindObjectOfType<GameController>();
        selfTower = gcs.AllTowers[(int)selfType];
        GetComponent<SpriteRenderer>().sprite = selfTower.Spr;
        selfProjectail = gcs.AllProjectails[selfTower.type];
        InvokeRepeating("SearchTarget", 0, 0.1f);
    }

    private void Update()
    {
        if (selfTower.CurrCooldown > 0)
            selfTower.CurrCooldown -= Time.deltaTime;
    }
    bool CanShoot()
    {
        if (selfTower.CurrCooldown <= 0)
            return true;
        return false;
    }

    void SearchTarget()
    {
        if (CanShoot())
        {
            Transform nearestEnemy = null;
            float nearestEnemyDistance = Mathf.Infinity;

            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                float CurrDistance = Vector2.Distance(transform.position, enemy.transform.position);

                if (CurrDistance < nearestEnemyDistance &&
                    CurrDistance <= selfTower.range)
                {
                    nearestEnemy = enemy.transform;
                    nearestEnemyDistance = CurrDistance;
                }
            }
            if (nearestEnemy != null)
                Shoot(nearestEnemy);
        }
    }
    public void Upgrade()
    {
        upgr1 = true;
    }
    public void Upgrade2()
    {
        upgr2 = true;
    }
    void Shoot(Transform enemy)
    {
        selfTower.CurrCooldown = selfTower.Cooldown;
        GameObject proj = Instantiate(projectail);
        if (upgr2 == true && upgr1 == true)
        {
            proj.GetComponent<TowerProjectail>().NewDmg = selfProjectail.damage + 4;
        }
        if (upgr1 == true && upgr2 == false)
        {
            proj.GetComponent<TowerProjectail>().NewDmg = selfProjectail.damage + 2;
        }
        if (upgr1 == false && upgr2 == false)
        {
            proj.GetComponent<TowerProjectail>().NewDmg = selfProjectail.damage;
        }

        proj.GetComponent<TowerProjectail>().selfTower = selfTower;
        proj.transform.position = transform.position;
        proj.GetComponent<TowerProjectail>().SetTarget(enemy);
    }
}
