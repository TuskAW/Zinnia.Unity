﻿namespace Zinnia.Rule
{
    using UnityEngine;

    /// <summary>
    /// Determines whether a <see cref="GameObject"/>'s <see cref="GameObject.layer"/> is part of a list.
    /// </summary>
    public class AnyLayerRule : GameObjectRule
    {
        [Tooltip("The layers to check against.")]
        [SerializeField]
        private LayerMask _layerMask;
        /// <summary>
        /// The layers to check against.
        /// </summary>
        public LayerMask LayerMask
        {
            get
            {
                return _layerMask;
            }
            set
            {
                _layerMask = value;
            }
        }

        /// <inheritdoc />
        protected override bool Accepts(GameObject targetGameObject)
        {
            return (LayerMask & (1 << targetGameObject.layer)) != 0;
        }
    }
}