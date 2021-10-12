using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text HighscoreText;
    private void Awake()
    {
        HighscoreText.text="HIGHSCORE:"+PlayerPrefs.GetInt("HighScore", 0);
    }
    public void Play_Button()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit_Button()
    {
        Application.Quit();
    }
}
