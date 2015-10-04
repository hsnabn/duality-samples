using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.Input;
using Duality.Resources;

namespace SceneTransitions
{
    // This is a component to attach to a GameObject in a scene.
    // It will receive input and take actions based on the input.
    public class InputManager : Component, ICmpUpdatable
    {
        // ContentRef to the scene we are going to switch to.
        public ContentRef<Scene> NextScene { get; set; }

        // The ICmpUpdatable interface allows us to perform actions
        // every frame update.
        // We will check for input here.
        void ICmpUpdatable.OnUpdate()
        {
            // Here we search the current scene for the SceneSwitcher component.
            SceneSwitcher switcher = this.GameObj.ParentScene.FindComponent<SceneSwitcher>();

            // If the SceneSwitcher component has been found, and if the ContentRef
            // to the next scene also exists...
            if (switcher != null && NextScene != null)
            {
                // If the "S" key is pressed, then switch to the next scene.
                if (DualityApp.Keyboard.KeyPressed(Key.S)) switcher.Switch(NextScene);
            }
        }
    }
}
