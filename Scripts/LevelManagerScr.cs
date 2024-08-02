using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManagerScr : MonoBehaviour
{
    public int fieldWighth, fieldHeight;
    public GameObject cellPref;

    public Transform cellParent;
    public Sprite[] tileSpr = new Sprite[3];

    public List<GameObject> wayPoints = new List<GameObject>();
    GameObject[,] allCell = new GameObject[10, 18];
    int currWayX, currWayY;
    GameObject firstCell;
    void Start()
    {
        CreateLevel();
        LoadWaypoints();
    }

    void CreateLevel()
    {
        Vector3 worldVec = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        int number = UnityEngine.Random.Range(1, 5);
        for (int i = 0; i < fieldHeight; i++)
            for (int k = 0; k < fieldWighth; k++)
            {
                int sprIndex = int.Parse(LoadLevelText(number)[i].ToCharArray()[k].ToString());
                Sprite spr = tileSpr[sprIndex];
                bool isGround = spr == tileSpr[1] ? true : false;
                bool buyTower = spr == tileSpr[2] ? true : false;
                CreateCell(buyTower, isGround, spr, k, i, worldVec);
            }
    }
    void CreateCell(bool buyTower, bool isGround, Sprite spr, int x, int y, Vector3 wV)
    {
        GameObject tmpCell = Instantiate(cellPref);

        tmpCell.transform.SetParent(cellParent, false);

        tmpCell.GetComponent<SpriteRenderer>().sprite = spr;
        float sprSizeX = tmpCell.GetComponent<SpriteRenderer>().bounds.size.x;
        float sprSizeY = tmpCell.GetComponent<SpriteRenderer>().bounds.size.y;

        tmpCell.transform.position = new Vector3(wV.x + (sprSizeX * x), wV.y - (sprSizeY * y));
        if (isGround)
        {
            tmpCell.GetComponent<Cell>().isGround = true;
            if (firstCell == null)
            {
                firstCell = tmpCell;
                currWayX = x;
                currWayY = y;
            }
        }
        if (buyTower)
        {
            tmpCell.GetComponent<Cell>().buyTower = true;
        }
            allCell[y, x] = tmpCell;

    }
    string[] LoadLevelText(int number)
    {
        TextAsset tmpTxt = Resources.Load<TextAsset>("Level" + number + "Ground");

        string tmpScr = tmpTxt.text.Replace(Environment.NewLine, string.Empty);

        return tmpScr.Split('!');
    }
    void LoadWaypoints()
    {
        GameObject currWayGO;
        wayPoints.Add(firstCell);

        while (true)
        {
            currWayGO = null;

            if (currWayX > 0 && allCell[currWayY, currWayX - 1].GetComponent<Cell>().isGround &&
                !wayPoints.Exists(x => x == allCell[currWayY, currWayX - 1]))
            {
                currWayGO = allCell[currWayY, currWayX - 1];
                currWayX--;
                Debug.Log("Next Cell is Left");
            }
            else if (currWayX < (fieldWighth - 1) && allCell[currWayY, currWayX + 1].GetComponent<Cell>().isGround &&
               !wayPoints.Exists(x => x == allCell[currWayY, currWayX + 1]))
            {
                currWayGO = allCell[currWayY, currWayX + 1];
                currWayX++;
                Debug.Log("Next Cell is Right");
            }
            else if (currWayY > 0 && allCell[currWayY - 1, currWayX].GetComponent<Cell>().isGround &&
               !wayPoints.Exists(x => x == allCell[currWayY - 1, currWayX]))
            {
                currWayGO = allCell[currWayY-1, currWayX];
                currWayY--;
                Debug.Log("Next Cell is Up");
            }
            else if (currWayX < (fieldHeight - 1) && allCell[currWayY + 1, currWayX].GetComponent<Cell>().isGround &&
               !wayPoints.Exists(x => x == allCell[currWayY + 1, currWayX]))
            {
                currWayGO = allCell[currWayY + 1, currWayX];
                currWayY++;
                Debug.Log("Next Cell is Down");
            }
            else
                break;

            wayPoints.Add(currWayGO);
        }
    }
}
