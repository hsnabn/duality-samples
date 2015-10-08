using System;
using System.Collections.Generic;
using System.Linq;

using Duality;
using Duality.Input;

namespace TopDownMovement
{
    /// <summary>
    /// This component allows us to listen for input in the scene,
    /// and act upon the input accordingly.
    /// </summary>
    public class InputManager : Component, ICmpUpdatable, ICmpInitializable
    {
        // The Movable component; it is assigned directly below this.
        private Movable movable;

        // OnInit allows us to perform actions on component activations.
        // We will assign the Movable component to it's variable here,
        // in the Activation Context.
        // The input keys are also assigned here.
        void ICmpInitializable.OnInit(Component.InitContext context)
        {
            // If tit is the Activation Context, assign the Movable.
            if (context == InitContext.Activate)
                movable = this.GameObj.GetComponent<Movable>();
        }

        // We can do actions each frame update in OnUpdate.
        // This is where we will check for input.
        void ICmpUpdatable.OnUpdate()
        {
            // If the Movable component was found...
            if (movable != null)
            {
                // We will use named parameters here, to save space and typing.
                // We can specify only as many parameters as we need, since we
                // made all the parameters optional.

                // Move forwards...
                if (DualityApp.Keyboard.KeyHit(Key.Up)) movable.Move(forward: true);

                // Move to the right...
                if (DualityApp.Keyboard.KeyHit(Key.Right)) movable.Move(right: true);

                // Move to the left...
                if (DualityApp.Keyboard.KeyHit(Key.Left)) movable.Move(left: true);

                // Move backwards...
                if (DualityApp.Keyboard.KeyHit(Key.Down)) movable.Move(backward: true);
            }
        }

        // This function is unused in this component.
        void ICmpInitializable.OnShutdown(Component.ShutdownContext context) { }
    }
}
