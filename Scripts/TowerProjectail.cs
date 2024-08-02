using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectail : MonoBehaviour
{
    Transform target;
    public TowerProjectailG selfProjectail;
    public TowerG selfTower;
    GameController gcs;
    public int NewDmg;

    private void Start()
    {
        gcs = FindObjectOfType<GameController>();
        selfProjectail = gcs.AllProjectails[selfTower.type];
        GetComponent<SpriteRenderer>().sprite = selfProjectail.Spr;
        //NewDmg = selfProjectail.damage;
    }
    void Update()
    {
        Move();
    }

    public void SetTarget(Transform enemy)
    {
        target = enemy;
    }
    private void Move()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) < 0.1f)
            {
                Hit();
            }
            else
            {
                Vector2 dir = target.position - transform.position;
                transform.Translate(dir.normalized * Time.deltaTime * selfProjectail.speed);
            }
        }
        else
            Destroy(gameObject);
    }
    void Hit()
    {
        switch(selfTower.type)
        {
            case (int)TowerType.FIRST_TOWER:
                target.GetComponent<Enemy>().TakeDamage(NewDmg);
                break;
            case (int)TowerType.SECOND_TOWER:
                target.GetComponent<Enemy>().TakeDamage(NewDmg);
                break;
            case (int)TowerType.THIRD_TOWER:
                target.GetComponent<Enemy>().AOEDamage(2, NewDmg);
                break;
            case (int)TowerType.FOUR_TOWER:
                target.GetComponent<Enemy>().StartSlow(2, 1);
                target.GetComponent<Enemy>().TakeDamage(NewDmg);
                break;
        }
        Destroy(gameObject);
    }
}
