using System;
using System.Collections.Generic;
using System.Linq;

using Duality;
using Duality.Components.Physics;

namespace Gravity
{
    /// <summary>
    /// This component will gravitate other objects based on it's configuration.
    /// </summary>
    [RequiredComponent(typeof(RigidBody))]
    public class Gravitator: Component, ICmpUpdatable
    {
        // Range is not an auto-property because I felt it needed a default value.
        // feel free to change it into an auto-property if you'd like.
        private float range = 1000f;

        /// <summary>
        /// The maximum range at which this object will gravitate other objects.
        /// </summary>
        public float Range
        {
            get { return this.range; }
            set { this.range = value; }
        }

        private float multiplier = 1f;

        /// <summary>
        /// The amount with which the gravitational force is multiplied with.
        /// </summary>
        public float ForceMultiplier
        {
            get { return this.multiplier; }
            set { this.multiplier = value; }
        }

        // An enum containing the types of gravitation that can be caused by this
        // object.
        public enum GravityType
        {
            Attract,
            Repel
        }

        /// <summary>
        /// The type of gravitation that will be caused by this object.
        /// </summary>
        public GravityType GravitationType { get; set; }

        // We can perform actions every game update here.
        void ICmpUpdatable.OnUpdate()
        {
            // Gravitate any objects within range of this object.
            this.GravitateAll();
        }

        /// <summary>
        /// This function attracts all RigidBodies that are in a list of RigidBodies
        /// towards this GameObject. This code is heavily based on a 
        /// <a href="http://wiki.unity3d.com/index.php/Gravity">Unity3D Wiki page</a>.
        /// </summary>
        public void GravitateAll()
        {
            // Enumerate over every RigidBody in the scene.
            foreach (RigidBody rigidBody in this.GameObj.ParentScene.FindComponents<RigidBody>())
            {
                // If the RigidBody, well, exists, and if it's not this object's RigidBody...
                if (rigidBody != null && rigidBody != this.GameObj.GetComponent<RigidBody>())
                {
                    // The difference in distance between this object and the other object, as
                    // a Vector2.
                    Vector2 distanceOffset = (this.GameObj.Transform.Pos - rigidBody.GameObj.Transform.Pos).Xy;

                    // The force that will be applied to the other object. It depends on the
                    // distanceOffset, it's squared magnitude and the mass of the other
                    // RigidBody.
                    // The force is multiplied by the ForceMultiplier, which is by default
                    // set to 1.
                    Vector2 appliedForce = (distanceOffset / distanceOffset.LengthSquared * rigidBody.Mass) * ForceMultiplier;

                    // If the other distance between both objects is within the Range...
                    if (distanceOffset.Length <= Range)
                    {
                        // Here we do a switch on the GravityType selected...
                        switch (GravitationType)
                        {
                            // If the GravityType is to Attract...
                            case GravityType.Attract:
                                // Then just apply the force as it is...
                                rigidBody.ApplyWorldForce(appliedForce);
                                break;

                            // If the GravityType is to Repel...
                            case GravityType.Repel:
                                // Then apply the force as negative.
                                rigidBody.ApplyWorldForce(-appliedForce);
                                break;
                        }
                    }
                }
            }
        }
    }
}
