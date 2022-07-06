using UnityEngine;

namespace Model  {
    [CreateAssetMenu(
        fileName = "Unit Size",
        menuName = "RT/Unit Properties/Size")]
    public class UnitSize : UnitComponent {
        public float Size;
    }
}