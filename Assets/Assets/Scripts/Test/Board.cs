using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject boardTile;
    private GameObject[,] boardTiles = new GameObject[8,8];
    float startPos = -2.24f;
    float squareSideLength = 0.64f;

    public GameObject[,] BoardTiles { get => boardTiles; set => boardTiles = value; }
    private void Awake()
    {
        if (boardTile == null) return;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                bool isWhite = (i + j) % 2 == 1 ? true : false;
                float x = squareSideLength * i;
                float y = squareSideLength * j;
                x += startPos;
                y += startPos;
                GameObject tileClone = Instantiate(boardTile, new Vector3(x, y, 0), Quaternion.identity, this.transform);
                tileClone.GetComponent<SpriteRenderer>().color = isWhite ? Color.white : Color.black;
                tileClone.name = "square_" + i + "_" + j;
                tileClone.GetComponent<BoardTile>().PositonV2 = new Vector2(x, y);
                BoardTiles[i, j] = tileClone;

            }
        }
    }
    public bool IsOnBoard(int x, int y)
    {
        return (x >=0 && y >= 0 && x<8 && y<8);
    }
    public BoardTile GetBoardTile(int x, int y)
    {
         return BoardTiles[x, y].GetComponent<BoardTile>();
    }
    public Vector2 GetPosition(int x, int y)
    {
        return GetBoardTile(x,y).transform.position;
    }
    public void SetEmptyBoardTile(int x, int y)
    {
        BoardTiles[x,y].GetComponent<BoardTile>().CurrentChessPiece = null; 
    }
}
