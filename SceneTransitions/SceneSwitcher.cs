using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.Resources;

namespace SceneTransitions
{
    // This is a component to attach to a GameObject in a scene.
    // It will allow switching to other scenes.
    public class SceneSwitcher : Component
    {
        // We are  going to use ContentRef<Scene> instead of using Scene directly

        // Function to switch to another scene.
        public void Switch(ContentRef<Scene> scene)
        {
            // Note that we are not doing any scene disposal here. This means that
            // the current scene will not be removed from memory, and that it will
            // retain changes made to it.
            Scene.SwitchTo(scene);
        }

        // Function to switch to another scene after disposing this scene.
        public void DisposeAndSwitch(ContentRef<Scene> scene)
        {
            // In this function, the current scene will be disposed, or removed
            // from memory before the switch to the next scene commences.

            // We are using DisposeLater() for safety, it will only dispose the
            // scene after the current update cycle is over.
            this.GameObj.ParentScene.DisposeLater();
            Scene.SwitchTo(scene);
        }

        // Function to reload the current scene
        public void Reload()
        {
            Scene.Reload();
        }
    }
}
