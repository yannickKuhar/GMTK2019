using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
	public string mouseXInputName, mouseYInputName;
	public float mouseSensitivity;

	public Transform playerBody;

	// Clamp x rotation axis.
	private float xAxisClamp;

	void Awake()
	{
		if (Cursor.lockState == CursorLockMode.None)
		{
			LockCursor();
		}

		xAxisClamp = 0.0f;
	}

    // Update is called once per frame
    void Update()
    {
		CameraRotation();
    }

	//////////////////// Functions. ////////////////////
	
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

	private void CameraRotation()
	{
		// Current x and y values of the mouse.
		float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;
		
		// Clamps x rotation axis, so we can view only 90deg upwards and 90deg downwards.
		xAxisClamp += mouseY;

		if (xAxisClamp > 90.0f)
		{
			xAxisClamp = 90.0f;
			mouseY = 0.0f;
			ClampXAxisRotationToValue(270.0f);
		}
		else if (xAxisClamp < -90.0f)
		{
			xAxisClamp = -90.0f;
			mouseY = 0.0f;
			ClampXAxisRotationToValue(90.0f);
		}

		// Rotate camera.
		transform.Rotate(Vector3.left * mouseY);
		playerBody.Rotate(Vector3.up * mouseX);
	}
	
	/**
	 * Clamp the x value of our rotation to a desierd value.
	 * @param val The value we want to clamp x to.
	 */
	private void ClampXAxisRotationToValue(float val)
	{
		Vector3 eulerTmp = transform.eulerAngles;
		eulerTmp.x = val;
		transform.eulerAngles = eulerTmp;
	}
}
