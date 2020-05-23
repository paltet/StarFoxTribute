using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static bool GameIsPaused = false;
    //public GameObject pauseMenuUI;
    //public GameObject endUI;
    
    public float duration;
    void Start()
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
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (GameIsPaused)
            {
                Resume();
            } 
            else {
                Pause();
            }
        }        
    }

    public void Resume ()
    {
        //pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause ()
    {
        //pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu ()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public static void EndScene ()
    {
        //endUI.SetActive(true);
        //Time.timeScale = 0f;
        SceneManager.LoadScene("Menu");
    }
}
