using System.Collections.Generic;
using UnityEngine;

namespace Model  {
    public abstract class UnitComponent : ScriptableObject {
        
        [SerializeField]
        protected List<UnitStatModel> StatModifs = new List<UnitStatModel>();

        public UnitStatModel Apply(UnitStatModel stat) {
            for (var i = 0; i < StatModifs.Count; i++)
                stat = StatModifs[i].Apply(stat);
            
            return stat;
        }
    }
}