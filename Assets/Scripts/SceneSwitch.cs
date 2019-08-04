using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{	
	public void SelectTorch()
	{
		WeaponType.weaponType = "torch";
	}

	public void SelectSling()
	{
		WeaponType.weaponType = "sling";
	}

    public void LoadLevel()
	{
		Debug.Log("yes0");
		SceneManager.LoadScene(1);
	}

	public void ExitGame()
	{
		// Debug.Log("yes1");
		Application.Quit();
	}
}
