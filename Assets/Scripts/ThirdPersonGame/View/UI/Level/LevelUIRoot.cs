using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.View.UI.Level
{
    public class LevelUIRoot : MonoBehaviour
    {
        [SerializeField] private InventoryView _inventoryView;

        public InventoryView InventoryView => _inventoryView;
    }
}
