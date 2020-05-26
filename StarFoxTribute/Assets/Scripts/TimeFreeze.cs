using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreeze : MonoBehaviour
{
    public static TimeFreeze INSTANCE;

    private int framesToFreeze = 0;
    private bool act = false;

    void Awake() {
        if (TimeFreeze.INSTANCE) {
            Destroy(this);
        } else {
            TimeFreeze.INSTANCE = this;
        }
    }
    
    void Update() {
        if(act) {
            if (framesToFreeze > 0) {
                Time.timeScale = 0;
                framesToFreeze--;
            } else {
                Time.timeScale = 1;
                act = false;
            }
        }
    }

    public void FreezeTime(int numberOfFrames) {
        framesToFreeze = numberOfFrames;
        act = true;
    }
}
