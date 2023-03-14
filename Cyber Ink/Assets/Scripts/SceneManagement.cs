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

    [Header("Scene Delay Time")]
    public float waitTime;

    private void Awake()
    {
        Screen.SetResolution(1920, 1080, false);
    }

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        fadePanel.SetActive(true);

        if (scene.name == "Start")
        {
            AudioManager.Instance.PlayMusic("Menu BGM");
        }
        else if (scene.name == "Level 1")
        {
            AudioManager.Instance.PlayMusic("Level 1 BGM");
        }
        else if (scene.name == "Level 2")
        {
            AudioManager.Instance.PlayMusic("Level 2 BGM");
        }
    }

    public IEnumerator FadeNextScene() //For going to the next scene
    {
        fadeAnim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(waitTime);

        if (scene.name == "Start")
        {
            SceneManager.LoadScene("Level 1");
        }
        else if (scene.name == "Level 1")
        {
            SceneManager.LoadScene("Level 2");
        }
        else if (scene.name == "Level 2")
        {
            SceneManager.LoadScene("Level 3");
        }
        else if (scene.name == "Level 3")
        {
            SceneManager.LoadScene("End Credits");
        }
    }

    public IEnumerator FadeSameScene() //For retry
    {
        fadeAnim.SetTrigger("fadeOut");
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
        else if (scene.name == "Level 3")
        {
            SceneManager.LoadScene("Level 3");
        }
    }

    public IEnumerator FadeMenu() //For returning to main menu
    {
        fadeAnim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(waitTime);

        if (scene.name == "Start")
        {
            SceneManager.LoadScene("Level 1");
        }
        else if (scene.name == "Level 1" || scene.name == "Level 2" || scene.name == "Level 3")
        {
            SceneManager.LoadScene("Start");
        }
    }
}
