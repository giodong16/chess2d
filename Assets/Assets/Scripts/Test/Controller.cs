using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public static Controller Instance;
    public GameObject chessPiecePrefab;
    public GameObject moveSquarePrefab;
    public Transform parent;

    private GameObject chessBoard; 
    private GameObject[] blackPieces = new GameObject[16];
    private GameObject[] whitePieces = new GameObject[16];
    private GameObject currentPiece;

    bool isGameOver;
    bool isWhitePlayer = false;

    public GameObject ChessBoard { get => chessBoard; }
    public GameObject CurrentPiece { get => currentPiece; set => currentPiece = value; }
    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    public bool IsWhitePlayer { get => isWhitePlayer; set => isWhitePlayer = value; }

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        chessBoard = GameObject.FindGameObjectWithTag("Board");
        if (chessPiecePrefab == null || ChessBoard == null || moveSquarePrefab == null) return;
        SpawnAllChessPieces();
    }
    private void SpawnAllChessPieces()
    { 
        blackPieces = new GameObject[]
        {
             SpawnChessPiece(ChessPiecesName.BLACK_ROOK,0,0),SpawnChessPiece(ChessPiecesName.BLACK_ROOK,7,0),
             SpawnChessPiece(ChessPiecesName.BLACK_KNIGHT,1,0),SpawnChessPiece(ChessPiecesName.BLACK_KNIGHT,6,0),
             SpawnChessPiece(ChessPiecesName.BLACK_BISHOP,2,0),  SpawnChessPiece(ChessPiecesName.BLACK_BISHOP,5,0),
             SpawnChessPiece(ChessPiecesName.BLACK_QUEEN,3,0),  SpawnChessPiece(ChessPiecesName.BLACK_KING,4,0),

             SpawnChessPiece(ChessPiecesName.BLACK_PAWN,0,1), SpawnChessPiece(ChessPiecesName.BLACK_PAWN,1,1),
             SpawnChessPiece(ChessPiecesName.BLACK_PAWN,2,1),SpawnChessPiece(ChessPiecesName.BLACK_PAWN,3,1),
             SpawnChessPiece(ChessPiecesName.BLACK_PAWN,4,1),SpawnChessPiece(ChessPiecesName.BLACK_PAWN,5,1),
             SpawnChessPiece(ChessPiecesName.BLACK_PAWN,6,1),SpawnChessPiece(ChessPiecesName.BLACK_PAWN,7,1),
        };

        whitePieces = new GameObject[]
        {
             SpawnChessPiece(ChessPiecesName.WHITE_ROOK,0,7),SpawnChessPiece(ChessPiecesName.WHITE_ROOK,7,7),
             SpawnChessPiece(ChessPiecesName.WHITE_KNIGHT,1,7),SpawnChessPiece(ChessPiecesName.WHITE_KNIGHT,6,7),
             SpawnChessPiece(ChessPiecesName.WHITE_BISHOP,2,7),  SpawnChessPiece(ChessPiecesName.WHITE_BISHOP,5,7),
             SpawnChessPiece(ChessPiecesName.WHITE_QUEEN,3,7),  SpawnChessPiece(ChessPiecesName.WHITE_KING,4,7),

             SpawnChessPiece(ChessPiecesName.WHITE_PAWN,0,6), SpawnChessPiece(ChessPiecesName.WHITE_PAWN,1,6),
             SpawnChessPiece(ChessPiecesName.WHITE_PAWN,2,6),SpawnChessPiece(ChessPiecesName.WHITE_PAWN,3,6),
             SpawnChessPiece(ChessPiecesName.WHITE_PAWN,4,6),SpawnChessPiece(ChessPiecesName.WHITE_PAWN,5,6),
             SpawnChessPiece(ChessPiecesName.WHITE_PAWN,6,6),SpawnChessPiece(ChessPiecesName.WHITE_PAWN,7,6),
        };
    }
    public GameObject SpawnChessPiece(ChessPiecesName name ,int matrixX, int matrixY)
    {
        float x  = ChessBoard.GetComponent<Board>().BoardTiles[matrixX, matrixY].transform.position.x;
        float y  = ChessBoard.GetComponent<Board>().BoardTiles[matrixX,matrixY].transform.position.y;
        GameObject chessPieceClone = Instantiate(chessPiecePrefab, new Vector3(x, y, -1), Quaternion.identity, parent);
        GameObject tileClone =  ChessBoard.GetComponent<Board>().BoardTiles[matrixX, matrixY];
        if (tileClone != null) { 
            tileClone.GetComponent<BoardTile>().CurrentChessPiece = chessPieceClone.GetComponent<ChessPiece>();
        }
        chessPieceClone.GetComponent<ChessPiece>().SetUp(name,matrixX,matrixY);
        return chessPieceClone;

    }
    void ClearAllChessPieces()
    {
        if (blackPieces != null)
        {
            for (int i = 0; i < blackPieces.Length; i++)
            {
                Destroy(blackPieces[i]);
            }
        }
        if(whitePieces != null)
        {
            for (int i = 0; i < whitePieces.Length; i++)
            {
                Destroy(whitePieces[i]);
            }
        }
        
    }
    public void SwitchPlayer()
    {
        isWhitePlayer =!isWhitePlayer;
    }
     
    // status

    public void BackToHome()
    {
        SceneManager.LoadScene("Home");
        isGameOver = false;
    }
    public void Pause() {
        Time.timeScale = 0f;
        //show pause dialog

    }
    public void Replay()
    {
        Time.timeScale = 1f;
        isGameOver = false ;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        //Đóng pause dialog
    }

}
