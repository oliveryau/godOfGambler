using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class EndCredits : MonoBehaviour
{
    public SceneManagement sceneManagement;
    [SerializeField] VideoPlayer videoPlayer;

    [Header("Skip Timer")]
    public Image timerBorder;
    public Image timerIcon;

    private bool timerRunning;
    private float startTime = 0f;
    private float holdTime = 2f;

    // Start is called before the first frame update
    private void Start()
    {
        timerBorder.enabled = false;
        timerIcon.fillAmount = 0;
        videoPlayer.loopPointReached += EndVideo;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startTime = Time.time;
            timerRunning = true;
            timerIcon.fillAmount = 1;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            timerRunning = false;
            timerBorder.enabled = false;
            timerIcon.fillAmount = 0;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (startTime + holdTime <= Time.time)
            {
                EndVideo(videoPlayer);
            }
        }

        if (timerRunning)
        {
            timerBorder.enabled = true;
            timerIcon.fillAmount -= 1 / holdTime * Time.deltaTime;
            if (timerIcon.fillAmount <= 0)
            {
                timerBorder.enabled = false;
                timerIcon.fillAmount = 0;
                timerRunning = false;
            }
        }
    }

    private void EndVideo(VideoPlayer video)
    {
        StartCoroutine(sceneManagement.FadeMenu());
    }
}
