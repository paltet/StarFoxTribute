using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorController : MonoBehaviour
{
    public void LoadLevel(int level){
        SceneManager.LoadScene("Scenes/Level" + level);
    }

    public void Back(){
        SceneManager.LoadScene(0);
    }
}
