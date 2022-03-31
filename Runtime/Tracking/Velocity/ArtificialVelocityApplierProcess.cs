﻿namespace Zinnia.Tracking.Velocity
{
    using UnityEngine;
    using Zinnia.Extension;
    using Zinnia.Process;

    /// <summary>
    /// Applies artificial velocities to the <see cref="Target"/> by changing the <see cref="Transform"/> properties.
    /// </summary>
    public class ArtificialVelocityApplierProcess : MonoBehaviour, IProcessable
    {
        /// <summary>
        /// The object to apply the artificial velocities to.
        /// </summary>
        [Tooltip("The object to apply the artificial velocities to.")]
        [SerializeField]
        private GameObject _target;
        public GameObject Target
        {
            get
            {
                return _target;
            }
            set
            {
                _target = value;
            }
        }
        /// <summary>
        /// The velocity to apply.
        /// </summary>
        [Tooltip("The velocity to apply.")]
        [SerializeField]
        private Vector3 _velocity;
        public Vector3 Velocity
        {
            get
            {
                return _velocity;
            }
            set
            {
                _velocity = value;
            }
        }
        /// <summary>
        /// The angular velocity to apply.
        /// </summary>
        [Tooltip("The angular velocity to apply.")]
        [SerializeField]
        private Vector3 _angularVelocity;
        public Vector3 AngularVelocity
        {
            get
            {
                return _angularVelocity;
            }
            set
            {
                _angularVelocity = value;
            }
        }
        /// <summary>
        /// The drag to apply to reduce the directional velocity over time and to slow down <see cref="Target"/>.
        /// </summary>
        [Tooltip("The drag to apply to reduce the directional velocity over time and to slow down Target.")]
        [SerializeField]
        private float _drag = 1f;
        public float Drag
        {
            get
            {
                return _drag;
            }
            set
            {
                _drag = value;
            }
        }
        /// <summary>
        /// The angular drag to apply to reduce the rotational velocity over time and to slow down <see cref="Target"/>.
        /// </summary>
        [Tooltip("The angular drag to apply to reduce the rotational velocity over time and to slow down Target.")]
        [SerializeField]
        private float _angularDrag = 0.5f;
        public float AngularDrag
        {
            get
            {
                return _angularDrag;
            }
            set
            {
                _angularDrag = value;
            }
        }
        /// <summary>
        /// The tolerance the velocity can be within zero to be considered nil.
        /// </summary>
        [Tooltip("The tolerance the velocity can be within zero to be considered nil.")]
        [SerializeField]
        private float _nilVelocityTolerance = 0.001f;
        public float NilVelocityTolerance
        {
            get
            {
                return _nilVelocityTolerance;
            }
            set
            {
                _nilVelocityTolerance = value;
            }
        }
        /// <summary>
        /// The tolerance the angular velocity can be within zero to be considered nil.
        /// </summary>
        [Tooltip("The tolerance the angular velocity can be within zero to be considered nil.")]
        [SerializeField]
        private float _nilAngularVelocityTolerance = 0.001f;
        public float NilAngularVelocityTolerance
        {
            get
            {
                return _nilAngularVelocityTolerance;
            }
            set
            {
                _nilAngularVelocityTolerance = value;
            }
        }

        /// <summary>
        /// Determine if we can process.
        /// </summary>
        protected bool canProcess = false;

        /// <summary>
        /// Clears <see cref="Target"/>.
        /// </summary>
        public virtual void ClearTarget()
        {
            if (!this.IsValidState())
            {
                return;
            }

            Target = default;
        }

        /// <summary>
        /// Sets the <see cref="Velocity"/> x value.
        /// </summary>
        /// <param name="value">The value to set to.</param>
        public virtual void SetVelocityX(float value)
        {
            Velocity = new Vector3(value, Velocity.y, Velocity.z);
        }

        /// <summary>
        /// Sets the <see cref="Velocity"/> y value.
        /// </summary>
        /// <param name="value">The value to set to.</param>
        public virtual void SetVelocityY(float value)
        {
            Velocity = new Vector3(Velocity.x, value, Velocity.z);
        }

        /// <summary>
        /// Sets the <see cref="Velocity"/> z value.
        /// </summary>
        /// <param name="value">The value to set to.</param>
        public virtual void SetVelocityZ(float value)
        {
            Velocity = new Vector3(Velocity.x, Velocity.y, value);
        }

        /// <summary>
        /// Increments the <see cref="Velocity"/> by the given value.
        /// </summary>
        /// <param name="value">The value to increment by.</param>
        public virtual void IncrementVelocity(Vector3 value)
        {
            Velocity += value;
        }

        /// <summary>
        /// Reset <see cref="Velocity"/> to <see cref="Vector3.zero"/>.
        /// </summary>
        public virtual void ClearVelocity()
        {
            Velocity = Vector3.zero;
        }

        /// <summary>
        /// Sets the <see cref="AngularVelocity"/> x value.
        /// </summary>
        /// <param name="value">The value to set to.</param>
        public virtual void SetAngularVelocityX(float value)
        {
            AngularVelocity = new Vector3(value, AngularVelocity.y, AngularVelocity.z);
        }

        /// <summary>
        /// Sets the <see cref="AngularVelocity"/> y value.
        /// </summary>
        /// <param name="value">The value to set to.</param>
        public virtual void SetAngularVelocityY(float value)
        {
            AngularVelocity = new Vector3(AngularVelocity.x, value, AngularVelocity.z);
        }

        /// <summary>
        /// Sets the <see cref="AngularVelocity"/> z value.
        /// </summary>
        /// <param name="value">The value to set to.</param>
        public virtual void SetAngularVelocityZ(float value)
        {
            AngularVelocity = new Vector3(AngularVelocity.x, AngularVelocity.y, value);
        }

        /// <summary>
        /// Increments the <see cref="AngularVelocity"/> by the given value.
        /// </summary>
        /// <param name="value">The value to increment by.</param>
        public virtual void IncrementAngularVelocity(Vector3 value)
        {
            AngularVelocity += value;
        }

        /// <summary>
        /// Reset <see cref="AngularVelocity"/> to <see cref="Vector3.zero"/>.
        /// </summary>
        public virtual void ClearAngularVelocity()
        {
            AngularVelocity = Vector3.zero;
        }

        /// <summary>
        /// Applies the velocity data to the <see cref="Target"/>.
        /// </summary>
        public virtual void Apply()
        {
            if (!this.IsValidState())
            {
                return;
            }

            canProcess = true;
        }

        /// <inheritdoc />
        public virtual void Process()
        {
            if (!canProcess)
            {
                return;
            }

            if (!Velocity.ApproxEquals(Vector3.zero, NilVelocityTolerance) || !AngularVelocity.ApproxEquals(Vector3.zero, NilAngularVelocityTolerance))
            {
                float deltaTime = Time.inFixedTimeStep ? Time.fixedDeltaTime : Time.deltaTime;
                Velocity = Vector3.Lerp(Velocity, Vector3.zero, Drag * deltaTime);
                AngularVelocity = Vector3.Lerp(AngularVelocity, Vector3.zero, AngularDrag * deltaTime);
                Target.transform.localRotation *= Quaternion.Euler(AngularVelocity);
                Target.transform.localPosition += Velocity * deltaTime;
            }
            else
            {
                Velocity = Vector3.zero;
                AngularVelocity = Vector3.zero;
                canProcess = false;
            }
        }

        /// <summary>
        /// Cancels the <see cref="decelerationRoutine"/>.
        /// </summary>
        public virtual void CancelDeceleration()
        {
            canProcess = false;
        }

        protected virtual void OnDisable()
        {
            CancelDeceleration();
        }
    }
}