using System;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour  {

    [SerializeField] 
    protected GameConfig _Config;
    
    [SerializeField] 
    protected GameState _State;

    [SerializeField] 
    protected Transform _BattlefieldContainer;
    
    [SerializeField] 
    protected Transform _NegGameButton;

    public UnityEvent OnFightStarted;
    
    public UnityEvent OnFightFinished;
    
    protected void Awake() {
        OnFightStarted.AddListener(() => {
            _State.IsFighting = true;
            GenerateArmies();
        });
        OnFightFinished.AddListener(() => {
            _State.IsFighting = false;
        });

        NewGame();
    }

    public void NewGame() {
        _State.Cash = 10;
        OnFightFinished?.Invoke();
    }

    public void StartFight() {
        if (_State.IsFighting)
            return;
        
        OnFightStarted?.Invoke();
    }

    private void EndFight() {
        if (!_State.IsFighting)
            return;
        
        OnFightFinished?.Invoke();
    }

    protected void LateUpdate() {
        if (_State.IsFighting) {
            if (_State._TeamBlack.Count == 0 || _State._TeamWhite.Count == 0) {
                EndFight();
            }
        }
    }

    public void EvaluateNewGame() {
        _NegGameButton.gameObject.SetActive(_State.Cash == 0);
    }

    private void GenerateArmies() {
        _BattlefieldContainer
            .gameObject
            .RemoveAllChildren();
        
        _State.ClearUnits();
        
        for (var i = 0; i < 2; i++) {
            for (var j = 0; j < _Config.ArmySize; j++) {
                var radius = Random.Range(_Config.SpawnRadius.x, _Config.SpawnRadius.y);
                
                var angle = Random.Range(
                    Mathf.PI * i + Mathf.PI * 0.5f, 
                    Mathf.PI * i + Mathf.PI * 1.5f);
                var pos = 
                    _Config.BoardCenterPos + 
                    new Vector2(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle));
                
                GenerateUnit(i, pos);
            }
        }
    }

    private void GenerateUnit(int team, Vector2 position) {
        var unitInstance = Instantiate(_Config.UnitPrefab, _BattlefieldContainer, false);
        
        unitInstance.Init(_Config.Factory.Build(team));
        unitInstance.RTransform.anchoredPosition = position;
    }
}
