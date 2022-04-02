using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameFlow : MonoBehaviour
{   
    public SceneData sceneData;
    public bool running;
    public void Awake(){
        DontDestroyOnLoad(this);
    }

    public void LateUpdate() {
        if(sceneData != null 
        && sceneData.LevelComplete){
            NextLevel();
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
        running = false;
        yield break;
    }
    
    public IEnumerator EndGame() {
        yield return SceneManager.LoadSceneAsync("start");
        yield break;
    }
}
