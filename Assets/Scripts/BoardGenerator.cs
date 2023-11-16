using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : SerializedMonoBehaviour
{
    public GameObject cubePrefab; // 设置Cube的预制体
    public int width = 8;         // 棋盘的宽度
    public int height = 8;        // 棋盘的高度

    public GameObject[,] board;  // 用于存储棋盘上的Cube的二维数组

    [TableMatrix(SquareCells =true)]
    public GameItem[,] boardItems=new GameItem[8,8];

    [OnInspectorInit]
    private void CreateData()
    {
        boardItems=new GameItem[width,height];
    }

    void Start()
    {
        GenerateBoard();
    }

    void GenerateBoard()
    {
        board = new GameObject[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GameObject cube = Instantiate(cubePrefab, new Vector3(x, 0, z), Quaternion.identity);
                board[x, z] = cube;
                cube.transform.parent = transform;
            }
        }
    }

    public Vector2 GetBoardSize()
    {
        return new Vector2(width, height);
    }

    // 根据坐标获取 GameItem
    public GameItem GetObjectAt(int x, int z)
    {
        if (x >= 0 && x < board.GetLength(0) && z >= 0 && z < board.GetLength(1))
        {
            return boardItems[x, z];
        }
        return null;
    }
}
