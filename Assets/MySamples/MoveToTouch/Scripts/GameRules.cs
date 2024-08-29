using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class GameRules : MonoBehaviour
{
    [SerializeField]
    GameObject winSign;
    static List<Move2DXY> move2DXYs_Enemy;
    static Move2DXY player;

    static string sceneName;

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        if (winSign != null)
        {
            winSign.SetActive(false);
        }
    }

    void Update()
    {
        if (move2DXYs_Enemy != null)
        {
            Debug.Log(move2DXYs_Enemy.Count);
            if (move2DXYs_Enemy.Count == 0)
            {
                winSign.SetActive(true);
            }
        }

        if (player == null)
        {
            move2DXYs_Enemy.Clear();
            player = null;
            SceneManager.LoadScene(sceneName);

        }

    }

    public static void SetEnemies(Move2DXY move2DXY)
    {
        if (move2DXYs_Enemy == null)
        {
            move2DXYs_Enemy = new List<Move2DXY>();
        }
        move2DXYs_Enemy.Add(move2DXY);
    }

    public static void SetPlayer(Move2DXY move2DXY)
    {
        player = move2DXY;
    }

    internal static void RemoveEnemy(Move2DXY move2DXY)
    {
        move2DXYs_Enemy.Remove(move2DXY);
    }
}
