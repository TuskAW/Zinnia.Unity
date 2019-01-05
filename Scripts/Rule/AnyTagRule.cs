﻿namespace Zinnia.Rule
{
    using UnityEngine;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Determines whether a <see cref="GameObject"/>'s <see cref="GameObject.tag"/> is part of a list.
    /// </summary>
    public class AnyTagRule : GameObjectRule
    {
        /// <summary>
        /// The tags to check against.
        /// </summary>
        public List<string> tags = new List<string>();

        /// <inheritdoc />
        protected override bool Accepts(GameObject targetGameObject)
        {
            return tags.Any(targetGameObject.CompareTag);
        }
    }
}