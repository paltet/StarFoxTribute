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
    public GameObject countDownUI;

    public AudioSource levelMusic;
    public AudioSource SFX;
    public AudioClip endMusic;
    public AudioClip pauseSound;
    public AudioClip unpauseSound;
    public AudioClip countdown;

    public bool godMode = false;
    public bool endScene = false;  
    //public float duration;

    void Awake()
    {
        SFX.PlayOneShot(countdown);
        StartCoroutine(WaitInit());
    }

    IEnumerator WaitInit()
    {
        countDownUI = transform.Find("Canvas").Find("CountDown").gameObject;
        Time.timeScale = 0;
        GameIsPaused = true;
        // Activate countingDownCanvas
        countDownUI.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.955f); //time of clip
        countDownUI.transform.GetChild(0).gameObject.SetActive(false);
        countDownUI.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.955f); //time of clip
        countDownUI.transform.GetChild(1).gameObject.SetActive(false);
        countDownUI.transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1.1f); //time of clip
        countDownUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
        levelMusic.Play();
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
        SFX.PlayOneShot(unpauseSound);
        levelMusic.UnPause();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause ()
    {
        levelMusic.Pause();
        SFX.PlayOneShot(pauseSound);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu ()
    {
        //Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void EndScene ()
    {
        endScene = true;
        endUI.SetActive(true);
        aimTarget.SetActive(false);
        healthBar.SetActive(false);
        levelMusic.Stop();
        levelMusic.PlayOneShot(endMusic);
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
