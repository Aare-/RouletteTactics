using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using View;

[CreateAssetMenu(
    fileName = "Game State",
    menuName = "RT/Game State")]
public class GameState : ScriptableObject  {
    
    public List<UnitView> _TeamBlack = new List<UnitView>();
    
    public List<UnitView> _TeamWhite = new List<UnitView>();

    public int Cash;

    public bool IsFighting;

    public UnityEvent OnBetPlaced;
    
    public void ClearUnits() {
        _TeamBlack.Clear();
        _TeamWhite.Clear();
    }
    
    public void Register(UnitView unit) {
        var list = GetAllies(unit);
        
        if (!list.Contains(unit))
            list.Add(unit);
    }

    public void Unregister(UnitView unit) {
        var list = GetAllies(unit);
        
        if (list.Contains(unit))
            list.Remove(unit);
    }

    public List<UnitView> GetAllies(UnitView unit) {
        return GetTeam(unit.Model.Team);
    } 
    
    public List<UnitView> GetOpponents(UnitView unit) {
        return GetTeam(unit.Model.Team == 1 ? 0 : 1);
    }

    private List<UnitView> GetTeam(int teamId) {
        return teamId == 0
            ? _TeamWhite
            : _TeamBlack;
    }
}
