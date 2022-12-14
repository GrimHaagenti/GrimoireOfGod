using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject Main;
    [SerializeField] GameObject LevelSelect;

    public void ChangeToMain()
    {
        Main.SetActive(true);
        LevelSelect.SetActive(false);
    }
    public void ChangeToLevelSelect()
    {
        LevelSelect.SetActive(true);
        Main.SetActive(false);
    }

    public void LaunchGame() {

        SceneManager.LoadScene((int)Scenes.LAUNCHER);
    }
    public void LaunchGameDebug() {

        SceneManager.LoadScene((int)Scenes.LAUNCHERDEBUG);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
