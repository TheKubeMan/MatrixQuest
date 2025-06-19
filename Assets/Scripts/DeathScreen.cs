using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
	public void Res()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Test()
	{
		SceneManager.LoadScene("DemoLevel");
	}

	public void MainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

}
