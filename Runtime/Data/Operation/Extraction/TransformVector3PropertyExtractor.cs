﻿namespace Zinnia.Data.Operation.Extraction
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// Provides a basis for extracting properties from a <see cref="Transform"/>.
    /// </summary>
    public abstract class TransformVector3PropertyExtractor : Vector3Extractor<GameObject, TransformVector3PropertyExtractor.UnityEvent>
    {
        /// <summary>
        /// Defines the event with the specified <see cref="Vector3"/>.
        /// </summary>
        [Serializable]
        public class UnityEvent : UnityEvent<Vector3> { }

        /// <summary>
        /// Determines whether to extract the local property or the world property.
        /// </summary>
        [Tooltip("Determines whether to extract the local property or the world property.")]
        [SerializeField]
        private bool _useLocal;
        public bool UseLocal
        {
            get
            {
                return _useLocal;
            }
            set
            {
                _useLocal = value;
            }
        }

        /// <summary>
        /// The last extracted property value.
        /// </summary>
        [Obsolete("Use `Result` instead.")]
        public Vector3 LastExtractedValue => Result.GetValueOrDefault();
    }
}