﻿using Game.Common;
using UnityEngine;

namespace Scripts.Gameplay.Player
{
    public class PlayerIdleState : IPlayerStateBehaviour
    {
        private readonly PlayerController playerController;
        private readonly Rigidbody2D rigidbody2D;
 
        public PlayerIdleState(PlayerController playerController)
        {
            this.playerController = playerController;

            var collider = playerController.GetComponent<Collider2D>();
            rigidbody2D = collider.attachedRigidbody;
        }

        public void OnStateEnter()
        {
            rigidbody2D.Sleep();
        }

        public void OnStateUpdate()
        {
            if (IsGrounded())
            {
                if (IsMoved())
                {
                    playerController.ChangePlayerState(PlayerState.Moving);
                }

                if (IsJumpKeyClicked())
                {
                    playerController.ChangePlayerState(PlayerState.Jumping);
                }
            }
            else
            {
                playerController.ChangePlayerState(PlayerState.Falling);
            }
        }

        public void OnStateFixedUpdate()
        {
            // Left blank intentionally
        }

        public void OnStateExit()
        {
            // Left blank intentionally
        }

        private bool IsGrounded()
        {
            return playerController.IsGrounded();
        }

        private bool IsMoved()
        {
            var horizontal = Utils.GetAxis(Axes.Horizontal, isRaw: true);
            return Mathf.Abs(horizontal) > 0;
        }

        private bool IsJumpKeyClicked()
        {
            var jumpKey = playerController.Properties.JumpKey;
            return Input.GetKeyDown(jumpKey);
        }
    }
}