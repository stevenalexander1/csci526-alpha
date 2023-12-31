using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
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
		public bool interact;
		public bool changePerspective;
		public bool lastUsedCamera;
		public bool inGameMenu;
		public bool chainState;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
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

		public void OnInteract(InputValue value)
		{
			InteractInput(value.isPressed);
		}

		public void OnChangePerspective(InputValue value)
		{
			ChangePerspectiveInput(value.isPressed);
		}
		
		public void OnAccessLastUsedCamera(InputValue value)
		{
			LastUsedCameraInput(value.isPressed);
		}
		
		public void OnInGameMenu(InputValue value)
    {
			InGameMenuInput(value.isPressed);
    }


		public void OnCameraChain(InputValue value)
        {
			ChainToNextCamera(value.isPressed);
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
		
		private void InteractInput(bool newInteractState)
		{
			interact = newInteractState;
		}
		
		private void ChangePerspectiveInput(bool newPerspectiveState)
		{
			changePerspective = newPerspectiveState;
		}

		private void InGameMenuInput(bool newInGameState)
        {
			inGameMenu = newInGameState;
        }

		private void ChainToNextCamera(bool newChainState)
        {
			chainState = newChainState;
        }
		
		private void LastUsedCameraInput(bool newLastUsedCameraState)
		{
			lastUsedCamera = newLastUsedCameraState;
		}
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
		
		
	}
	
}