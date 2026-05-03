using UnityEngine;
using UnityEngine.Profiling;
using Fusion;
using Fusion.Addons.KCC;

namespace MultiplayCore
{
	[DefaultExecutionOrder(-5)]
	public sealed class Agent : ContextBehaviour, ISortedUpdate
	{
		// PUBLIC METHODS

		public bool IsObserved => Context != null && Context.ObservedAgent == this;

		public AgentInput AgentInput => _agentInput;
		public Interactions Interactions => _interactions;
		public Character Character => _character;
		public Weapons Weapons => _weapons;
		public Health Health => _health;
		public AgentSenses Senses => _senses;
		public Jetpack Jetpack => _jetpack;
		public AgentVFX Effects => _agentVFX;
		public AgentInterestView InterestView => _interestView;

		[Networked]
		public NetworkBool LeftSide { get; private set; }

		// PRIVATE MEMBERS

		[SerializeField]
		private float _jumpPower;
		[SerializeField]
		private float _topCameraAngleLimit;
		[SerializeField]
		private float _bottomCameraAngleLimit;
		[SerializeField]
		private GameObject _visualRoot;

		[Header("Fall Damage")]
		[SerializeField]
		private float _minFallDamage = 5f;
		[SerializeField]
		private float _maxFallDamage = 200f;
		[SerializeField]
		private float _maxFallDamageVelocity = 20f;
		[SerializeField]
		private float _minFallDamageVelocity = 5f;

		private AgentInput _agentInput;
		private Interactions _interactions;
		private AgentFootsteps _footsteps;
		private Character _character;
		private Weapons _weapons;
		private Jetpack _jetpack;
		private AgentSenses _senses;
		private Health _health;
		private AgentVFX _agentVFX;
		private AgentInterestView _interestView;
		private SortedUpdateInvoker _sortedUpdateInvoker;
		private Quaternion _cachedLookRotation;
		private Quaternion _cachedPitchRotation;

	}
}