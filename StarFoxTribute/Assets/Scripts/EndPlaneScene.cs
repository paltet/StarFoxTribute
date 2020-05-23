using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndPlaneScene : MonoBehaviour
{
    public GameObject camera;

    void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("Player"))
        {
            SceneController sc = camera.GetComponent<SceneController>();
            // O fer el que vulguem realment
            sc.EndScene();
        }
    }
}
