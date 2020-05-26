using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject endUI;
    public GameObject aimTarget;
    public GameObject healthBar;
    public GameObject godModeUI;
    public GameObject dieUI;

    public AudioSource levelMusic;

    public bool godMode = false;    
    public float duration;

    void Awake()
    {               
        StartCoroutine(WaitInit());
    }

    IEnumerator WaitInit()
    {
        Time.timeScale = 0;
        GameIsPaused = true;
        // Activate countingDownCanvas
        yield return new WaitForSecondsRealtime(duration);
        // Deactivate countingDownCanvas
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && endUI.activeSelf == false) 
        {
            if (GameIsPaused)
            {
                Resume();
            } 
            else {
                Pause();
            }
        }
        if (Input.GetKeyDown(KeyCode.G)) godMode = !godMode;
        godModeUI.SetActive(godMode);
    }

    public void Resume ()
    {
        levelMusic.UnPause();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause ()
    {
        levelMusic.Pause();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu ()
    {
        //Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public  void EndScene ()
    {
        endUI.SetActive(true);
        aimTarget.SetActive(false);
        healthBar.SetActive(false);
        //Time.timeScale = 0f;
        //SceneManager.LoadScene("Menu");

    }

    public void DieScene(){

        dieUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
