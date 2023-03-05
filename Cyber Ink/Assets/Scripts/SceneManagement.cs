using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public Scene scene;

    [Header("Fade Animation")]
    public Animator fadeAnim;
    public GameObject fadePanel;

    [Header("Music Transition")]
    public Animator musicAnim;

    [Header("Scene Delay Time")]
    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        fadePanel.SetActive(true);
    }

    public IEnumerator MusicFadeSameScene() //For retry
    {
        fadeAnim.SetTrigger("fadeOut");
        musicAnim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(waitTime);

        if (scene.name == "Start")
        {
            SceneManager.LoadScene("Start");
        }
        else if (scene.name == "Level 1")
        {
            SceneManager.LoadScene("Level 1");
        }
        else if (scene.name == "Level 2")
        {
            SceneManager.LoadScene("Level 2");
        }
    }

    public IEnumerator MusicFadeChangeScene() //For returning to main menu
    {
        fadeAnim.SetTrigger("fadeOut");
        musicAnim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(waitTime);

        if (scene.name == "Start")
        {
            SceneManager.LoadScene("Level 2"); //Level 1
        }
        else if (scene.name == "Level 1" || scene.name == "Level 2")
        {
            SceneManager.LoadScene("Start");
        }
    }
}
