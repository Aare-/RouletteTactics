using System.Linq;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace View  {
    public class UnitView : MonoBehaviour {
        public RectTransform RTransform => _RTransf;

        [SerializeField] 
        protected GameConfig _GConfig;
        
        [SerializeField] 
        protected GameState _State;

        [SerializeField] 
        protected CanvasGroup _Canvas;
        
        [SerializeField] 
        protected RectTransform _RTransf;
        
        [SerializeField] 
        protected Rigidbody2D _Body;
        
        [SerializeField] 
        protected Image _Image;
        
        [SerializeField] 
        protected Image _Border;

        private int _StartingHp;

        private int _DamageTaken;

        private float _AtkCooldown;
        
        public UnitModel _UnitModel;
        
        public UnitModel Model => _UnitModel;
        
        public bool IsInitialised => _StartingHp > 0;
        
        public bool IsAlive => IsInitialised && Health > 0;

        public int Health => _StartingHp - _DamageTaken;
        
        private bool CanAttack => _AtkCooldown >= _GConfig.BaseAtkCooldown;

        private float MovVelocity =>
            _GConfig.SpeedCurve.Evaluate(Model.GetStatVal(UnitStatModel.UnitStats.MOV_SPEED)) *
            _GConfig.BaseMovSpeed;
        
        public void Init(UnitModel model) {
            _UnitModel = model;
            _StartingHp = Model.GetStatVal(UnitStatModel.UnitStats.HP);

            if (_StartingHp <= 0) {
                Destroy(gameObject);
                return;
            }

            _RTransf.localScale = Vector3.one * Model.Size;
            
            _Image.sprite = Model.Shape;
            _Image.color = Model.Color;

            _Border.sprite = Model.Shape;
            _Border.color = model.Team == 0
                ? Color.white
                : Color.black;
            
            _State.Register(this);
        }

        public void TakeDamage(int damage) {
            _DamageTaken += damage;
        }
        
        protected void Update() {
            if (!_State.IsFighting)
                return;
            if (!IsInitialised)
                return;
            
            _AtkCooldown += Time.deltaTime * Model.GetStatVal(UnitStatModel.UnitStats.ATK_SPD);
            
            #region Movement
            var movTarget = FindMovementTarget();
            var newMovVelocity = Vector2.zero;
            
            if (movTarget != null) {
                var targetVector = (movTarget.RTransform.anchoredPosition - RTransform.anchoredPosition);
                var targetDistance = targetVector.magnitude;
                var movDir = targetVector.normalized;

                if (targetDistance > _GConfig.MaxAttackDistance) {
                    newMovVelocity = movDir * MovVelocity;   
                }
            }

            _Body.velocity = newMovVelocity;
            #endregion
            
            #region Attack

            if (CanAttack) {
                var attackTarget = FindAttackTarget();

                if (attackTarget != null) {
                    PerformAttack(attackTarget);
                }
            }

            #endregion
            
            _Canvas.alpha = Mathf.Lerp(0.3f, 1.0f, (float)(Health) / _StartingHp);
        }

        protected void LateUpdate() {
            if (!_State.IsFighting)
                return;
            if (!IsInitialised)
                return;
            
            if (!IsAlive) {
                Destroy(gameObject);
            }
        }

        private void OnDestroy() {
            _State.Unregister(this);
        }

        protected UnitView FindMovementTarget() {
            switch (_UnitModel.Behaviour) {
                case UnitShape.AIBehaviour.SEEK_CLOSEST_ENEMY:
                    return FindClosestOpponent();
                
                case UnitShape.AIBehaviour.SEEK_WEAKEST_ENEMY:
                    return FindWeakestOpponent();
                
                default:
                    return null;
            }
        }

        protected UnitView FindAttackTarget() {
            var target = FindClosestOpponent();

            if (target == null)
                return null;

            var distanceToTarget = (target.RTransform.anchoredPosition - RTransform.anchoredPosition).magnitude;
            
            return distanceToTarget <= _GConfig.MaxAttackDistance
                ? target
                : null;
        }

        private UnitView FindClosestOpponent() {
            var list = _State.GetOpponents(this);
            
            // It's ok to modify the original list since we don't care about it's order
            list.Sort(PickClosest);
            
            return list.FirstOrDefault();
        }
        
        private UnitView FindWeakestOpponent() {
            var list = _State.GetOpponents(this);
            
            // It's ok to modify the original list since we don't care about it's order
            list.Sort((a, b) =>
            {
                var healthDiff = a.Health - b.Health;

                if (healthDiff != 0)
                    return healthDiff;

                return PickClosest(a, b);
            });
            
            return list.FirstOrDefault();
        }

        private int PickClosest(UnitView a, UnitView b) {
            var aDistance = (a.RTransform.anchoredPosition - RTransform.anchoredPosition).sqrMagnitude;
            var bDistance = (b.RTransform.anchoredPosition - RTransform.anchoredPosition).sqrMagnitude;
            return (int)(aDistance - bDistance);
        }
        
        protected void PerformAttack(UnitView opponent) {
            opponent.TakeDamage(_UnitModel.GetStatVal(UnitStatModel.UnitStats.ATK));
            _AtkCooldown = 0.0f;
        }
    }
}