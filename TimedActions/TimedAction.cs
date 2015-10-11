using System;
using System.Collections.Generic;
using System.Linq;

namespace TimedActions
{
    /// <summary>
    /// The TimedActions abstract class provides the basic infrastructure to
    /// create time-based triggers.
    /// </summary>
    public abstract class TimedAction
    {
        // Default values for the properties.
        private static readonly float DEFAULT_TIMELIMIT = 5f;
        private static readonly int DEFAULT_TRIGGERS  = 1;

        // This backing field will store the time limit of this timed action
        // in the form of milliseconds.
        private float timeLimit;

        /// <summary>
        /// The time limit of the action, in seconds.
        /// </summary>
        public float TimeLimit
        {
            get { return this.timeLimit / 1000; }
            set { this.timeLimit = value * 1000; }
        }

        /// <summary>
        /// The amount of triggers this action needs to pass to be completed.
        /// </summary>
        public int Triggers { get; set; }

        /// <summary>
        /// Returns whether this action has been completed yet.
        /// </summary>
        public bool Completed { get; set; }

        public TimedAction()
        {
            // The properties are set to default values when this class is initialized.
            TimeLimit = DEFAULT_TIMELIMIT;
            Triggers = DEFAULT_TRIGGERS;
        }

        /// <summary>
        /// This function is where the actual action that will be executed after
        /// the time limit is passed is defined.
        /// This abstract function does not provide any default implementation:
        /// all derived classes must provide their own.
        /// </summary>
        protected abstract void Action();

        // How about a Trigger() function here?

        /// <summary>
        /// The function to actually run the timed action until it has been completed.
        /// It is a virtual function: it provides an implementation, but it can be overridden.
        /// </summary>
        /// <param name="timeStep">The amount of time that has passed since the last frame, in seconds.
        /// Can be calculated using Time.TimeMult * Time.SPFMult.</param>
        public virtual void Run(float timeStep)
        {
            if (!Completed) this.Action();
        }
    }
}
