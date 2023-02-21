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
    public Dialogue finishConditionDialogue;

    [Header("Others")]
    public GameObject player;
    public Image healthBar;
    public Image dashCooldownImage;
    public GameObject keyPanel;

    private void Update()
    {
        FinishCheck();
    }

    public void FinishCheck()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 1f && Input.GetKeyDown(KeyCode.E))
        {
            if (firstKey != null)
            {
                MissingKey();
            }
            else if (secondKey != null)
            {
                MissingKey();
            }
            else if (thirdKey != null)
            {
                MissingKey();
            }
            else
            {
                StartCoroutine(MusicFadeChangeScene());
            }
        }
    }

    private void MissingKey()
    {
        healthBar.enabled = false;
        dashCooldownImage.enabled = false;
        keyPanel.SetActive(false);
        finishConditionDialogue.StartDialogue();
    }

    private IEnumerator MusicFadeChangeScene()
    {
        musicAnim.SetTrigger("fadeOut");
        StartCoroutine(levelFade.LoadingScene());
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Start"); //Change to level 3
    }
}
