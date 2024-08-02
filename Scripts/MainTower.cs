using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTower : MonoBehaviour
{
    public Color CurrColor, MainColor;
    public int health = 10;
    public GameObject GameOver;
    [SerializeField] Text pointText;

    void Update()
    {
        pointText.text = health.ToString();
        if (health <= 0)
        {
            Destroy(gameObject);
            GameOver.SetActive(true);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            health -= 1;
            Destroy(other.gameObject);
            StartCoroutine(SetColor());
        }
        else
        {
        }
    }

    IEnumerator SetColor()
    {
        GetComponent<SpriteRenderer>().color = CurrColor;
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().color = MainColor;
    }
}
