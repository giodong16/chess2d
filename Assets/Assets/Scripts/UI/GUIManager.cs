using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance;
    public UIPieces piecesUI01; // lưu white
    public UIPieces piecesUI02; // lưu black

    public PawnUpDialog upRankDialog;
    public GameoverDialog gameoverDialog;

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
        if (piecesUI01 == null || piecesUI02 == null) return;
        piecesUI02.ClearAllPieces();
        piecesUI01.ClearAllPieces();
    }
    public void AddPiece(Sprite sprite)
    {
        if (Controller.Instance != null)
        {
            if (Controller.Instance.IsWhitePlayer)
            {
                piecesUI02.AddPieceUI(sprite);
            }
            else
            {
                piecesUI01.AddPieceUI(sprite);
            }
        }
    }
    
}
