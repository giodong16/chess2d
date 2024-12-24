using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile : MonoBehaviour
{
    Vector2 positonV2;
    ChessPiece currentChessPiece;

    public Vector2 PositonV2 { get => positonV2; set => positonV2 = value; }
    public ChessPiece CurrentChessPiece { get => currentChessPiece; set => currentChessPiece = value; }
    public bool IsOccupied()
    {
        return currentChessPiece != null;
    }
}
