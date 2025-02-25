using UnityEngine;

[CreateAssetMenu(fileName ="AutonomousAgentData", menuName ="Data/AutonomousAgent")]
public class AutonomousAgentData : ScriptableObject {
    [Range(0, 20)] public float displacement;
    [Range(0, 180)]public float angle;
    [Range(0, 25)]public float radius;

    [Range(0, 50)]public float cohesionWeight;
    [Range(0, 50)]public float separationWeight;
    [Range(0, 25)]public float separationRadius;
    [Range(0, 50)]public float alignmentWeight;
    [Range(0, 20)] public float obstacleWeight;
}
