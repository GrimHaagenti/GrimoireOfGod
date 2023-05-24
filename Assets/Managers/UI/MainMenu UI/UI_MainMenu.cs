using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] UI_MainMenuOptions Options;

   
    public  void Init()
    {
        Options.OnEnter();
    } 
    public void LaunchGame()
    {

        SceneManager.LoadScene("NewGameTestScene");     
    }
    

    public void ExitGame()
    {
        Application.Quit();
    }

}
