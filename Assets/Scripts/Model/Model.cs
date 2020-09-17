using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Model : MonoBehaviour
{
    public const int NORMAL_ROWS = 20;//地图行数
    public const int MAX_ROWS = 23;//地图行数+3
    public const int MAX_COLLUMS = 10;//地图列数
    private Transform[,] map = new Transform[MAX_COLLUMS, MAX_ROWS];

    int score = 0;//该局分数
    int highestScore = 0;//历史最高分
    int times = 0;//游戏次数


    public bool isDataUpdate = false;

    public int Score { get => score;}
    public int HighestScore { get => highestScore;}
    public int Times { get => times;}

    private void Awake()
    {
        LoadData();
    }

    public bool IsValidMapPosition(Transform t)
    {
        foreach (Transform child in t)
        {
            if (child.tag != "Block") continue;
            Vector2 pos = child.position.Round();
            if (IsInsideMap(pos) == false) return false;
            if (map[(int)pos.x, (int)pos.y] != null) return false;
        }
        return true;
    }
    public bool IsGameOver()
    {
        for (int i = NORMAL_ROWS; i < MAX_ROWS; i++)
        {
            for (int j = 0; j < MAX_COLLUMS; j++)
            {
                if (map[j, i] != null)
                {
                    times++;
                    SaveData();
                    return true;
                }
            }
        }
        return false;
    }
    private bool IsInsideMap(Vector2 pos)
    {
        return pos.x >= 0 && pos.x < MAX_COLLUMS && pos.y >= 0;
    }
    public bool PlaceShape(Transform t)
    {
        foreach (Transform child in t)
        {
            if (child.tag != "Block") continue;
            Vector2 pos = child.position.Round();
            map[(int)pos.x, (int)pos.y] = child;
        }
        return CheckMap();
    }
    /// <summary>
    ///检查地图需要消除几行
    /// </summary>
    private bool CheckMap()
    {
        int count = 0;
        for (int i = 0; i < MAX_ROWS; i++)
        {
            bool isFull = CheckIsRowFull(i);
            if (isFull)
            {
                count++;
                DeleteRow(i);
                MoveDownRowsAbove(i + 1);
                i--;
            }
        }
        if (count > 0)
        {
            score += (count * 100);
            if(score > highestScore)
            {
                highestScore = score;
            }
            isDataUpdate = true;
            return true;
        }
        else return false;
    }
    /// <summary>
    /// 检查某一行是否满格
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    private bool CheckIsRowFull(int row)
    {
        for (int i = 0; i < MAX_COLLUMS; i++)
        {
            if (map[i, row] == null) return false;
        }
        return true;
    }
    private void DeleteRow(int row)
    {
        bool canMoveDown = true;
        for (int i = 0; i < MAX_COLLUMS; i++)
        {
            if (map[i, row] != null)
            {
                Transform block = map[i, row];
                map[i, row] = null;
                Destroy(block.gameObject);
            }
        }
    }
    private void MoveDownRowsAbove(int row)
    {
        for (int i = row; i < MAX_ROWS; i++)
        {
            MoveDownRow(i);
        }
    }
    private void MoveDownRow(int row)
    {
        for (int i = 0; i < MAX_COLLUMS; i++)
        {
            if (map[i, row] != null)
            {
                map[i, row - 1] = map[i, row];
                map[i, row] = null;
                map[i, row - 1].position += new Vector3(0, -1, 0);
            }
        }
    }
    void LoadData()
    {
        highestScore = PlayerPrefs.GetInt("HighestScore", 0);
        times = PlayerPrefs.GetInt("Times", 0);
    }
    void SaveData()
    {
        PlayerPrefs.SetInt("HighestScore", highestScore);
        PlayerPrefs.SetInt("Times", times);
    }
    public void Restart()
    {
        for (int i = 0; i < MAX_COLLUMS; i++)
        {
            for (int j = 0; j < MAX_ROWS; j++)
            {
                if (map[i,j] != null)
                {
                    Destroy(map[i, j].gameObject);
                    map[i, j] = null;
                }
            }
        }
        score = 0;
        //map = new Transform[MAX_COLLUMS, MAX_ROWS];
    }
    public void ClearData()
    {
        score = 0;
        highestScore = 0;
        times = 0;
        SaveData();
    }
}
