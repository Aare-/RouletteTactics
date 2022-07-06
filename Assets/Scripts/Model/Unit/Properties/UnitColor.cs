using UnityEngine;

namespace Model  {
    [CreateAssetMenu(
        fileName = "Unit Color",
        menuName = "RT/Unit Properties/Color")]
    public class UnitColor : UnitComponent {
        public Color Color;
    }
}