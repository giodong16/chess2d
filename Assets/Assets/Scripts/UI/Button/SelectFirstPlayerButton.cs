using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectFirstPlayerButton : MonoBehaviour
{
    Button button;
    public bool isWhite;

    private void Start()
    {
        button = GetComponent<Button>();
        if(button == null ) return;
        button.onClick.AddListener(SetUpFirstPlayer);
    }
    private void SetUpFirstPlayer()
    {
        if (Controller.Instance != null) { 
            Controller.Instance.IsWhitePlayer = isWhite;
        } 
    }
}
