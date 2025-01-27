using UnityEngine;

[CreateAssetMenu(fileName ="AutonomousAgentData", menuName ="Data/AutonomousAgent")]
public class AutonomousAgentData : ScriptableObject {
    [Range(0, 5)] public float displacement;
    [Range(0, 5)]public float angle;
    [Range(0, 5)]public float radius;

    [Range(0, 5)]public float cohesionWeight;
    [Range(0, 5)]public float separationWeight;
    [Range(0, 5)]public float separationRadius;
    [Range(0, 5)]public float alignmentWeight;
}
