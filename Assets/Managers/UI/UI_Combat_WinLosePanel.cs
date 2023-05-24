using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Combat_WinLosePanel : MonoBehaviour
{
    bool buttonPushed = false;
    [SerializeField ]bool isWinPanel= false;
  
    void Update()
    {
        if (InputManager._INPUT_MANAGER.Menu_GetAcceptButtonPressed() && !buttonPushed)
        {
            if (isWinPanel)
            {
                buttonPushed = true;
                GameManager._GAME_MANAGER.UnloadBattleScene();
            }
            else
            {
                Application.Quit();
            }
        }


    }
}
