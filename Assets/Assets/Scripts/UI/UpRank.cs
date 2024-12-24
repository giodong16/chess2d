using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnUpDialog : Dialog
{
    public GameObject upRankPiecePrefab;
    public Transform root;
    public void Show(bool isWhite)
    {
        Clear();
        if (isWhite)
        {
            GameObject queen =  Instantiate(upRankPiecePrefab,root);
            queen.GetComponent<UpRankPiece>().SetUp(isWhite,ChessPiecesName.WHITE_QUEEN);

            GameObject rook = Instantiate(upRankPiecePrefab, root);
            rook.GetComponent<UpRankPiece>().SetUp(isWhite, ChessPiecesName.WHITE_ROOK);

            GameObject bishop = Instantiate(upRankPiecePrefab, root);
            bishop.GetComponent<UpRankPiece>().SetUp(isWhite, ChessPiecesName.WHITE_BISHOP);

            GameObject knight = Instantiate(upRankPiecePrefab, root);
            knight.GetComponent<UpRankPiece>().SetUp(isWhite, ChessPiecesName.WHITE_KNIGHT);
        }
        else
        {
            GameObject queen = Instantiate(upRankPiecePrefab, root);
            queen.GetComponent<UpRankPiece>().SetUp(isWhite, ChessPiecesName.BLACK_QUEEN);

            GameObject rook = Instantiate(upRankPiecePrefab, root);
            rook.GetComponent<UpRankPiece>().SetUp(isWhite, ChessPiecesName.BLACK_ROOK);

            GameObject bishop = Instantiate(upRankPiecePrefab, root);
            bishop.GetComponent<UpRankPiece>().SetUp(isWhite, ChessPiecesName.BLACK_BISHOP);

            GameObject knight = Instantiate(upRankPiecePrefab, root);
            knight.GetComponent<UpRankPiece>().SetUp(isWhite, ChessPiecesName.BLACK_KNIGHT);
        }
        Show();

        
    }
    public void Clear()
    {
        if(root.childCount == 0)  return;
        for (int i = root.childCount - 1; i >= 0; i--)
        {
            Destroy(root.GetChild(i).gameObject);
        }
    }
}
