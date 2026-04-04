using Assets.Scripts.ThirdPersonGame.Core.Minigames;
using System;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class Player
    {
        private readonly Input _input;

        private readonly PlayerMover _playerMover;
        private readonly PlayerRotator _playerRotator;
        private readonly Inventory _inventory;
        private readonly Animator _animator;
        private readonly Collider _collider;

        private CharacterBehaviourState _behaviourState;
        private int _danceActionZoneIndex;
        private int _danceActionZoneCount = 2;

        public Player(PlayerMover playerMover,
            PlayerRotator playerRotator,
            Inventory inventory,
            Collider collider,
            Input input,
            Animator animator)
        {
            _playerMover = playerMover;
            _playerRotator = playerRotator;
            _inventory = inventory;
            _collider = collider;
            _input = input;
            _animator = animator;

            _input.ToggleDanceStateActionPerformed += ToggleBehaviourState;
            _input.DanceActionPerformed += HandleDanceActionInput;

            Dance.ActionCaused += HandleDanceActionCaused;
            Dance.ActionCompleted += HandleActionComplete;
            Dance.DanceSucceded += HandleDanceEnd;

            DanceScore.SpecialScoreValueReached += HandleDanceSpecialScoreValue;
        }

        public Dance Dance { get; } = new();
        public DanceScore DanceScore { get; } = new();

        public event Action<CharacterBehaviourState> BehaviourStateChanged;
        public event EventHandler DanceStarted;
        public event EventHandler DanceInterrupted;
        public event Action<int> ActionCaused;
        public event Action<int, bool> DanceActionPerformed;

        public void Update()
        {
            if (_behaviourState != CharacterBehaviourState.Default)
                return;

            _playerMover.Move(_input.MovementInput);
            _playerMover.Jump(_input.IsJumpPressed);
            _playerRotator.Update();
        }

        public bool IsColliderMatch(Collider collider)
        {
            return _collider == collider;
        }

        public void AddCoinToInventory(IReadOnlyCoin coin)
        {
            _inventory.AddCoin(coin);
        }

        private void ToggleBehaviourState()
        {
            if (!_playerMover.IsGrounded())
                return;

            if (_behaviourState == CharacterBehaviourState.Default)
            {
                _behaviourState = CharacterBehaviourState.Dance;
                _animator.TrySetAnimatorState(AnimatorState.Dance);
                DanceScore.Reset();
                Dance.Start();
                DanceStarted?.Invoke(this, new EventArgs());
            }
            else
            {
                _behaviourState = CharacterBehaviourState.Default;
                _animator.TrySetAnimatorState(AnimatorState.Default);
                Dance.Stop();
                DanceInterrupted?.Invoke(this, new EventArgs());
            }

            BehaviourStateChanged?.Invoke(_behaviourState);
        }

        private void HandleDanceActionInput(int actionIndex)
        {
            if (_behaviourState != CharacterBehaviourState.Dance)
                return;

            if (actionIndex != _danceActionZoneIndex)
                return;

            Dance.DoAction();
        }

        private void HandleDanceEnd(bool isDanceSucceded)
        {
            _behaviourState = CharacterBehaviourState.Default;
            _animator.TrySetAnimatorState(AnimatorState.Default);

            BehaviourStateChanged?.Invoke(_behaviourState);
            DanceInterrupted?.Invoke(this, new EventArgs());
        }

        private void HandleDanceActionCaused()
        {
            var actionZoneIndex = UnityEngine.Random.Range(0, _danceActionZoneCount);
            _danceActionZoneIndex = actionZoneIndex;
            ActionCaused?.Invoke(_danceActionZoneIndex);
        }

        private void HandleActionComplete(bool isSuccess)
        {
            if (isSuccess)
                DanceScore.AddScoreValue(25);

            DanceActionPerformed?.Invoke(_danceActionZoneIndex, isSuccess);
        }

        private void HandleDanceSpecialScoreValue()
        {
            _animator.SetTrigger(DanceAnimatorControllerParameters.SpecialDance);
        }
    }

    public enum CharacterBehaviourState
    {
        Default,
        Dance,
    }
}
