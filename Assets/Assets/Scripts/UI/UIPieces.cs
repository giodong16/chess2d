using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPieces : MonoBehaviour
{
    public GameObject pieceUIPrefab;

    public void AddPieceUI(Sprite sprite)
    {
         GameObject pieceUIClone = Instantiate(pieceUIPrefab,this.gameObject.transform);
         pieceUIClone.GetComponent<PieceUI>().SetUp(sprite);
    }
    public void ClearAllPieces()
    {
        if (transform.childCount == 0) return;
        for (int i = transform.childCount-1; i >=0 ; i--) { 
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
    }
    
}
