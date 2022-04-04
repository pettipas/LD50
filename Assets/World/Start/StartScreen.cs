using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    public GameFlow gameFlow;
    public Text barrelText;

    public void Update(){
        barrelText.text = gameFlow.barrels.ToString();
        if(Input.anyKeyDown) {
            gameFlow.NextLevel();
        }
    }
}
