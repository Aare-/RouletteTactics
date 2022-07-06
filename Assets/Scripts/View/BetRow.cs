using System.Linq;
using UnityEngine;

namespace View  {
    public class BetRow : MonoBehaviour {
        public void Evaluate() {
            var bets = GetComponentsInChildren<BetButton>()
                .ToList();

            bets.Sort((a, b) => b.TrackValue - a.TrackValue);

            if (bets.Count > 1 && bets[0].TrackValue > 0 && bets[0].TrackValue > bets[1].TrackValue) {
                bets[0].PayOut();
            }

            for (var i = 1; i < bets.Count; i++) {
                bets[i].CollectBet();
            }
        }

        public void ClearMarkers() {
            GetComponentsInChildren<BetButton>()
                .ToList()
                .ForEach(b => b.ClearMarkers());
        }
    }
}