using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.Components;

namespace TopDownMovement
{
    // This attribute indicates that this component requires another
    // component to be attached to the same GameObject.
    [RequiredComponent(typeof(Transform))]

    ///<summary>
    /// This component gives the GameObject it is attached to the ability
    /// to move across the scene.
    ///</summary>
    public class Movable : Component, ICmpUpdatable
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

        // Declare this component's MoveState
        private MoveState moveState;

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
            switch (moveState)
            {
                case MoveState.MOVE_FORWARD:
                    if (forward) this.GameObj.Transform.MoveBy(-Vector2.UnitY * moveSpeed);
                    break;

                case MoveState.MOVE_RIGHT:
                    if (right) this.GameObj.Transform.MoveBy(Vector2.UnitX * moveSpeed);
                    break;

                case MoveState.MOVE_LEFT:
                    if (right) this.GameObj.Transform.MoveBy(-Vector2.UnitX * moveSpeed);
                    break;

                case MoveState.MOVE_BACKWARD:
                    if (right) this.GameObj.Transform.MoveBy(Vector2.UnitY * moveSpeed);
                    break;
            }
        }
    }
}