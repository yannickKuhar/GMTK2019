using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	public static bool gameIsPaused = false;
	public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (gameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}

		Debug.Log("Cursor: " + Cursor.lockState);
    }

	//////////////////// Functions. ////////////////////
	
	public void Resume()
	{
		LockCursor();
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1.0f;
		gameIsPaused = false;
	}

	private void Pause()
	{
		UnlockCursor();
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0.0f;
		gameIsPaused = true;
	}
	
	private void LockCursor()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void UnlockCursor()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
}
