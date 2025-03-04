using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopPauseMenu : MonoBehaviour
{
    public GameObject pauseMenuObject;
    private bool isPausing = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CallPause();
        }
    }

    public void CallPause()
    {
        if (isPausing)
        {
            pauseMenuObject.SetActive(false);
            Time.timeScale = 1;
            isPausing = false;
        }
        else
        {
            pauseMenuObject.SetActive(true);
            Time.timeScale = 0;
            isPausing = true;
        }
    }
}
