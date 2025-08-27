using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public AudioSource BGM;
    public GameObject timer, score;
    public GameObject resumeButton;
    public GameObject crosshair, TaskIcon, FreezeIcon;
    public void Pause()
    {
        Time.timeScale = 0;
        BGM.volume /= 2;
        timer.SetActive(false);
        score.SetActive(false);
        crosshair.SetActive(false);
        TaskIcon.SetActive(false);
        FreezeIcon.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        EventSystem.current.SetSelectedGameObject(resumeButton);
    }
    public void Unpause()
    {
        Time.timeScale = 1;
        BGM.volume *= 2;
        timer.SetActive(true);
        score.SetActive(true);
        crosshair.SetActive(true);
        TaskIcon.SetActive(true);
        // if (GameManager.GM.singlePlayer)
            FreezeIcon.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
