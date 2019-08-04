using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{	
	// Fileds for movemont.
	public string horizontalInputName;
	public string verticalInputName;
	public float movementSpeed;
	private CharacterController charController;

	// Fileds for jumping.
	public float jumpMultyplier;
	public AnimationCurve jumpFallOff; 
	public KeyCode jumpKey;
	private bool isJumping;
	
	// Fields for running.
	public float deltaSpeedForRunning;
	public KeyCode runKey;
	private bool isRunning;

	private void Awake()
	{
		isRunning = false;
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
		
		Vector3 move = forwardMovement + rightMovement;

		charController.SimpleMove(move);

		//	Debug.Log("x: " + move.x);
		//	Debug.Log("y: " + move.y);
		//	Debug.Log("z: " + move.z);
		
		if ((move.x != 0) && (move.z != 0))
		{
			SoundManager.Instance.Play(SoundManager.Instance.walk);
		}
		else if ((move.x == 0) && move.y == 0 &&(move.z == 0) && !isJumping)
		{
			SoundManager.Instance.Stop();
		}

		// Debug.Log("weapon: " + WeaponType.weaponType);

		Run(move);
		JumpInput();
	}

	private void Run(Vector3 move)
	{
		if (Input.GetKeyDown(runKey))
		{
			movementSpeed += deltaSpeedForRunning;
			isRunning = true;
		}

		if (isRunning && (move.x != 0) && (move.z != 0))
		{
			SoundManager.Instance.StopContinuousPlay(SoundManager.Instance.run);
		}
		
		if (Input.GetKeyUp(runKey))
		{
			movementSpeed -= deltaSpeedForRunning;
			isRunning = false;
		}
	}

	private void JumpInput()
	{
		// Prevents double jump.
		if (Input.GetKeyDown(jumpKey) && !isJumping)
		{
			SoundManager.Instance.Stop();
			SoundManager.Instance.Play(SoundManager.Instance.jump);
			// SoundManager.Instance.StopPlay(SoundManager.Instance.jump);
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
