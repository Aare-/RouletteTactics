using Model;
using UnityEngine;
using View;

[CreateAssetMenu(
    fileName = "Game Config",
    menuName = "RT/Game Config")]
public class GameConfig : ScriptableObject {
    public UnitFactory Factory;

    public UnitView UnitPrefab;

    public Vector2 BoardCenterPos;

    public Vector2 SpawnRadius;

    public int ArmySize;

    public float BaseAtkCooldown;

    public float BaseMovSpeed;

    public AnimationCurve SpeedCurve;

    public float MaxAttackDistance;
}
