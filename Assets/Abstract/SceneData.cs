using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SceneData : MonoBehaviour
{
    public static SceneData Instance;
    public int Quota = 3;
    public int Current;
    public bool LevelComplete;
    public bool running;
    public Animator doors;

    public Text remaining;
    public void Awake() {
        Instance = this;
    }

    public IEnumerator DoLevelEnd() {
        doors.SafePlay("doors_close");
        yield return null;
        while(!doors.AtEndOfAnimation()){
            yield return null;
        }
        LevelComplete = true;
        running = false;
        yield break;
    }
    public void Update() {
        remaining.text = (Quota - Current).ToString();
        if(Quota <= Current && !running){
            running = true;
            StartCoroutine(DoLevelEnd());
        }
    }
   
}
