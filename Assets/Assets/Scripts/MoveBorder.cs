using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBorder : MonoBehaviour
{
    public GameObject gameController;
    GameObject reference = null;

    int matrixX;
    int matrixY;

    // false: moverment, true: attacking
    public bool isAttack = false;
    public Sprite scale;
    public Sprite square;

    public GameObject Reference { get => reference; set => reference = value; }

    public void Start()
    {
        if (isAttack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,0f,0f,1f);
        }
    }

    public void OnMouseUp()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");

        if (isAttack) {
            GameObject obj = gameController.GetComponent<GameController>().GetPosition(matrixX,matrixY);
            if(obj.name == "white_king")
            {
                gameController.GetComponent<GameController>().Winner("Black");
            }
            if (obj.name == "black_king")
            {
                gameController.GetComponent<GameController>().Winner("White");
            }
            Destroy(obj);
        }
        gameController.GetComponent<GameController>().SetPositionEmpty(Reference.GetComponent<ChessMan>().GetXBoard(),
        reference.GetComponent<ChessMan>().GetYBoard());
        reference.GetComponent<ChessMan>().SetXBoard(matrixX);
        reference.GetComponent<ChessMan>().SetYBoard(matrixY);
        reference.GetComponent<ChessMan>().SetCoords();

        gameController.GetComponent<GameController>().SetPosition(Reference);

        gameController.GetComponent<GameController>().NextTurn();
        reference.GetComponent<ChessMan>().DestroyMovePlates();
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }
    
}
