using System;
using System.Collections.Generic;
using System.Linq;

using Duality;
using Duality.Components;
using Duality.Components.Renderers;

namespace TopDownMovement
{
    /// <summary>
    /// This component gives the GameObject it is attached to the ability
    /// to move within the scene.
    /// </summary>
    // This attribute indicates that this component requires another
    // component to be attached to the same GameObject.
    [RequiredComponent(typeof(Transform))]
    public class Movable : Component
    {
        // An enum containing all the possible moving states for
        // the movable. This is used for the simple Finite State
        // Machine pattern this component uses.
        private enum MoveState
        {
            MOVE_FORWARD,
            MOVE_RIGHT,
            MOVE_LEFT,
            MOVE_BACKWARD
        }

        // The speed at which this object will move.
        private float moveSpeed = 5f;

        // Declare/create this component's MoveState.
        private MoveState moveState = new MoveState();

        /// <summary>
        /// This function is what does all the moving. It's parameters default
        /// to "false" (optional parameters) so we can just set the parameter
        /// we need and the rest will stay the same.
        /// </summary>
        /// <param name="forward">Whether to move forwards.</param>
        /// <param name="right">Whether to move to the right.</param>
        /// <param name="left">Whether to move to the left.</param>
        /// <param name="backward">Whether to move backwards.</param>
        public void Move
            (bool forward = false, 
            bool right = false, 
            bool left = false, 
            bool backward = false)
        {
            // We are going to use a switch statement for moving.
            switch (moveState)
            {
                // Move forward case.
                case MoveState.MOVE_FORWARD:
                    if (forward) // If the "forward" variable was set to true...
                    {
                        // Move this GameObject...
                        this.GameObj.Transform.MoveBy(-Vector2.UnitY * moveSpeed);
                        // Change this GameObject's AnimSpriteRenderer's displayed frame
                        this.SetGraphics(1);
                    }
                    goto case MoveState.MOVE_RIGHT;

                // Move right case.
                case MoveState.MOVE_RIGHT:
                    if (right) // If the "right" variable was set to true...
                    {
                        // Move this GameObject...
                        this.GameObj.Transform.MoveBy(Vector2.UnitX * moveSpeed);
                        // Change this GameObject's AnimSpriteRenderer's displayed frame
                        this.SetGraphics(2);
                    }
                    goto case MoveState.MOVE_LEFT;

                // Move left case.
                case MoveState.MOVE_LEFT:
                    if (left) // If the "left" variable was set to true...
                    {
                        // Move this GameObject...
                        this.GameObj.Transform.MoveBy(-Vector2.UnitX * moveSpeed);
                        // Change this GameObject's AnimSpriteRenderer's displayed frame
                        this.SetGraphics(3);
                    }
                    goto case MoveState.MOVE_BACKWARD;

                // Move backward case.
                case MoveState.MOVE_BACKWARD:
                    if (backward) // If the "backward" variable was set to true...
                    {
                        // Move this GameObject...
                        this.GameObj.Transform.MoveBy(Vector2.UnitY * moveSpeed);
                        // Change this GameObject's AnimSpriteRenderer's displayed frame
                        this.SetGraphics(4);
                    }
                    break;
            }
        }

        /// <summary>
        /// This function sets the first animation frame of the AnimSpriteRenderer
        /// component of the GameObject this component is attached to.
        /// </summary>
        /// <param name="firstFrame">The first frame's frame number.</param>
        private void SetGraphics(int firstFrame)
        {
            // Set this GameObject's AnimSpriteRenderer's AnimFirstFrame (basically,
            // this is the frame that is displayed) to the supplied value.
            this.GameObj.GetComponent<AnimSpriteRenderer>().AnimFirstFrame = firstFrame;
        }
    }
}