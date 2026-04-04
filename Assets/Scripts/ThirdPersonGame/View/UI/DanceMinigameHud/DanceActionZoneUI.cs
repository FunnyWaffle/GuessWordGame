using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ThirdPersonGame.View.UI.DanceMinigameHud
{
    public class DanceActionZoneUI : MonoBehaviour
    {
        [SerializeField] private Vector3 _startSize;
        [SerializeField] private Vector3 _endSize;
        [SerializeField] private Image _ring;

        private Tween _tween;

        private void Start()
        {
            ResetState();
        }

        public void StartRingAnimation(float animationDuration)
        {
            _ring.gameObject.SetActive(true);
            _ring.transform.localScale = _startSize;
            _ring.color = Color.white;

            _tween?.Kill();
            _tween = _ring.transform.DOScale(_endSize, animationDuration)
                .OnComplete(ResetState);
        }

        public void StartRingLoseAnimation()
        {
            if (_tween == null)
                return;

            _tween.Kill();
            _tween = _ring.DOColor(Color.red, 0.3f)
                .OnComplete(ResetState);
        }

        public void StartRingWinAnimation()
        {
            if (_tween == null)
                return;

            _tween.Kill();
            _tween = _ring.DOColor(Color.green, 0.3f)
                .OnComplete(ResetState);
        }

        private void ResetState()
        {
            _ring.gameObject.SetActive(false);
            _ring.transform.localScale = _startSize;
            _ring.color = Color.white;
        }
    }
}
