using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{   
    public SceneData sceneData;
    public bool running;
    public int barrels;
    public int highend = 10;

    public void Awake(){
        DontDestroyOnLoad(this);
    }
    public void Start(){
        barrels = Random.Range(5,highend);
    }
    public void LateUpdate() {
        if(sceneData != null 
        && sceneData.LevelComplete
        && !running){
            running = true;
            StartCoroutine(EndGame());
        }
    }

    public void NextLevel(){
        if(!running) {
            running = true;
            StartCoroutine(_StartLevel());
        }
    }

    public IEnumerator _StartLevel(string level = "main"){
        yield return SceneManager.LoadSceneAsync(level);
        sceneData = GameObject.FindObjectOfType<SceneData>();
        sceneData.Quota = barrels;
        highend+=5;
        running = false;
        yield break;
    }
    
    public IEnumerator EndGame() {
        sceneData = null;
        yield return SceneManager.LoadSceneAsync("start");
        running = false;
        yield break;
    }
}
