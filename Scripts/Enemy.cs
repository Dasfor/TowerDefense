using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
//, IPointerDownHandler
{
    List<GameObject> wayPoints = new List<GameObject>();
    int wayIndex = 0;
    private Vector3 scale;
    private Vector3 newscale;
    public EnemyG selfEnemy;
    [SerializeField] Text pointText;
    public float TimeToScale = 0f;
    //int upgrade = 0;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = selfEnemy.Spr;
        scale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z); // Проверяем текущий размер
        newscale = scale;
        GetWaypoints();
    }
    void GetWaypoints()
    {
        wayPoints = GameObject.Find("LevelGroup").GetComponent<LevelManagerScr>().wayPoints;
    }
    void Update()
    {
        pointText.text = selfEnemy.Health.ToString();
        Move();
        gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, newscale, Time.deltaTime * selfEnemy.Speed);
        if (TimeToScale <= 0f)
        {
            newscale = new Vector3(0.2f, 0.2f, 0.5f);
            TimeToScale = 1f;
        }
        if (TimeToScale <= 0.5f && TimeToScale > 0f)
        {
            newscale = new Vector3(0.4f, 0.4f, 0.5f);
            TimeToScale -= selfEnemy.Speed * Time.deltaTime;
        }
        else
        {
            TimeToScale -= selfEnemy.Speed * Time.deltaTime;
        }
    }
    /*public void OnPointerDown(PointerEventData eventData)
    {
        health -= 1;
    }*/


    private void Move()
    {
        Transform currWayPoint = wayPoints[wayIndex].transform;
        Vector3 currWayPos = new Vector3(currWayPoint.position.x + currWayPoint.GetComponent<SpriteRenderer>().bounds.size.x / 2,
            currWayPoint.position.y - currWayPoint.GetComponent<SpriteRenderer>().bounds.size.y / 2);

        Vector3 dir = currWayPos - transform.position;
        transform.Translate(dir.normalized * Time.deltaTime * selfEnemy.Speed);

        if (Vector3.Distance(transform.position, currWayPos) < 0.1f)
        {
            if (wayIndex < wayPoints.Count - 1)
                wayIndex++;
            else
                Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        selfEnemy.Health -= damage;
        CheckIsAlive();
    }

    /*public void EnemyUpgrade()
    {
        selfEnemy.Health += upgrade;
        upgrade = upgrade + 1;
        TimeToUpgrade = 2f;
    }*/

    public void CheckIsAlive()
    {
        if (selfEnemy.Health <= 0)
        {
            MoneyManager.Instance.GameMoney += 5;
            Destroy(gameObject);
        }
    }
    public void StartSlow(float duration, float slowValue)
    {
        StopCoroutine("GetSlow");
        selfEnemy.Speed = selfEnemy.StartSpeed;
        StartCoroutine(GetSlow(duration, slowValue));
    }
    IEnumerator GetSlow(float duration, float slowValue)
    {
        selfEnemy.Speed -= slowValue;
        yield return new WaitForSeconds(duration);
        selfEnemy.Speed = selfEnemy.StartSpeed;
    }
    public void AOEDamage(float range, int damage)
    {
        List<Enemy> enemies = new List<Enemy>();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy") )
        {
            if (Vector2.Distance(transform.position, go.transform.position) <= range)
                enemies.Add(go.GetComponent<Enemy>());
        }
        foreach (Enemy es in enemies)
            es.TakeDamage(damage);
    }
}
