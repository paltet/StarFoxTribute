using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    public Camera main;
    public Transform ship;
    public float rangeX = 0.25f;
    public float rangeY = 0.3f;
    float xMin = Screen.width/2 - 25; //middle of the screen approx
    float yMin = Screen.height/2 + 25; //middle of the screen approx
    public Texture2D crosshairImage;
    public bool crosshairTrespassBoundaries = false;
    public float crosshairSize = 1;

    // Update is called once per frame

    void OnGUI() {
        float mouseX = Input.mousePosition.x - (crosshairSize*crosshairImage.width / 20);
        float mouseY = Input.mousePosition.y + (crosshairSize*crosshairImage.height / 20);
        if(!crosshairTrespassBoundaries) {
            var shi = Camera.main.WorldToScreenPoint(ship.position);
            float minx = Mathf.Min((1-2*rangeX)*Screen.width,Mathf.Max(-(crosshairSize*crosshairImage.width / 20),shi.x - rangeX*Screen.width));
            float maxx = Mathf.Max(2*rangeX*Screen.width,Mathf.Min(Screen.width,shi.x + rangeX*Screen.width));
            float miny = Mathf.Min((1-2*rangeY)*Screen.height,Mathf.Max(-(crosshairSize*crosshairImage.height / 20),shi.y - rangeY*Screen.height));
            float maxy = Mathf.Max(2*rangeY*Screen.height,Mathf.Min(Screen.height,shi.y + rangeY*Screen.height));
            mouseX = Mathf.Clamp(mouseX, minx, maxx);
            mouseY = Mathf.Clamp(mouseY, miny, maxy);
        }
        if(Time.timeScale==1) {
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
        pos.x = Mathf.Clamp(pos.x, shi.x - rangeX, shi.x + rangeX);
        pos.y = Mathf.Clamp(pos.y, shi.y - rangeY, shi.y + rangeY);
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        //transform.position = lookPos;
    }
}
