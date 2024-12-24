using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSquare : MonoBehaviour
{
    Vector2 positionV2;
    bool isAttack = false; // false:move: true: attack(red)
    int xBoard;
    int yBoard;
    public GameObject controller;
    public Controller controllerScript;

    public int YBoard { get => yBoard; set => yBoard = value; }
    public int XBoard { get => xBoard; set => xBoard = value; }

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        controllerScript = controller.GetComponent<Controller>();
    }
    public void SetUp(bool isAttack,int x, int y)
    {
        this.isAttack = isAttack;
        this.xBoard = x;
        this.yBoard = y;
        if (isAttack)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }
    private void OnMouseUp()
    {
        if (controller == null || controllerScript.IsGameOver) return;
       
        int x = controllerScript.CurrentPiece.GetComponent<ChessPiece>().XBoard;
        int y = controllerScript.CurrentPiece.GetComponent<ChessPiece>().YBoard;
        ChessPiece piece = controllerScript.CurrentPiece.GetComponent<ChessPiece>();
        if (isAttack) 
        {
            ChessPiece pieceIsAttacked = controllerScript.ChessBoard.GetComponent<Board>().GetBoardTile(xBoard, yBoard).CurrentChessPiece;
            if (pieceIsAttacked.IsKing())
            {
                controllerScript.IsGameOver = true;
                if (controllerScript.IsWhitePlayer)
                {
                    if (GUIManager.Instance != null)
                    {
                        GUIManager.Instance.gameoverDialog.Show("White Is The Winner!");
                    }
                }
                else
                {
                    if (GUIManager.Instance != null)
                    {
                        GUIManager.Instance.gameoverDialog.Show("Black Is The Winner!");
                    }
                }
                return;
            }
            if (GUIManager.Instance != null) {
                GUIManager.Instance.AddPiece(pieceIsAttacked.GetComponent<SpriteRenderer>().sprite);
            }
            Destroy(pieceIsAttacked.gameObject);
            controllerScript.ChessBoard.GetComponent<Board>().SetEmptyBoardTile(xBoard, yBoard);

        }
        controllerScript.CurrentPiece.GetComponent<ChessPiece>().XBoard = xBoard;
        controllerScript.CurrentPiece.GetComponent<ChessPiece>().YBoard = yBoard;
        controllerScript.CurrentPiece.GetComponent<ChessPiece>().SetCoords();
        controllerScript.ChessBoard.GetComponent<Board>().SetEmptyBoardTile(x, y);
        controllerScript.CurrentPiece.GetComponent<ChessPiece>().DestroyAllMoveSquare();
        controllerScript.ChessBoard.GetComponent<Board>().GetBoardTile(xBoard, yBoard).CurrentChessPiece = piece;

        if(piece.IsPawn() && piece.IsPawnFirstMove)
        {
            piece.IsPawnFirstMove = false;
        }
        if (piece.IsPawn() && (piece.YBoard == 0 || piece.YBoard == 7))
        {
           
            if (GUIManager.Instance != null && GUIManager.Instance.upRankDialog !=null) {
                GUIManager.Instance.upRankDialog.Show(piece.IsWhite);
              
            }
        }
        controllerScript.SwitchPlayer();
    }

}
