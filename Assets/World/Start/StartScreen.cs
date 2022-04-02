using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public GameFlow gameFlow;
    public void Update(){
        if(Input.anyKeyDown) {
            gameFlow.NextLevel();
        }
    }
}
