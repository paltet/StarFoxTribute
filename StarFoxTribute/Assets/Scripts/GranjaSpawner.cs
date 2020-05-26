using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GranjaSpawner : MonoBehaviour
{
    public float awakePos = 50f; 

    // Update is called once per frame
    void Update()
    {
        float pos = transform.GetComponent<CinemachineDollyCart>().m_Position;

        if (pos > awakePos){
            transform.GetChild(0).gameObject.SetActive(true);
            this.enabled = false;
        }
    }
}
