using System;
using System.Collections.Generic;
using System.Linq;

using Duality;
using Duality.Input;
using Duality.Drawing;
using Duality.Resources;
using Duality.Components;
using Duality.Components.Renderers;

namespace SceneTransitions
{
    /// <summary>
    /// This is a component to attach to a GameObject in a scene.
    /// It will receive input and take actions based on the input.
    /// It also has a property containing a ContentRef of the next scene.
    /// </summary>
    public class InputManager : Component, ICmpInitializable, ICmpUpdatable
    {
        /// <summary>
        /// ContentRef to the scene we are going to switch to.
        /// </summary>
        public ContentRef<Scene> NextScene { get; set; }

        // The ICmpInitializable interface exposes two functions, OnInit and OnShutdown
        // (located at the bottom of this file). Here, in OnInit, it allows us to
        // perform actions on initialization of this component, such as addition,
        // activation, entering a scene et cetera.
        // We will do some startup checks here.
        void ICmpInitializable.OnInit(Component.InitContext context)
        {
            // InitContext checking is important. If you do not check for context, you
            // will end up running code put here many times, in contexts you may not wish
            // to check for, since the OnInit function is not specific to any context.

            // We are using the Component Activation InitContext.
            if (context == InitContext.Activate)
            {
                // Here we check whether the sample is being run within the editor.
                if (DualityApp.ExecEnvironment == DualityApp.ExecutionEnvironment.Editor)
                {
                    // Write an error to the log, and display the message in the scene.
                    Log.Game.WriteWarning("Please run through launcher. Editor does not allow full demonstration of scene switching.");
                   // this.GameObj.ParentScene.FindGameObject("EditorMessage").GetComponent<TextRenderer>().Active = true;
                }
            }
        }

        // The ICmpUpdatable interface allows us to perform actions on
        // every frame update.
        // We will check for input here.
        void ICmpUpdatable.OnUpdate()
        {
            // If the ContentRef to the next scene exists...
            if (NextScene != null)
            {
                // If the "A" key is pressed, then spawn an object displaying the text "Hi.".
                if (DualityApp.Keyboard.KeyHit(Key.A)) this.SpawnHi();

                // If the "S" key is pressed, then switch to the next scene.
                if (DualityApp.Keyboard.KeyHit(Key.S)) SceneSwitcher.Switch(NextScene);

                // If the "D" key is pressed, then dispose this scene, then switch
                // to the next scene.
                // There is no need to cast a Scene resource to a ContentRef, as
                // every resource has an implicit cast to it's corresponding
                // ContentRef<T>.
                if (DualityApp.Keyboard.KeyHit(Key.D)) SceneSwitcher.DisposeAndSwitch
                                                                        (this.GameObj.ParentScene, NextScene);

                // If the "R" key is pressed, then reload the current scene.
                if (DualityApp.Keyboard.KeyHit(Key.R)) SceneSwitcher.Reload();

                // If this scene's name is "SceneC", and if the "C" key is pressed,
                // then save this scene.
                if (this.GameObj.ParentScene.Name == "SceneC" 
                    && DualityApp.Keyboard.KeyHit(Key.C)) SceneSwitcher.SaveSceneCopy
                                                                        (this.GameObj.ParentScene);
            }
        }

        /// <summary>
        /// A function to create a GameObject that will display the text "Hi.".
        /// </summary>
        public void SpawnHi()
        {
            // Initializing the actual Hi GameObject.
            // This initialized GameObject will be a child object of the "HiObjects"
            // GameObject.
            GameObject hiObject = new GameObject("Hi");

            // Adding the TextRenderer and Transform components to the GameObject.
            hiObject.AddComponent<TextRenderer>();
            hiObject.AddComponent<Transform>();

            // Setting the GameObject's TextRenderer's displayed source text to "Hi.".
            // The color of the displayed text is also set to a random color.
            hiObject.GetComponent<TextRenderer>().Text.ApplySource("Hi.");
            hiObject.GetComponent<TextRenderer>().ColorTint = MathF.Rnd.NextColorRgba();

            // Set the scale of the GameObject's Transform component, which will
            // make the text appear smaller.
            // We will also randomize the GameObject's position in the scene.
            hiObject.Transform.Scale = 0.5f;
            hiObject.Transform.Pos = new Vector3(MathF.Rnd.NextVector2(0f, 100f), 1f);

            // Add the GameObject to the scene.
            this.GameObj.ParentScene.AddObject(hiObject);
        }

        // OnShutdown allows you to perform actions on shutdown of this component,
        // such as removal, deactivation, and leaving a scene et cetera.
        // As in the OnInit function, remember to check for context if you don't
        // want your code to run every time OnShutdown is called, even with another
        // context than what you actually wanted.

        // This function is not used in this sample.
        void ICmpInitializable.OnShutdown(Component.ShutdownContext context) { }
    }
}
