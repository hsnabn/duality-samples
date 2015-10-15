using System;
using System.Collections.Generic;
using System.Linq;

using Duality;
using Duality.Components;

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
        // The speed at which this object will move.
        private float moveSpeed = 1f;

        public Movable mv { get; set; }

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
            // Here we define a "totalMovement" variable, which is the total movement
            // that will be applied to this Movable.
            Vector2 totalMovement = Vector2.Zero;

            // Here we add the respective movement axes multiplied with the movement
            // speed, based on the parameters passed to this function.
            if (forward == true) totalMovement += -Vector2.UnitY * this.moveSpeed;
            if (right == true) totalMovement += Vector2.UnitX * this.moveSpeed;
            if (left == true) totalMovement += -Vector2.UnitX * this.moveSpeed;
            if (backward == true) totalMovement += Vector2.UnitY * this.moveSpeed;

            // If the added movement is not zero, then move!
            if (totalMovement != Vector2.Zero) this.GameObj.Transform.MoveBy(totalMovement);
        }
    }
}