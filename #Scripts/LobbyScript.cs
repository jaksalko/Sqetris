using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScript : MonoBehaviour {

    private void Start()
    {
      
    }
    public void PlayButton() {
        if (PlayerPrefs.GetInt("NewPlayer", 0) == 0)
        {
            AutoFade.LoadLevel("Tutorial", 1, 1, Color.black);
        }
        else { AutoFade.LoadLevel("Sqetris", 1, 1, Color.black); }
       
    }
}
