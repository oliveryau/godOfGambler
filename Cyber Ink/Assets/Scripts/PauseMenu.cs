using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerCombat playerCombat;
    public GameObject pauseScreen;

    private void Start()
    {
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        PlayerCombat playerCombat = GetComponent<PlayerCombat>();
    }

    public void PauseGame()
    {
        playerMovement.canMove = false;
        playerCombat.canAttack = false;
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        //script.rbSprite.flipX = false;
    }

    public void ResumeGame()
    {
        playerMovement.canMove = true;
        playerCombat.canAttack = true;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        Time.timeScale = 1;
    }
}
