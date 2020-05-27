using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    public Camera main;
    public Transform ship;
    public float range = 0.1f;
    float xMin = Screen.width/2 - 25;
    float yMin = Screen.height/2 - 25;
    public Texture2D crosshairImage;
    public bool crosshairTrespassBoundaries = false;
    public float crosshairSize = 1;

    // Update is called once per frame

    void OnGUI() {
        float mouseX = Input.mousePosition.x - (crosshairSize*crosshairImage.width / 20);
        float mouseY = Input.mousePosition.y + (crosshairSize*crosshairImage.height / 20);
        if(!crosshairTrespassBoundaries) {
            var shi = Camera.main.WorldToScreenPoint(ship.position);
            mouseX = Mathf.Clamp(mouseX, shi.x - range*Screen.width, shi.x + range*Screen.width);
            mouseY = Mathf.Clamp(mouseY, shi.y - range*Screen.height, shi.y + range*Screen.width);
        }
        if(Time.timeScale>0) {
            xMin = mouseX; //- (crosshairSize*crosshairImage.width / 20);
            yMin = (Screen.height - mouseY);// - (crosshairSize*crosshairImage.height / 20);
        }
        if(Camera.main.GetComponent<SceneController>().playing)
            GUI.DrawTexture(new Rect(xMin, yMin, crosshairSize*crosshairImage.width/10, crosshairSize*crosshairImage.height/10), crosshairImage);
    }

    void FixedUpdate()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.localPosition.z);
        Vector3 lookPos = main.ScreenToWorldPoint(mousePos);

        var shi = Camera.main.WorldToViewportPoint(ship.position);
        var pos = Camera.main.WorldToViewportPoint(lookPos);
        pos.x = Mathf.Clamp(pos.x, shi.x - range, shi.x + range);
        pos.y = Mathf.Clamp(pos.y, shi.y - range, shi.y + range);
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        //transform.position = lookPos;
    }
}
