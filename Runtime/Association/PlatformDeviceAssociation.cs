﻿namespace Zinnia.Association
{
    using System;
    using System.Text.RegularExpressions;
    using UnityEngine;
    using UnityEngine.XR;

    /// <summary>
    /// Holds <see cref="GameObject"/>s to (de)activate based on the current platform and loaded XR device type.
    /// </summary>
    [Obsolete("Use `RuleAssociation` instead.")]
    public class PlatformDeviceAssociation : GameObjectsAssociation
    {
        /// <summary>
        /// A regular expression to match the name of the current <see cref="RuntimePlatform"/>.
        /// </summary>
        [Tooltip("A regular expression to match the name of the current RuntimePlatform.")]
        [SerializeField]
        private string _platformPattern;
        public string PlatformPattern
        {
            get
            {
                return _platformPattern;
            }
            set
            {
                _platformPattern = value;
            }
        }
        /// <summary>
        /// A regular expression to match the name of the XR device that needs to be loaded.
        /// </summary>
        [Tooltip("A regular expression to match the name of the XR device that needs to be loaded.")]
        [SerializeField]
        private string _xrSdkPattern;
        public string XrSdkPattern
        {
            get
            {
                return _xrSdkPattern;
            }
            set
            {
                _xrSdkPattern = value;
            }
        }

        /// <summary>
        /// A regular expression to match the name of the XR model that is being used.
        /// </summary>
        [Tooltip("A regular expression to match the name of the XR model that is being used.")]
        [SerializeField]
        private string _xrModelPattern;
        public string XrModelPattern
        {
            get
            {
                return _xrModelPattern;
            }
            set
            {
                _xrModelPattern = value;
            }
        }

        /// <inheritdoc/>
        public override bool ShouldBeActive()
        {
            string modelName = "";
#if UNITY_2020_2_OR_NEWER
            InputDevice currentDevice = InputDevices.GetDeviceAtXRNode(XRNode.Head);
            modelName = currentDevice != null && currentDevice.name != null ? currentDevice.name : "";
#else
            modelName = XRDevice.model;
#endif
            return Regex.IsMatch(Application.platform.ToString(), PlatformPattern) &&
                Regex.IsMatch(XRSettings.loadedDeviceName, XrSdkPattern) &&
                Regex.IsMatch(modelName, XrModelPattern);
        }
    }
}