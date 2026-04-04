using Assets.Scripts.ThirdPersonGame.Core;
using Assets.Scripts.ThirdPersonGame.View.UI.DanceMinigameHud;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.ThirdPersonGame.Controllers.DanceMinigame
{
    public class DanceActionZoneController
    {
        private readonly Player _player;
        private readonly List<DanceActionZoneUI> _danceActionZoneUI = new();

        public DanceActionZoneController(Player player, params DanceActionZoneUI[] danceActionZoneUI)
        {
            _player = player;
            _danceActionZoneUI = danceActionZoneUI.ToList();

            _player.DanceActionPerformed += HandleActionEnd;
            _player.ActionCaused += HandleDanceActionCall;
        }

        private void HandleActionEnd(int actionZoneIndex, bool isSuccess)
        {
            if (isSuccess)
                _danceActionZoneUI[actionZoneIndex].StartRingWinAnimation();
            else
                _danceActionZoneUI[actionZoneIndex].StartRingLoseAnimation();

        }

        private void HandleDanceActionCall(int zoneIndex)
        {
            _danceActionZoneUI[zoneIndex].StartRingAnimation(_player.Dance.ActionDuration);
        }
    }
}
