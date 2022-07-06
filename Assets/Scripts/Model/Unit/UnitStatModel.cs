using System;

namespace Model  {
    [Serializable]
    public struct UnitStatModel {
        public enum UnitStats {
            HP = 1,
            ATK = 2,
            MOV_SPEED = 3,
            ATK_SPD = 4
        }

        public UnitStats Stat;

        public int StatVal;

        public UnitStatModel Apply(UnitStatModel stat) {
            if (stat.Stat != Stat)
                return stat;
            
            return new UnitStatModel() {
                StatVal = stat.StatVal + StatVal,
                Stat = Stat,
            };
        }
    }
}