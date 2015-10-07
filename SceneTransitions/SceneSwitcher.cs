using System;
using System.Collections.Generic;
using System.Linq;

using Duality;
using Duality.Resources;
using Duality.Components.Renderers;
using Duality.Drawing;

namespace SceneTransitions
{
    /// <summary>
    /// This static class provides functions related to scenes, such as switching,
    /// dispose then switch, scene reloading and scene saving.
    /// </summary>
    public static class SceneSwitcher
    {
        // We are going to use ContentRefs instead of using Scene Resources directly

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
            // from memory, before the switch to the next scene commences.

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

        /// <summary>
        /// Function to save a copy of the specified scene.
        /// </summary>
        /// <param name="scene">The scene to be saved.</param>
        public static void SaveSceneCopy(ContentRef<Scene> scene)
        {
            // This is the path to which the file will be saved. It is constructed by
            // combining the Duality Data directory path with the sample name, which
            // results in the actual directory the file will be saved to.
            // This result is concatenated with the actual file name.
            // The file name is constructed by concatenating the specified scene's name,
            // along with "_Copy" and the Scene Resource file extension.
            string filePath = Duality.IO.PathOp.Combine(DualityApp.DataDirectory, @"SceneTransitions\")
                                + (scene.Name + "_Copy" + Resource.GetFileExtByType<Scene>());

            // Here we save the scene.
            scene.Res.Save(filePath);

            // The "Press to save" object's TextRenderer.
            TextRenderer textRenderer = scene.Res.FindGameObject("Text5").GetComponent<TextRenderer>();

            // Set the "Press to save" object's TextRenderer's source text and color tint,
            // if the TextRenderer was found.
            if (textRenderer != null)
            {
                textRenderer.Text.ApplySource("The saved scene can be found in the Duality Data directory.");
                textRenderer.ColorTint = ColorRgba.Green;
            }
        }
    }
}
