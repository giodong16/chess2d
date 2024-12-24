using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeButton : MonoBehaviour
{
    Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        if (button == null) return;
        button.onClick.AddListener(BackToHome);
    }

    private void BackToHome()
    {
        if (Controller.Instance != null) {
            Controller.Instance.BackToHome();
        }
    }
}
