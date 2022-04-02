using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public static SceneData Instance;
    public int Quota = 3;
    public int Current;
    public bool LevelComplete;
    public bool running;

    public void Awake() {
        Instance = this;
    }
    public bool GoToNextLevel {
        get {
            return LevelComplete;
        }
    }
    public IEnumerator DoLevelEnd() {
        // wind everything down in the scene
        // gameflow does the rest 
        LevelComplete = true;
        running = false;
        yield break;
    }
    public void Update() {
        if(Quota <= Current && !running){
            running = true;
            StartCoroutine(DoLevelEnd());
        }
    }
   
}
