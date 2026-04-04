using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Core.Minigames
{
    public class Dance
    {
        private readonly float _danceDuration = 15f;
        private readonly float _timeBetweenActions = 1.2f;
        private readonly float _actionThreshold = 0.3f;
        private readonly float _actionDuration = 1f;
        private readonly int _actionCountPerDance;

        private Coroutine _danceUpdate;

        private float _currentTime;
        private float _nextActionEndTime;
        private float _currentActionEndTime;
        private int _succeededActionCount;

        private bool _isCurrentActionHandled = false;

        public float ActionDuration => _actionDuration;

        public event Action ActionCaused;
        public event Action<bool> ActionCompleted;
        public event Action<bool> DanceSucceded;

        public Dance()
        {
            _actionCountPerDance = (int)(_danceDuration / _timeBetweenActions);
        }

        public void Start()
        {
            _currentActionEndTime = _timeBetweenActions;
            _nextActionEndTime = _timeBetweenActions;
            _succeededActionCount = 0;

            _danceUpdate = CoroutineRunner.StartCoroutine(Update());
        }

        public void Stop()
        {
            CoroutineRunner.StopCoroutine(_danceUpdate);
            Finish();
        }

        public void DoAction()
        {
            if (_isCurrentActionHandled)
                return;

            bool isActionSuccessful = IsActionSuccessful();
            if (isActionSuccessful)
                _succeededActionCount++;

            _isCurrentActionHandled = true;

            ActionCompleted?.Invoke(isActionSuccessful);
        }

        private bool IsActionSuccessful()
        {
            var error = _currentActionEndTime - _currentTime;
            return error <= _actionThreshold;
        }

        private bool IsDanceSucceed()
        {
            return _succeededActionCount > _actionCountPerDance * 0.5;
        }

        private IEnumerator Update()
        {
            while (_currentTime < _danceDuration)
            {
                _currentTime += Time.deltaTime;

                if (_currentTime >= _currentActionEndTime
                    && (!_isCurrentActionHandled))
                {
                    ActionCompleted?.Invoke(false);
                    _isCurrentActionHandled = true;
                }

                if (_nextActionEndTime - _currentTime <= _actionDuration)
                {
                    _currentActionEndTime = _nextActionEndTime;
                    _isCurrentActionHandled = false;
                    SetNextActionEndTime();
                    ActionCaused?.Invoke();
                }

                yield return null;
            }

            Finish();
        }

        private void SetNextActionEndTime()
        {
            _nextActionEndTime += _timeBetweenActions;
        }

        private void Finish()
        {
            var isDanceSucceed = IsDanceSucceed();

            _currentTime = 0f;
            _nextActionEndTime = 0f;
            _succeededActionCount = 0;

            DanceSucceded?.Invoke(isDanceSucceed);
        }
    }
}
