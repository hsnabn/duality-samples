using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Duality;
using Duality.Resources;

namespace SceneTransitions
{
    /// <summary>
    /// This static class provides functions related to scenes, such as switching,
    /// dispos then switch, and scene reloading.
    /// </summary>
    public static class SceneSwitcher
    {
        // We are  going to use ContentRefs instead of using Scene directly

        /// <summary>
        /// Function to switch to another scene.
        /// </summary>
        /// <param name="scene">The ContentRef of the scene to switch to.</param>
        public static void Switch(ContentRef<Scene> scene)
        {
            // Note that we are not doing any scene disposal here. This means that
            // the current scene will not be removed from memory, and that it will
            // retain changes made to it.
            Scene.SwitchTo(scene);
        }

        /// <summary>
        /// Function to switch to another scene after disposing the specified scene.
        /// </summary>
        /// <param name="nextScene">The ContentRef of the scene to dispose.</param>
        /// <param name="nextScene">The ContentRef of the scene to switch to.</param>
        public static void DisposeAndSwitch(ContentRef<Scene> disposeScene, 
                                            ContentRef<Scene> nextScene)
        {
            // In this function, the current scene will be disposed, or removed
            // from memory before the switch to the next scene commences.

            // We are using DisposeLater() for safety, it will only dispose the
            // scene after the current update cycle is over.
            disposeScene.Res.DisposeLater();
            Scene.SwitchTo(nextScene);
        }

        /// <summary>
        /// Function to reload the current scene.
        /// </summary>
        public static void Reload()
        {
            Scene.Reload();
        }
    }
}
