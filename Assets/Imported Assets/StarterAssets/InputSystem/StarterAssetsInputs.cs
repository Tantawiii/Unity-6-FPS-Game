using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool shoot;
		public bool zoom;
		public int gun;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		
		public void OnShoot(InputValue value)
		{
			ShootInput(value.isPressed);
		}

		public void OnZoom(InputValue value)
		{
			ZoomInput(value.isPressed);
		}

		public void OnChange(InputValue value)
		{
			if (value.Get<float>() > 0.5f) // Key is pressed
			{
				int selectedWeapon = GetSelectedWeaponSlot(); // Get correct weapon slot
				ChangeInput(selectedWeapon);
			}
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void ShootInput(bool newShootState)
		{
			shoot = newShootState;
		}

		public void ZoomInput(bool newZoomState)
		{
			zoom = newZoomState;
		}

		public void ChangeInput(int newChangeState)
		{
			gun = newChangeState;
		}
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
		int GetSelectedWeaponSlot()
		{
			if (Keyboard.current.digit1Key.isPressed) return 1;
			if (Keyboard.current.digit2Key.isPressed) return 2;
			if (Keyboard.current.digit3Key.isPressed) return 3;
			
			return 0; // No weapon slot selected
		}
	}
	
}