using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    public void PlayGame(){
        SceneManager.LoadScene ("Untitled");
    }
    public void ToTitleScreen(){
        SceneManager.LoadScene ("TitleScreen");
    }
    public void ToWinScreen(){
        SceneManager.LoadScene ("WinScreen");
    }
    public void ToCreditsScreen(){
        SceneManager.LoadScene ("Credits");
    }
    
}
