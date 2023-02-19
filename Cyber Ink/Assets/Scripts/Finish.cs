using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [Header("UX/UI")]
    public LevelFade levelFade;
    public Animator musicAnim;
    public float waitTime;

    [Header("Keys")]
    public GameObject firstKey;
    public GameObject secondKey;
    public GameObject thirdKey;

    [Header("Others")]
    public GameObject player;

    private void Update()
    {
        CheckLevelComplete();
    }

    public void CheckLevelComplete()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 1f && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(MusicFadeChangeScene());
        }
    }

    private IEnumerator MusicFadeChangeScene()
    {
        musicAnim.SetTrigger("fadeOut");
        StartCoroutine(levelFade.LoadingScene());
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Start"); //Change to level 3
    }
}
