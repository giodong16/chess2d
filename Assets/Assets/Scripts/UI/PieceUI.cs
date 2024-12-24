using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceUI : MonoBehaviour
{
    Sprite sprite;
    public void SetUp(Sprite sprite)
    {
        this.sprite = sprite;
        this.gameObject.GetComponent<Image>().sprite = sprite;
    }
}
