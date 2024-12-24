using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpRankPiece : MonoBehaviour
{
    bool isWhite;
    public PiecesData[] piecesDatas;
    private ChessPiecesName piecesName;
    public void SetUp(bool white, ChessPiecesName name)
    {
       
        this.isWhite = white;
        piecesName = name;
        this.gameObject.name = "1111";
        if(piecesDatas == null || piecesDatas.Length == 0 ) return;
        for (int i = 0; i < piecesDatas.Length; i++) {
            if (piecesDatas[i] != null && piecesDatas[i].namePiece == name) { 
                this.GetComponent<Image>().sprite = piecesDatas[i].sprite;
                this.GetComponent<Button>().onClick.AddListener(RankUp);
            }
        }
    }
    public void RankUp()
    {
        if (Controller.Instance != null) {
            ChessPiece pawn = Controller.Instance.CurrentPiece.GetComponent<ChessPiece>();
            int mx = pawn.XBoard;
            int my = pawn.YBoard;
            Destroy(pawn.gameObject);
            BoardTile boardTile = Controller.Instance.ChessBoard.GetComponent<Board>().GetBoardTile(mx, my);

            boardTile.CurrentChessPiece =  Controller.Instance.SpawnChessPiece(piecesName, mx, my).GetComponent<ChessPiece>();
        }
        GameObject dialog = GameObject.FindGameObjectWithTag("UpRankDialog");
        if (dialog != null) { 
            dialog.GetComponent<PawnUpDialog>().Close();
        }
    }
}
