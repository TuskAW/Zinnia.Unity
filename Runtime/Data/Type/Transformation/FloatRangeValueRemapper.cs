﻿namespace Zinnia.Data.Type.Transformation
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using Zinnia.Extension;

    /// <summary>
    /// Transforms a <see cref="float"/> by remapping from a range to a new range.
    /// </summary>
    /// <example>
    /// 2f -> From(0f, 10f), To(0f, 1f), Mode(Lerp) = 0.2f
    /// 2f -> From(0f, 10f), To(1f, 0f), Mode(Lerp) = 0.8f
    /// 2f -> From(10f, 0f), To(0f, 1f), Mode(Lerp) = 0.8f
    /// 2f -> From(10f, 0f), To(1f, 0f), Mode(Lerp) = 0.2f
    /// 2f -> From(0f, 10f), To(0f, 1f), Mode(SmoothStep) = 0.104f
    /// </example>
    public class FloatRangeValueRemapper : Transformer<float, float, FloatRangeValueRemapper.UnityEvent>
    {
        /// <summary>
        /// Defines the event with the remapped <see cref="float"/> value.
        /// </summary>
        [Serializable]
        public class UnityEvent : UnityEvent<float> { }

        /// <summary>
        /// The range of the value from.
        /// </summary>
        [Tooltip("The range of the value from.")]
        [SerializeField]
        private FloatRange _from = new FloatRange(0f, 1f);
        public FloatRange From
        {
            get
            {
                return _from;
            }
            set
            {
                _from = value;
            }
        }

        /// <summary>
        /// The range of the value remaps to.
        /// </summary>
        [Tooltip("The range of the value remaps to.")]
        [SerializeField]
        private FloatRange _to = new FloatRange(0f, 1f);
        public FloatRange To
        {
            get
            {
                return _to;
            }
            set
            {
                _to = value;
            }
        }

        /// <summary>
        /// The mode to use when remapping.
        /// </summary>
        public enum OutputMode
        {
            /// <summary>
            /// Linearly interpolates.
            /// </summary>
            Lerp,
            /// <summary>
            /// Interpolates with smoothing at the limits
            /// </summary>
            SmoothStep
        }

        /// <summary>
        /// The mode to use when remapping.
        /// </summary>
        [Tooltip("The mode to use when remapping.")]
        [SerializeField]
        private OutputMode _mode = OutputMode.Lerp;
        public OutputMode Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;
            }
        }

        /// <summary>
        /// Sets the <see cref="Mode"/>.
        /// </summary>
        /// <param name="index">The index of the <see cref="OutputMode"/>.</param>
        public virtual void SetMode(int index)
        {
            Mode = EnumExtensions.GetByIndex<OutputMode>(index);
        }

        /// <summary>
        /// Transforms the given <see cref="float"/> by remapping to a new range.
        /// </summary>
        /// <param name="input">The value to remap.</param>
        /// <returns>A new <see cref="float"/> remapped.</returns>
        protected override float Process(float input)
        {
            float t = Mathf.InverseLerp(From.minimum, From.maximum, input);
            return Mode == OutputMode.Lerp ?
                Mathf.Lerp(To.minimum, To.maximum, t) :
                Mathf.SmoothStep(To.minimum, To.maximum, t);
        }
    }
}