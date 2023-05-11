using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Panel;

    public static LevelManager pause;
    private void Start()
    {

    }
    private void Update()
    {

    }

    public void _PlayAgain()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
        DontDestroyOnLoad(gameObject);
        if (!pause) pause = this;
        else Destroy(gameObject);
    }

    public void _MainMenu()
    {
        Debug.Log("MAIN MENU");
        SceneManager.LoadScene("Start Menu");
        Time.timeScale = 1;
        DontDestroyOnLoad(gameObject);
        if (!pause) pause = this;
        else Destroy(gameObject);
    }

    // Pause game
    public void _PauseButton()
    {
        Time.timeScale = 0;
        Panel.SetActive(true);
    }

    // Resume
    public void _ResumeButton()
    {
        Time.timeScale = 1;
        Panel.SetActive(false);
    }
}  
