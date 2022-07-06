using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model  {
    [CreateAssetMenu(
        fileName = "Unit Factory",
        menuName = "RT/Units Factory")]
    public class UnitFactory : UnitComponent {
        
        [SerializeField]
        protected List<UnitColor> _Colors = new List<UnitColor>();

        [SerializeField]
        protected List<UnitSize> _Sizes = new List<UnitSize>();

        [SerializeField]
        protected List<UnitShape> _Shapes = new List<UnitShape>();
        
        public UnitModel Build(int team) {
            var statDict = new Dictionary<UnitStatModel.UnitStats, int>();
            var color = _Colors.GetRandom();
            var shape = _Shapes.GetRandom();
            var size = _Sizes.GetRandom();

            int GetStatValue(UnitStatModel.UnitStats stat) {
                var statModel = new UnitStatModel() {
                    Stat = stat,
                    StatVal = 0
                };

                statModel = Apply(statModel);
                statModel = color?.Apply(statModel) ?? statModel;
                statModel = shape?.Apply(statModel) ?? statModel;
                statModel = size?.Apply(statModel) ?? statModel;

                return statModel.StatVal;
            }

            foreach (var stat in (UnitStatModel.UnitStats[]) Enum.GetValues(typeof(UnitStatModel.UnitStats))) {
                statDict[stat] = GetStatValue(stat);
            }

            return new UnitModel(team, statDict, color, shape, size);
        }
    }
}