using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public AudioSource BGM;
    public GameObject timer;
    public GameObject combo;
    public GameObject resumeButton;
    public void Pause()
    {
        Time.timeScale = 0;
        BGM.volume /= 2;
        timer.SetActive(false);
        combo.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        EventSystem.current.SetSelectedGameObject(resumeButton);
    }
    public void Unpause()
    {
        Time.timeScale = 1;
        BGM.volume *= 2;
        timer.SetActive(true);
        combo.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
