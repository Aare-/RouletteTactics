using UnityEngine;

namespace Model  {
    [CreateAssetMenu(
        fileName = "Unit Shape",
        menuName = "RT/Unit Properties/Shape")]
    public class UnitShape : UnitComponent {
        public enum AIBehaviour {
            NONE,
            SEEK_CLOSEST_ENEMY,
            SEEK_WEAKEST_ENEMY
        }
        
        public Sprite Shape;

        public AIBehaviour Behaviour;
    }
}