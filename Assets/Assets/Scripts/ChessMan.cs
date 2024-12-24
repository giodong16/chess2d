using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessMan : MonoBehaviour
{
    public GameObject controller;
    public GameObject moveBorderPrefab;

    //position
    private int xBoard = -1;
    private int yBoard = -1;

    // black or white
    private string player;

    //
    public Sprite black_king,black_queen, black_knight, black_rook, black_bishop, black_pawn; // bishop:tượng, knight:mã
    public Sprite white_king,white_queen,white_knight, white_rook, white_bishop, white_pawn;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        //take the instantiated location and adjust the transform
        SetCoords();

        switch (this.name)
        {
            case "black_queen": 
                this.GetComponent<SpriteRenderer>().sprite = black_queen;
                player = "black";
                break;
            case "black_king":  
                this.GetComponent<SpriteRenderer>().sprite = black_king;
                player = "black";
                break;
            case "black_bishop":
                this.GetComponent<SpriteRenderer>().sprite = black_bishop;
                player = "black";
                break;
            case "black_rook":
                this.GetComponent<SpriteRenderer>().sprite = black_rook;
                player = "black";
                break;
            case "black_knight":
                this.GetComponent<SpriteRenderer>().sprite = black_knight;
                player = "black";
                break;
            case "black_pawn":
                this.GetComponent<SpriteRenderer>().sprite = black_pawn;
                player = "black";
                break;

            case "white_queen":
                this.GetComponent<SpriteRenderer>().sprite = white_queen;
                player = "white";
                break;
            case "white_king":
                this.GetComponent<SpriteRenderer>().sprite = white_king;
                player = "white";
                break;
            case "white_bishop":
                this.GetComponent<SpriteRenderer>().sprite = white_bishop;
                player = "white";
                break;
            case "white_rook":
                this.GetComponent<SpriteRenderer>().sprite = white_rook;
                player = "white";
                break;
            case "white_knight":
                this.GetComponent<SpriteRenderer>().sprite = white_knight;
                player = "white";
                break;
            case "white_pawn":
                this.GetComponent<SpriteRenderer>().sprite = white_pawn;
                player = "white";
                break;
        }
    }

    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 0.64f;
        y *= 0.64f;

        x += -2.24f;
        y += -2.24f;

        this.transform.position = new Vector3(x, y, -1f);
    }

    public int GetXBoard()
    {
        return xBoard;
    }
    public int GetYBoard()
    {
        return yBoard;
    }
    public void SetXBoard(int x)
    {
        this.xBoard = x;
    }
    public void SetYBoard(int y) { 
        this.yBoard = y;
    }

    private void OnMouseUp()
    {
        if (!controller.GetComponent<GameController>().IsGameOver()&& controller.GetComponent<GameController>().CurrentPlayer == player)
        {
            DestroyMovePlates();
            InitiateMoveBorders();
        }
    

    }

    public void InitiateMoveBorders()
    {
        switch (this.name) {
            case "black_queen":
            case "white_queen":
                LineMoveBorder(1, 0);
                LineMoveBorder(-1, 0);
                LineMoveBorder(0, 1);
                LineMoveBorder(0, -1);
                LineMoveBorder(1, 1);
                LineMoveBorder(-1, 1);
                LineMoveBorder(1, -1);
                LineMoveBorder(-1, -1);
                break;

            case "black_king":
            case "white_king":
                SurroundMoveBorder();
                break;

            case "black_bishop":
            case "white_bishop":
                LineMoveBorder(1, 1);
                LineMoveBorder(-1, 1);
                LineMoveBorder(1, -1);
                LineMoveBorder(-1, -1);
                break;

            case "black_rook":
            case "white_rook":
                LineMoveBorder(1, 0);
                LineMoveBorder(-1,0);
                LineMoveBorder(0,1);
                LineMoveBorder(0,-1);
                break;

            case "black_knight":
            case "white_knight":
                LMoveBorder();
                /*LineMoveBorder(1, 2);
                LineMoveBorder(1, -2);
                LineMoveBorder(-1, 2);
                LineMoveBorder(-1, -2);

                LineMoveBorder(2,1);
                LineMoveBorder(-2, 1);
                LineMoveBorder(-2, -1);
                LineMoveBorder(2, -1);*/
                break;

            case "black_pawn":
                PawnMoveBorder(xBoard,yBoard-1);
                break;

            case "white_pawn":
                PawnMoveBorder(xBoard, yBoard+1);
                break;
        }
    }

    private void PawnMoveBorder(int x, int y)
    {
        GameController gameController = controller.GetComponent<GameController>();
        if (gameController.PositionOnBoard(x, y))
        {
            if (gameController.GetPosition(x, y) == null)
            {
                MoveBorderSpawn(x, y);
            }

            if(gameController.PositionOnBoard(x+1,y) && gameController.GetPosition(x+1,y) !=null && gameController.GetPosition(x+1, y).GetComponent<ChessMan>().player != player)
            {
                MoveBorderAttackSpawn(x + 1, y);
            }
            if (gameController.PositionOnBoard(x - 1, y) && gameController.GetPosition(x - 1, y) != null && gameController.GetPosition(x - 1, y).GetComponent<ChessMan>().player != player)
            {
                MoveBorderAttackSpawn(x - 1, y);
            }
        }
    }

  

    private void LMoveBorder()
    {
        PointMoveBorder(xBoard + 1,yBoard + 2);
        PointMoveBorder(xBoard - 1, yBoard + 2);
        PointMoveBorder(xBoard + 1, yBoard - 2);
        PointMoveBorder(xBoard - 1, yBoard - 2);

        PointMoveBorder(xBoard + 2, yBoard + 1);
        PointMoveBorder(xBoard + 2, yBoard - 1);
        PointMoveBorder(xBoard - 2, yBoard + 1);
        PointMoveBorder(xBoard - 2, yBoard - 1);
    }

    private void SurroundMoveBorder()
    {
        PointMoveBorder(xBoard + 0, yBoard + 1);
        PointMoveBorder(xBoard + 0, yBoard - 1);
        PointMoveBorder(xBoard - 1, yBoard + 0);
        PointMoveBorder(xBoard + 1, yBoard + 0);

        PointMoveBorder(xBoard + 1, yBoard - 1);
        PointMoveBorder(xBoard + 1, yBoard + 1);
        PointMoveBorder(xBoard - 1, yBoard + 1);
        PointMoveBorder(xBoard - 1, yBoard - 1);

    }

    private void PointMoveBorder(int x, int y)
    {
         GameController gameController = controller.GetComponent<GameController>();
        if (gameController.PositionOnBoard(x, y)) { 
            GameObject obj = gameController.GetPosition(x, y);
            if(obj == null)
            {
                MoveBorderSpawn(x, y);

            }
            else if(obj.GetComponent<ChessMan>().player != player)
            {
                MoveBorderAttackSpawn(x, y);
            }
        }
    }

    private void LineMoveBorder(int x,int y)
    {
        GameController gameController = controller.GetComponent<GameController>();
        int vx = xBoard + x;
        int vy = yBoard + y;

        while (gameController.PositionOnBoard(vx,vy) && gameController.GetPosition(vx,vy)== null)
        {  
            MoveBorderSpawn(vx,vy);
            vx = vx+x;
            vy = vy+y;
        }
        if(gameController.PositionOnBoard(vx, vy) && gameController.GetPosition(vx,vy).GetComponent<ChessMan>().player != player){
            MoveBorderAttackSpawn(vx,vy);
        }
    }


    private void MoveBorderAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;
        x *= 0.64f;
        y *= 0.64f;

        x += -2.24f;
        y += -2.24f;

        GameObject moveBorder = Instantiate(moveBorderPrefab, new Vector3(x, y, -3f), Quaternion.identity);
        MoveBorder mClass = moveBorder.GetComponent<MoveBorder>();
        mClass.isAttack = true;
        mClass.Reference = gameObject;
        mClass.SetCoords(matrixX, matrixY);
    }

    private void MoveBorderSpawn(int matrixX, int matrixY, bool isAttack = false)
    {
        float x = matrixX;
        float y = matrixY;
        x *= 0.64f;
        y *= 0.64f;

        x += -2.24f;
        y += -2.24f;

        GameObject moveBorder = Instantiate(moveBorderPrefab, new Vector3(x, y, -3f), Quaternion.identity);
        MoveBorder mClass = moveBorder.GetComponent<MoveBorder>();
        mClass.Reference = gameObject;
        mClass.SetCoords(matrixX, matrixY);
    }
    public void DestroyMovePlates()
    {
        GameObject[] moveBorders = GameObject.FindGameObjectsWithTag("MoveBorder");
        for (int i = 0; i < moveBorders.Length; i++) { 
            Destroy(moveBorders[i]);
        }
    }

}

