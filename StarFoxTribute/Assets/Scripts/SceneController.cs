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
    public bool playing = false;
    //public float duration;

    void Awake()
    {
        playing = false;
        SFX.PlayOneShot(countdown);
        StartCoroutine(WaitInit());
        Cursor.visible = false;
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
        playing = true;
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
        Cursor.visible = false;
        SFX.PlayOneShot(unpauseSound);
        levelMusic.UnPause();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        playing = true;
    }

    void Pause ()
    {
        levelMusic.Pause();
        SFX.PlayOneShot(pauseSound);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
        playing = false;
    }

    public void LoadMenu ()
    {
        //Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void EndScene ()
    {
        Cursor.visible = true;
        endScene = true;
        endUI.SetActive(true);
        aimTarget.SetActive(false);
        healthBar.SetActive(false);
        levelMusic.Stop();
        levelMusic.PlayOneShot(endMusic);
        playing = false;
        //Time.timeScale = 0f;
        //SceneManager.LoadScene("Menu");

    }

    public void DieScene(){
        Cursor.visible = true;
        dieUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        playing = false;
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
