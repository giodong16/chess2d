using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        if(button == null ) return;
        button.onClick.AddListener(Play);
    }
    public void Play()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
