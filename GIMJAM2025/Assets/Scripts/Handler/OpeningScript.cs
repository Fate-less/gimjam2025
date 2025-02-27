using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class OpeningScript : Handler
{
    VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        // When the video playback is done, restart the video. 
        videoPlayer.isLooping = true;

        // Each time the video reaches the end, call this function. 
        videoPlayer.loopPointReached += OnLoopPointReached;

        videoPlayer.Play();
    }

    void OnLoopPointReached(VideoPlayer vp)
    {
        SceneManager.LoadScene("Gameplay");
    }
}
