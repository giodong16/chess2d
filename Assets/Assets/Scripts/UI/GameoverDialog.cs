using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameoverDialog : Dialog
{
    public Text content;
    public void Show(string text)
    {
        content.text = text;
        Show();
    }
}
