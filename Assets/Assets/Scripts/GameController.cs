using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject chessPiecesPrefab;

    private GameObject[,] positions = new GameObject[8,8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    private string currentPlayer = "white";
    private bool isGameOver = false;

    public string CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }

    private void Start()
    {
        playerWhite = new GameObject[]
        {
            CreateChessPieces("white_rook",0,0), CreateChessPieces("white_knight",1,0),
            CreateChessPieces("white_bishop",2,0), CreateChessPieces("white_queen",3,0),
            CreateChessPieces("white_king",4,0), CreateChessPieces("white_bishop",5,0),
            CreateChessPieces("white_knight",6,0),CreateChessPieces("white_rook",7,0),

            CreateChessPieces("white_pawn",0,1),CreateChessPieces("white_pawn",1,1),
            CreateChessPieces("white_pawn",2,1),CreateChessPieces("white_pawn",3,1),
            CreateChessPieces("white_pawn",4,1),CreateChessPieces("white_pawn",5,1),
            CreateChessPieces("white_pawn",6,1),CreateChessPieces("white_pawn",7,1)
        };

        playerBlack = new GameObject[]
        {
            CreateChessPieces("black_rook",0,7), CreateChessPieces("black_knight",1,7),
            CreateChessPieces("black_bishop",2,7), CreateChessPieces("black_queen",3,7),
            CreateChessPieces("black_king",4,7), CreateChessPieces("black_bishop",5,7),
            CreateChessPieces("black_knight",6,7),CreateChessPieces("black_rook",7,7),

            CreateChessPieces("black_pawn",0,6),CreateChessPieces("black_pawn",1,6),
            CreateChessPieces("black_pawn",2,6),CreateChessPieces("black_pawn",3,6),
            CreateChessPieces("black_pawn",4,6),CreateChessPieces("black_pawn",5,6),
            CreateChessPieces("black_pawn",6,6),CreateChessPieces("black_pawn",7,6)
        };

        // set all piece positions on the position board
        for(int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }
    }

    private GameObject CreateChessPieces(string name, int x, int y)
    {
        GameObject gameObject = Instantiate(chessPiecesPrefab,new Vector3(0,0,-1),Quaternion.identity);
        ChessMan chessMan = gameObject.GetComponent<ChessMan>();
        chessMan.name = name;
        chessMan.SetXBoard(x);
        chessMan.SetYBoard(y);
        chessMan.Activate();
        
        return gameObject;
    }
    public void SetPosition(GameObject gameObject) {
        ChessMan chessMan = gameObject.GetComponent<ChessMan>();
        positions[chessMan.GetXBoard(), chessMan.GetYBoard()] = gameObject;
    }


    public GameObject GetPosition(int x, int y) {  
        return positions[x,y];
    }

    public bool PositionOnBoard(int x, int y) { 
        if(x < 0 || y < 0 || x >= positions.GetLength(0) || y>= positions.GetLength(1)) return false;
        return true;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x,y] = null; 
    }

    public bool IsGameOver()
    {
        return isGameOver;

    }
    public void NextTurn()
    {
        if(currentPlayer == "white")
        {
            currentPlayer = "black";
        }
        else
        {
            currentPlayer = "white";
        }
    }
    private void Update()
    {
        if (isGameOver && Input.GetMouseButtonDown(0)) { 
            isGameOver = false;
            SceneManager.LoadScene("GamePlay");
        }
    }

    public void Winner(string playerWinner)
    {
        isGameOver = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWinner + " is the winner";
        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;
    }
}
