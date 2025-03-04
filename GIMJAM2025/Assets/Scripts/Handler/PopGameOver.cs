using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopGameOver : MonoBehaviour
{
    public GameObject gameOverObject;
    public void CallGameOver()
    {
        gameOverObject.SetActive(true);
        Time.timeScale = 0;
    }
}
