using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        if (button == null) return;
        button.onClick.AddListener(Resume);
    }

    private void Resume()
    {
        if (Controller.Instance != null)
        {
            Controller.Instance.Resume();
        }
    }
}
