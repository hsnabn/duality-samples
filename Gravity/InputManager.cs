using System;
using System.Collections.Generic;
using System.Linq;

using Duality;
using Duality.Input;
using Duality.Resources;
using Duality.Components;

namespace Gravity
{
    /// <summary>
    /// This component receives user input and performs actions based on the input.
    /// </summary>
    public class InputManager : Component, ICmpUpdatable
    {
        /// <summary>
        /// The prefab used to create a new Ball object.
        /// </summary>
        public ContentRef<Prefab> BallPrefab { get; set; }

        /// <summary>
        /// The prefab used to create a new Gravitator object.
        /// </summary>
        public ContentRef<Prefab> GravitatorPrefab { get; set; }

        // This function allows us to perform actions every game update.
        void ICmpUpdatable.OnUpdate()
        {
            // If the "R" key is hit, then reload the current scene.
            if (DualityApp.Keyboard.KeyHit(Key.R)) Scene.Reload();

            // If the "S" key is hit, then...
            if (DualityApp.Keyboard.KeyHit(Key.S))
            {
                // Enumerate over all Gravitators in the scene.
                foreach (Gravitator gravitator in this.GameObj.ParentScene.FindComponents<Gravitator>())
                {
                    // If the gravitator exists...
                    if (gravitator != null)
                    {
                        // If the gravitator is set to Attract mode...
                        if (gravitator.GravitationType == Gravitator.GravityType.Attract)
                            // Set it to Repel mode.
                            gravitator.GravitationType = Gravitator.GravityType.Repel;

                        // Else, if the gravitator is set to Repel mode...
                        else if (gravitator.GravitationType == Gravitator.GravityType.Repel)
                            // Set it to Attract mode.
                            gravitator.GravitationType = Gravitator.GravityType.Attract;
                    }
                }
            }

            // If the "F" key is hit...
            if (DualityApp.Keyboard.KeyHit(Key.F))
            {
                // Enumerate over all Gravitators in the scene.
                foreach (Gravitator gravitator in this.GameObj.ParentScene.FindComponents<Gravitator>())
                {
                    // If the gravitator exists, then increase it's force multiplier.
                    if (gravitator != null) gravitator.ForceMultiplier += 1f;
                }
            }

            // If the left mouse button is hit...
            if (DualityApp.Mouse.ButtonHit(MouseButton.Left))
            {
                // Get the world space position of the mouse pointer.
                Vector2 mouseWorldPos = this.GameObj.ParentScene.FindComponent<Camera>()
                                            .GetSpaceCoord(DualityApp.Mouse.Pos).Xy;

                // Create the ball at the world space mouse position.
                CreateBall(mouseWorldPos);
            }

            // If the right mouse button is hit...
            if (DualityApp.Mouse.ButtonHit(MouseButton.Right))
            {
                // Get the world space position of the mouse pointer.
                Vector2 mouseWorldPos = this.GameObj.ParentScene.FindComponent<Camera>()
                                            .GetSpaceCoord(DualityApp.Mouse.Pos).Xy;

                // Create the gravitator at the world space mouse position.
                CreateGravitator(mouseWorldPos);
            }
        }

        /// <summary>
        /// This function creates a Ball object using the Ball prefab assigned in this
        /// component, and places it at a specified position.
        /// </summary>
        /// <param name="position">The location the Ball object is to be placed at.</param>
        public void CreateBall(Vector2 position)
        {
            // Instantiate a new GameObject called "ball"
            GameObject ball = this.BallPrefab.Res.Instantiate();

            // Break the newly instantiated ball's prefab link.
            ball.BreakPrefabLink();

            // You can use alternative functions here instead; for example,
            // Transform.Pos.MoveToAbs.
            ball.Transform.Pos = new Vector3(position);

            // Actually add the ball to the scene.
            this.GameObj.ParentScene.AddObject(ball);
        }

        /// <summary>
        /// This function creates a Gravitator object using the Gravitator prefab assigned 
        /// in this component, and places it at a specified position.
        /// </summary>
        /// <param name="position">The location the Gravitator object is to be placed at.</param>
        public void CreateGravitator(Vector2 position)
        {
            // Instantiate a new GameObject called "gravitator"
            GameObject gravitator = this.GravitatorPrefab.Res.Instantiate();

            // Break the newly instantiated gravitator's prefab link.
            gravitator.BreakPrefabLink();

            // You can use alternative functions here instead; for example,
            // Transform.Pos.MoveToAbs.
            gravitator.Transform.Pos = new Vector3(position);

            // Actually add the gravitator to the scene.
            this.GameObj.ParentScene.AddObject(gravitator);
        }
    }
}
