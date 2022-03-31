﻿namespace Zinnia.Process.Moment
{
    using Malimbe.MemberChangeMethod;
    using UnityEngine;
    using Zinnia.Extension;

    /// <summary>
    /// Wrapper for an <see cref="IProcessable"/> process that has a state to determine when it is to be processed.
    /// </summary>
    public class MomentProcess : MonoBehaviour, IProcessable
    {
        /// <summary>
        /// The source process to attach to the moment.
        /// </summary>
        [Tooltip("The source process to attach to the moment.")]
        [SerializeField]
        private ProcessContainer _source;
        public ProcessContainer Source
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
            }
        }
        /// <summary>
        /// The process only executes if the <see cref="GameObject"/> is active and the <see cref="Component"/> is enabled.
        /// </summary>
        [Tooltip("The process only executes if the GameObject is active and the Component is enabled.")]
        [SerializeField]
        private bool _onlyProcessOnActiveAndEnabled = true;
        public bool OnlyProcessOnActiveAndEnabled
        {
            get
            {
                return _onlyProcessOnActiveAndEnabled;
            }
            set
            {
                _onlyProcessOnActiveAndEnabled = value;
            }
        }
        /// <summary>
        /// The interval in seconds defining how often to process the <see cref="Process"/>. Negative values will be clamped to zero.
        /// </summary>
        [Tooltip("The interval in seconds defining how often to process the Process. Negative values will be clamped to zero.")]
        [SerializeField]
        private float _interval;
        public float Interval
        {
            get
            {
                return _interval;
            }
            set
            {
                _interval = value;
            }
        }
        /// <summary>
        /// When to call <see cref="Process"/> the next time. Updated automatically based on <see cref="Interval"/> after <see cref="Process"/> has been called.
        /// </summary>
        public float NextProcessTime { get; set; }

        /// <summary>
        /// Clears <see cref="Source"/>.
        /// </summary>
        public virtual void ClearSource()
        {
            if (!this.IsValidState())
            {
                return;
            }

            Source = default;
        }

        /// <summary>
        /// Calls <see cref="IProcessable.Process"/> on <see cref="Source"/> if <see cref="NextProcessTime"/> allows.
        /// </summary>
        public virtual void Process()
        {
            if (NextProcessTime <= Time.time)
            {
                ProcessNow();
            }
        }

        /// <summary>
        /// Calls <see cref="IProcessable.Process"/> on <see cref="Source"/>, ignoring whether <see cref="NextProcessTime"/> allows.
        /// </summary>
        public virtual void ProcessNow()
        {
            if (Source == null || (OnlyProcessOnActiveAndEnabled && !isActiveAndEnabled))
            {
                return;
            }

            Source.Interface.Process();
            UpdateNextProcessTime();
        }

        /// <summary>
        /// Sets <see cref="NextProcessTime"/> to a random time between now and now plus <see cref="Interval"/>.
        /// </summary>
        public virtual void RandomizeNextProcessTime()
        {
            NextProcessTime = Time.time + (Random.value * Interval);
        }

        protected virtual void Awake()
        {
            RandomizeNextProcessTime();
        }

        protected virtual void OnEnable()
        {
            OnAfterIntervalChange();
        }

        /// <summary>
        /// Updates <see cref="NextProcessTime"/> to adjust to the latest <see cref="Interval"/>.
        /// </summary>
        protected virtual void UpdateNextProcessTime()
        {
            NextProcessTime = Time.time + Interval;
        }

        /// <summary>
        /// Called after <see cref="Interval"/> has been changed.
        /// </summary>
        [CalledAfterChangeOf(nameof(Interval))]
        protected virtual void OnAfterIntervalChange()
        {
            Interval = Mathf.Max(0f, Interval);
        }
    }
}