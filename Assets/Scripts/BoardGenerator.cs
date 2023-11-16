using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : SerializedMonoBehaviour
{
    public GameObject cubePrefab; // ����Cube��Ԥ����
    public int width = 8;         // ���̵Ŀ��
    public int height = 8;        // ���̵ĸ߶�

    public GameObject[,] board;  // ���ڴ洢�����ϵ�Cube�Ķ�ά����

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

    // ���������ȡ GameItem
    public GameItem GetObjectAt(int x, int z)
    {
        if (x >= 0 && x < board.GetLength(0) && z >= 0 && z < board.GetLength(1))
        {
            return boardItems[x, z];
        }
        return null;
    }
}
