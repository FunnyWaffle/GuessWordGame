using Assets.Scripts.ThirdPersonGame.Core;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.View.UI.Level
{
    public class ChangeStateView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _defailtStateText;
        [SerializeField] private TMP_Text _danceStateText;

        public void SetState(CharacterBehaviourState state)
        {
            if (state == CharacterBehaviourState.Default)
            {
                _defailtStateText.gameObject.SetActive(true);
                _defailtStateText.DOFade(0, 5f);

                _danceStateText.gameObject.SetActive(false);
            }
            else
            {
                _danceStateText.gameObject.SetActive(true);
                _danceStateText.DOFade(0, 5f);

                _defailtStateText.gameObject.SetActive(false);
            }
        }
    }
}
