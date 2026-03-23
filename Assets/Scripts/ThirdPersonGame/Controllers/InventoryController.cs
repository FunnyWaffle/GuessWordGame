using Assets.Scripts.ThirdPersonGame.Core;
using Assets.Scripts.ThirdPersonGame.View.UI.Level;

namespace Assets.Scripts.ThirdPersonGame.Controllers
{
    public class InventoryController
    {
        public InventoryController(Inventory inventory, InventoryView inventoryView)
        {
            inventory.CoinsCountChanged += inventoryView.SetCoinsCount;
        }
    }
}
