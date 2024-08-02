using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TowerG
{
    public float range, Cooldown, CurrCooldown;
    public int type, Price;
    public string Name;
    public Sprite Spr;

    public TowerG(string Name, string path, int type, float range, float cd, int Price)
    {
        Spr = Resources.Load<Sprite>(path);
        this.Name = Name;
        this.Price = Price;
        this.type = type;
        this.range = range;
        this.Spr = Spr;
        Cooldown = cd;
        CurrCooldown = 0;
    }
}

public struct TowerProjectailG
{
    public float speed;
    public int damage;
    public Sprite Spr;

    public TowerProjectailG(string path, float speed, int dmg)
    {
        Spr = Resources.Load<Sprite>(path);
        this.Spr = Spr;
        this.speed = speed;
        damage = dmg;
    }
}

public struct EnemyG
{
    public float Speed, StartSpeed;
    public int Health;
    public Sprite Spr;

    public EnemyG(string path, int health, float speed)
    {
        Spr = Resources.Load<Sprite>(path);
        this.Spr = Spr;
        StartSpeed = Speed = speed;
        Health = health;
    }
}

public struct BossG
{
    public float Speed, StartSpeed;
    public int Health;
    public Sprite Spr;

    public BossG(string path, int health, float speed)
    {
        Spr = Resources.Load<Sprite>(path);
        this.Spr = Spr;
        StartSpeed = Speed = speed;
        Health = health;
    }
}

public enum TowerType
{
    FIRST_TOWER,
    SECOND_TOWER,
    THIRD_TOWER,
    FOUR_TOWER
}


public class GameController : MonoBehaviour 
{
    public List<TowerG> AllTowers = new List<TowerG>();
    public List<TowerProjectailG> AllProjectails = new List<TowerProjectailG>();
    public List<EnemyG> AllEnemies = new List<EnemyG>();
    public List<EnemyG> AllBoss = new List<EnemyG>();

    private void Awake()
    {
        AllTowers.Add(new TowerG("Damage", "TowerSprites/1", 0, 4f, 2f, 30));
        AllTowers.Add(new TowerG("Speed", "TowerSprites/2", 1, 2f, 0.4f, 40));
        AllTowers.Add(new TowerG("Splash", "TowerSprites/3", 2, 2f, 2f, 50));
        AllTowers.Add(new TowerG("Snow", "TowerSprites/4", 3, 2f, 0.4f, 30));

        AllProjectails.Add(new TowerProjectailG("ProjectailSprites/1", 10f, 8));
        AllProjectails.Add(new TowerProjectailG("ProjectailSprites/2", 6f, 2));
        AllProjectails.Add(new TowerProjectailG("ProjectailSprites/3", 6f, 6));
        AllProjectails.Add(new TowerProjectailG("ProjectailSprites/4", 6f, 1));

        AllEnemies.Add(new EnemyG("EnemySprites/1", 10, 3f));
        AllEnemies.Add(new EnemyG("EnemySprites/2", 8, 2f));
        AllEnemies.Add(new EnemyG("EnemySprites/3", 12, 1f));
        AllEnemies.Add(new EnemyG("EnemySprites/4", 7, 5f));

        AllBoss.Add(new EnemyG("EnemySprites/5", 300, 1f));
    }
}
