using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    // Start is called before the first frame update

    public float fillAmount = 1;
    public Slider slider;

    // Update is called once per frame
    void Update()
    {   
        //Slider slider = GetComponent<Slider>();

        if (fillAmount < 0) fillAmount = 0;

        slider.value = fillAmount;
    }

    public void updateSlider(float newAmount){
        fillAmount = newAmount;
    }
}
