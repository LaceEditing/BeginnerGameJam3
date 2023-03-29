using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonInputFunctions : MonoBehaviour
{
   //Pause Menu Buttons
    public void resume()
    {
        if(GameManager.instance.isPaused) //Pause menu check
        {
            //Add button SFX here
            GameManager.instance.isPaused = !GameManager.instance.isPaused; //Toggle paused bool
            GameManager.instance.gameUnlockPause(); //Unlock game
            GameManager.instance.pauseMenu.SetActive(GameManager.instance.isPaused); //Toggle off UI for pause menu
        }
    }
    public void quit()
    {
        //Add button SFX here
        Application.Quit(); //Exit the game, could change this to exit to main menu.
    }

}
