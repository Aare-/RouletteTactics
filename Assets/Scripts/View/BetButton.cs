using System.Linq;
using Model;
using TMPro;
using UnityEngine;

namespace View  {
    public class BetButton : MonoBehaviour {

        [SerializeField] 
        protected UnitComponent _TrackProperty;

        [SerializeField] 
        protected int _TrackTeam = -1;

        [SerializeField] 
        protected int _PayoutValue;
        
        [SerializeField]
        private GameState _State;
        
        [SerializeField] 
        protected TextMeshProUGUI _Label;
        
        [SerializeField] 
        protected TextMeshProUGUI _MoneyBetted;
        
        [SerializeField] 
        protected TextMeshProUGUI _Payout;

        [SerializeField] 
        protected GameObject _PayoutMarker;
        
        public int TrackValue => _TrackValue;

        public int BettedCash {
            get => _BettedCash;
            set {
                _BettedCash = value;

                _MoneyBetted.text = _BettedCash > 0 
                    ? $"{_BettedCash}$" 
                    : "";
            }
        }

        private int _TrackValue;

        private int _BettedCash;

        protected void OnEnable() {
            _Payout.text = $"1:{_PayoutValue}";
            _MoneyBetted.text = "";

            _State.OnBetPlaced.AddListener(() => {
                ClearMarkers();
                BettedCash = BettedCash;
            });
        }

        protected void LateUpdate() {
            _TrackValue = 0;
            
            if (_TrackProperty != null) {
                int PropertyTracker(UnitView u) {
                    return u.IsAlive && u.Model.HasProperty(_TrackProperty)
                        ? 1
                        : 0;
                }
                
                _TrackValue += _State._TeamBlack.Sum(PropertyTracker);
                _TrackValue += _State._TeamWhite.Sum(PropertyTracker);
            }

            if (_TrackTeam >= 0) {
                _TrackValue = _TrackTeam == 0
                    ? _State._TeamWhite.Count
                    : _State._TeamBlack.Count;
            }
        }

        public void BetOnMe() {
            if (_State.Cash <= 0)
                return;
            if (_State.IsFighting)
                return;

            _State.Cash -= 1;
            BettedCash++;
            
            _State.OnBetPlaced?.Invoke();
        }

        public void PayOut() {
            var payout = BettedCash * (1 + _PayoutValue); 
            
            _State.Cash += payout;
            BettedCash = 0;
            
            _PayoutMarker
                .gameObject
                .SetActive(true);

            if (payout > 0) {
                _MoneyBetted.text = $"<color=green>Won: {payout}$!</color>";   
            }
        }

        public void CollectBet() {
            var lost = BettedCash;
            BettedCash = 0;

            if (lost > 0) {
                _MoneyBetted.text = $"<color=red>Lost: {lost}$</color>";
            }
        }

        public void ClearMarkers() {
            _PayoutMarker
                .gameObject
                .SetActive(false);
            BettedCash = BettedCash;
        }
    }
}