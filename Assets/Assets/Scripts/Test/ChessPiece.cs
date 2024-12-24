using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    public PiecesData[] piecesDatas = new PiecesData[12];
    public GameObject moveSquarePrefab;
    public GameObject controller;

    private Controller controllerScript;
    ChessPiecesName pieceName;

    bool isPawnFirstMove = true;
   // Sprite pieceSprite;
    bool isWhite;
    int xBoard = -1;
    int yBoard = -1;

    public int XBoard { get => xBoard; set => xBoard = value; }
    public int YBoard { get => yBoard; set => yBoard = value; }
    public bool IsWhite { get => isWhite; set => isWhite = value; }
    public bool IsPawnFirstMove { get => isPawnFirstMove; set => isPawnFirstMove = value; }
  //  public Sprite PieceSprite { get => pieceSprite; set => pieceSprite = value; }

    public bool IsKing()
    {
        return pieceName == ChessPiecesName.BLACK_KING || pieceName == ChessPiecesName.WHITE_KING;
    }
    public bool IsPawn()
    {
        return pieceName == ChessPiecesName.BLACK_PAWN || pieceName == ChessPiecesName.WHITE_PAWN;
    }
    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        controllerScript = controller.GetComponent<Controller>();
        
    }
    public void SetUp(ChessPiecesName name,int x,int y)
    {
        pieceName = name;
        xBoard = x;
        yBoard = y;
        if (piecesDatas == null || piecesDatas.Length<=0) return;
       

        for (int i = 0; i < piecesDatas.Length; i++) { 
            if(pieceName == piecesDatas[i].namePiece)
            {
                IsWhite = piecesDatas[i].isWhite;
                this.GetComponent<SpriteRenderer>().sprite = piecesDatas[i].sprite;
               // pieceSprite = piecesDatas[i].sprite;
                break;
            }
        }
    }

    public void SetCoords()
    {
        float x = controllerScript.ChessBoard.GetComponent<Board>().GetBoardTile(XBoard, yBoard).transform.position.x;
        float y = controllerScript.ChessBoard.GetComponent<Board>().GetBoardTile(XBoard, yBoard).transform.position.y;

        this.transform.position = new Vector3(x, y, -1);
    }
    private void OnMouseUp()
    {
        if (controller == null || controllerScript.IsGameOver || controllerScript.IsWhitePlayer != isWhite) return;
        controllerScript.CurrentPiece = this.gameObject;
        
        DestroyAllMoveSquare();
        InstantiateMoveSquare();
      
    }


    public void DestroyAllMoveSquare()
    {
        GameObject[] moves = GameObject.FindGameObjectsWithTag("MoveBorder");
        if (moves == null || moves.Length == 0) return;
        for (int i = 0; i < moves.Length; i++)
        {
            Destroy(moves[i]);
        }
    }
    public void InstantiateMoveSquare()
    {
        switch (pieceName)
        {
            case ChessPiecesName.BLACK_KING:
            case ChessPiecesName.WHITE_KING:
                MoveArround();
                break;
            
            case ChessPiecesName.BLACK_QUEEN:
            case ChessPiecesName.WHITE_QUEEN:
                LineMove(0, 1);
                LineMove(0, -1);
                LineMove(1, 0);
                LineMove(-1, 0);

                LineMove(1, 1);
                LineMove(1, -1);
                LineMove(-1, 1);
                LineMove(-1, -1);
                break;

            case ChessPiecesName.BLACK_ROOK:
            case ChessPiecesName.WHITE_ROOK:
                LineMove(0,1);
                LineMove(0, -1);
                LineMove(1, 0);
                LineMove(-1, 0);
                break;

            case ChessPiecesName.BLACK_BISHOP:
            case ChessPiecesName.WHITE_BISHOP:            
                LineMove(1, 1);
                LineMove(1, -1);
                LineMove(-1, 1);
                LineMove(-1, -1);
                break;

            case ChessPiecesName.BLACK_KNIGHT:
            case ChessPiecesName.WHITE_KNIGHT:
                LMove();
                break;

            case ChessPiecesName.BLACK_PAWN:
                PawnMove(xBoard, yBoard + 1); //đen đang ở dưới
                
                break;
            case ChessPiecesName.WHITE_PAWN:
                PawnMove(xBoard, yBoard - 1); //trắng đang ở trên
                break;

            
        }
    }
    void MoveArround()
    {
        if (moveSquarePrefab == null) return;
        PointMove(xBoard + 0, yBoard + 1);
        PointMove(xBoard + 0, yBoard - 1);
        PointMove(xBoard - 1, yBoard + 0);
        PointMove(xBoard + 1, yBoard + 0);

        PointMove(xBoard + 1, yBoard - 1);
        PointMove(xBoard + 1, yBoard + 1);
        PointMove(xBoard - 1, yBoard + 1);
        PointMove(xBoard - 1, yBoard - 1);
    }
    void PointMove(int x,int y) 
    { 
        Board board = controllerScript.ChessBoard.GetComponent<Board>();
        if(board == null || !board.IsOnBoard(x,y)) return;
        float xPos = board.GetBoardTile(x, y).transform.position.x;
        float yPos = board.GetBoardTile(x, y).transform.position.y;
        if (board.GetBoardTile(x,y).IsOccupied() == false)
        {

         
            GameObject moveClone = Instantiate(moveSquarePrefab, new Vector3(xPos, yPos, -2), Quaternion.identity);
            moveClone.GetComponent<MoveSquare>().SetUp(false, x, y);

        }
        else if(board.GetBoardTile(x,y).CurrentChessPiece.IsWhite != isWhite)
        {
            GameObject moveSquareClone = Instantiate(moveSquarePrefab, new Vector3(xPos, yPos, -2), Quaternion.identity);
            moveSquareClone.GetComponent<MoveSquare>().SetUp(true,x,y);
        }
    }
    void LineMove(int x ,int y)
    {
        Board board = controllerScript.ChessBoard.GetComponent<Board>();
        int matrixX = xBoard + x;
        int matrixY = yBoard + y;
        while (board.IsOnBoard(matrixX,matrixY) && !board.GetBoardTile(matrixX, matrixY).IsOccupied())
        {
            PointMove(matrixX,matrixY);
            matrixX += x;
            matrixY += y;
        }
        if(board.IsOnBoard(matrixX,matrixY) && board.GetBoardTile(matrixX,matrixY).CurrentChessPiece.IsWhite != isWhite)
        {
            PointMove(matrixX, matrixY);
        }
    }
    void LMove()
    {
        PointMove(xBoard + 2, yBoard + 1);
        PointMove(xBoard + 2, yBoard - 1);
        PointMove(xBoard - 2, yBoard + 1);
        PointMove(xBoard - 2, yBoard - 1);

        PointMove(xBoard + 1, yBoard + 2);
        PointMove(xBoard + 1, yBoard - 2);
        PointMove(xBoard - 1, yBoard + 2);
        PointMove(xBoard - 1, yBoard - 2);
    }
    void PawnMove(int x, int y)
    {
        Board board = controllerScript.ChessBoard.GetComponent<Board>();
        if (board == null || !board.IsOnBoard(x, y)) return;
        
      
        if (IsPawnFirstMove)
        {
            int v = 1;
            if(y< yBoard) v=-1;
       
            if (board.IsOnBoard(x,y+v) &&!board.GetBoardTile(x, y+v).IsOccupied() && !board.GetBoardTile(x, y).IsOccupied())
            {
                SpawnMoveSquare(board, x, y + v);
            }
        
        }
        if (!board.GetBoardTile(x, y).IsOccupied())
        {
            SpawnMoveSquare(board,x, y);
        }
        if (board.IsOnBoard(x-1, y ) && board.GetBoardTile(x - 1, y).IsOccupied() && isWhite != board.GetBoardTile(x - 1, y).CurrentChessPiece.IsWhite)
        {
            /* GameObject moveClone = Instantiate(moveSquarePrefab, new Vector3(xPos, yPos, -2), Quaternion.identity);
             moveClone.GetComponent<MoveSquare>().SetUp(true, x - 1, y);*/
            SpawnMoveSquare(board, x-1, y,true);
        }
        if (board.IsOnBoard(x+1, y) && board.GetBoardTile(x + 1, y).IsOccupied() && isWhite != board.GetBoardTile(x + 1, y).CurrentChessPiece.IsWhite)
        {
            /* GameObject moveClone = Instantiate(moveSquarePrefab, new Vector3(xPos, yPos, -2), Quaternion.identity);
             moveClone.GetComponent<MoveSquare>().SetUp(true, x + 1, y);*/
            SpawnMoveSquare(board, x+1, y,true);
        }
    }
    public void SpawnMoveSquare(Board board,int x,int y, bool isAttack = false)
    {
        float xPos = board.GetBoardTile(x, y).transform.position.x;
        float yPos = board.GetBoardTile(x, y).transform.position.y;
        GameObject moveClone = Instantiate(moveSquarePrefab, new Vector3(xPos, yPos, -2), Quaternion.identity);
        moveClone.GetComponent<MoveSquare>().SetUp(isAttack, x,y);
    }


}
public enum ChessPiecesName
{
    BLACK_KING,
    WHITE_KING,
    BLACK_QUEEN,
    WHITE_QUEEN,
    BLACK_ROOK,
    WHITE_ROOK,
    BLACK_KNIGHT,
    WHITE_KNIGHT,
    BLACK_BISHOP,
    WHITE_BISHOP,
    BLACK_PAWN,
    WHITE_PAWN,
}

[System.Serializable]
public class PiecesData
{
    public ChessPiecesName namePiece;
    public Sprite sprite;
    public bool isWhite = false;
}