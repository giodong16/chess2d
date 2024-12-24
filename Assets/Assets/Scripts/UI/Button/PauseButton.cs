using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        if (button == null) return;
        button.onClick.AddListener(Pause);
    }

    private void Pause()
    {
        if (Controller.Instance != null)
        {
            Controller.Instance.Pause();
        }
    }
}
