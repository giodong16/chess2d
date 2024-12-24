using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplayButton : MonoBehaviour
{
    Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        if (button == null) return;
        button.onClick.AddListener(Replay);
    }

    private void Replay()
    {
        if (Controller.Instance != null)
        {
            Controller.Instance.Replay();
        }
    }
}
