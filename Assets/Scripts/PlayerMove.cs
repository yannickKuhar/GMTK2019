﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{	
	// Filed for movemont.
	public string horizontalInputName;
	public string verticalInputName;
	public float movementSpeed;
	private CharacterController charController;

	// Filed for jumping.
	public float jumpMultyplier;
	public AnimationCurve jumpFallOff; 
	public KeyCode jumpKey; 
	private bool isJumping;

	private void Awake()
	{
		charController = GetComponent<CharacterController>();
	}

	private void Update()
	{
		PlayerMovement();
	}

	//////////////////// Functions. ////////////////////
	
	private void PlayerMovement()
	{
		float vertInput = Input.GetAxis(verticalInputName) * movementSpeed;
		float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;

		Vector3 forwardMovement = transform.forward * vertInput;
		Vector3 rightMovement = transform.right * horizInput;

		charController.SimpleMove(forwardMovement + rightMovement);

		JumpInput();
	}

	private void JumpInput()
	{
		// Prevents double jump.
		if (Input.GetKeyDown(jumpKey) && !isJumping)
		{
			isJumping = true;
			StartCoroutine(JumpEvent());
		}
	}

	private IEnumerator JumpEvent()
	{
		float timeInAir = 0.0f;

		do
		{
			float jumpForce = jumpFallOff.Evaluate(timeInAir);
			charController.Move(Vector3.up * jumpForce * jumpMultyplier * Time.deltaTime);
			timeInAir += Time.deltaTime;
			yield return null;	

		// The second part of the condition causes that if the player hits a ceiling it stops moving.
		} while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

		isJumping = false;
	}
}