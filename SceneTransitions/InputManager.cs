using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    public class InputManager : Component, ICmpUpdatable
    {
        /// <summary>
        /// ContentRef to the scene we are going to switch to.
        /// </summary>
        public ContentRef<Scene> NextScene { get; set; }

        // The ICmpUpdatable interface allows us to perform actions
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
            }
        }

        /// <summary>
        /// A function to create a GameObject that will display the text "Hi.".
        /// </summary>
        public void SpawnHi()
        {
            // Initializing the GameObject, and the FormattedText the TextRenderer
            // component will use to display the text.
            GameObject    hiObject = new GameObject("Hi");
            FormattedText hiText   = new FormattedText();

            // Setting the text of the FormattedText which will be displayed by
            // the TextRenderer component.
            hiText.SourceText = "Hi.";

            // Adding the TextRenderer and Transform components to the GameObject.
            hiObject.AddComponent<TextRenderer>();
            hiObject.AddComponent<Transform>();

            // Setting the GameObject's TextRenderer's displayed text to hiText
            // that was created earlier
            // The color of the displayed text is also set to a random color.
            hiObject.GetComponent<TextRenderer>().Text = hiText;
            hiObject.GetComponent<TextRenderer>().ColorTint = MathF.Rnd.NextColorRgba();

            // Set the scale of the GameObject's Transform component, which will
            // make the text appear smaller.
            // We will also randomize the GameObject's position in the scene.
            hiObject.Transform.Scale = 0.5f;
            hiObject.Transform.Pos = new Vector3(MathF.Rnd.NextVector2(0f, 100f), 1f);

            // Add the GameObject to the scene.
            this.GameObj.ParentScene.AddObject(hiObject);
        }
    }
}
