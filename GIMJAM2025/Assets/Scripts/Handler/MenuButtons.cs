using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void ResumeButton()
    {
        GetComponent<PopPauseMenu>().CallPause();
    }
    public void RestartButton()
    {
        if(Time.timeScale < 1)
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void IngameExitButton()
    {
        if(Time.timeScale < 1)
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene("Main menu");
    }
    public void MainmenuExitButton()
    {
        Application.Quit();
    }
    public void MoveToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
