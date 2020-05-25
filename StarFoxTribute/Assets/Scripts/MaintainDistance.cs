using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MaintainDistance : MonoBehaviour
{
    public GameObject playerCart;
    public float maxDist = 5.0f;
    float dist;
    public float minDist = 5.0f;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        dist = transform.GetComponent<CinemachineDollyCart>().m_Position;
        maxDist = dist + maxDist;
        minDist = dist - minDist;
        speed = transform.GetComponent<CinemachineDollyCart>().m_Speed;
    }

    // Update is called once per frame
    void Update()
    {
        float myPos = transform.GetComponent<CinemachineDollyCart>().m_Position;
        float playerPos = playerCart.transform.GetComponent<CinemachineDollyCart>().m_Position;
        if(Mathf.Abs((myPos-playerPos)-dist) <= 1.0f) {
            transform.GetComponent<CinemachineDollyCart>().m_Speed = speed;
        }  
        else if((myPos-playerPos) >= maxDist) {
            transform.GetComponent<CinemachineDollyCart>().m_Speed = 0.8f*speed;
        }
        else if((myPos-playerPos) <= minDist) {
            transform.GetComponent<CinemachineDollyCart>().m_Speed = 1.1f*speed;
        }
              
    }
}
