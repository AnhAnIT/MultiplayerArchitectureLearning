using Fusion;
using Fusion.Addons.KCC;
using Fusion.Plugin;

namespace MultiplayCore
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using MultiplayCore.UI;
    [DefaultExecutionOrder(-10)]
    public sealed partial class AgentInput : ContextBehaviour, IBeforeTick, IAfterAllTicks
    {
        // PUBLIC MEMBERS

        /// <summary>
        /// Holds input for fixed update.
        /// </summary>
        public GameplayInput 