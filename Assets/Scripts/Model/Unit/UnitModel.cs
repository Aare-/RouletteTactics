using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model {
    [Serializable]
    public struct UnitModel {

        private readonly int _Team;
        
        private readonly UnitColor _Color;

        private readonly UnitShape _Shape;

        private readonly UnitSize _Size;
        
        private readonly Dictionary<UnitStatModel.UnitStats, int> _Stats;

        public UnitModel(
                int team,
                Dictionary<UnitStatModel.UnitStats, int> stats,
                UnitColor color,
                UnitShape shape,
                UnitSize size) {
            _Team = team;
            _Stats = stats;
            _Color = color;
            _Shape = shape;
            _Size = size;
        }

        public int Team => _Team;
        
        public Color Color => _Color?.Color ?? Color.magenta;

        public Sprite Shape => _Shape?.Shape ?? null;

        public float Size => _Size?.Size ?? 10.0f;

        public UnitShape.AIBehaviour Behaviour => _Shape?.Behaviour ?? UnitShape.AIBehaviour.NONE;
        
        public int GetStatVal(UnitStatModel.UnitStats stat) {
            return _Stats.ContainsKey(stat) 
                ? _Stats[stat] 
                : 0;
        }

        public bool HasProperty(UnitComponent property) {
            return _Color == property ||
                   _Shape == property ||
                   _Size == property;
        }
    }
}