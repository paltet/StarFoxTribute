using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuController : MonoBehaviour
{
    public void MainMenu(){
        SceneManager.LoadScene("Scenes/Menu");
    }
    
    public void LevelSelector(){
        SceneManager.LoadScene("Scenes/LevelSelector");
    }
}
