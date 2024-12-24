using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public Text contentText;
    public Text titleText;

    public virtual void Show()
    {
        this.gameObject.SetActive(true);
    }
    public virtual void Close() {  this.gameObject.SetActive(false); }
}
