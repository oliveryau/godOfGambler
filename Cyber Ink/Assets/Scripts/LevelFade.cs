using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFade : MonoBehaviour
{
    public Animator anim;
    public GameObject fadePanel;

    private void Start()
    {
        fadePanel.SetActive(true);
    }

    public IEnumerator LoadingScene()
    {
        anim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(1f);
    }
}
