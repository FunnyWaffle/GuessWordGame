using Assets.Scripts.ThirdPersonGame.Data;
using System.Threading.Tasks;

namespace Assets.Scripts.ThirdPersonGame.UI
{
    public class UICreator
    {
        public UICreator()
        {
            _ = Create();
        }
        private async Task Create()
        {
            var data = await Storage.Load(new UIData(), Storage.UIDataPath);
        }
    }
}
