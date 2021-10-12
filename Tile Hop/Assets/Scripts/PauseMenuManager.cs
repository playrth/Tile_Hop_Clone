using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject PauseMenuDisplay;
    [SerializeField]
    GameObject player;
    float timescale;

    private void Awake()
    {
        PauseMenuDisplay.SetActive(false);
    }
    public void pause()
    {
        AudioSystem.Singleton.StopSound(player);
        PauseMenuDisplay.SetActive(true);
        timescale= Time.timeScale;
        Time.timeScale = 0;
    }
    public void back()
    {
        AudioSystem.Singleton.PlaySound($"BackGround_{Random.Range(1, 2)}", player);
        PauseMenuDisplay.SetActive(false);
        Time.timeScale = timescale;
    }
    public void Exit_Button()
    {
        Time.timeScale = timescale;
        Application.Quit();
    }
    public void Menu()
    {
        Time.timeScale = timescale;
        SceneManager.LoadScene(0);
    }

}
