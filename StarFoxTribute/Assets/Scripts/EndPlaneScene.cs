using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndPlaneScene : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            // O fer el que vulguem realment
            SceneManager.LoadScene("Scenes/Menu");
        }
    }
}
