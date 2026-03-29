using TMPro;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.View.UI.Level
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinsCount;
        [SerializeField] private Transform _coinPreview;

        private void Update()
        {
            _coinPreview.rotation *= Quaternion.Euler(0, 20 * Time.deltaTime, 0);
        }

        public void SetCoinsCount(int value)
        {
            _coinsCount.SetText(value.ToString());
        }
    }
}
