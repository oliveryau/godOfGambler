using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCredits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndingCredits());
    }

    private IEnumerator EndingCredits()
    {
        yield return new WaitForSeconds(15f);
        SceneManager.LoadScene("Start");
    }
}
