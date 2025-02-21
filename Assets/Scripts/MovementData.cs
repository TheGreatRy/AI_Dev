using UnityEngine;

[CreateAssetMenu(fileName ="Movement", menuName ="Data/MovementData")]
public class MovementData : ScriptableObject {
    [SerializeField] public float maxSpeed = 5;
    [SerializeField] public float minSpeed = 5;
    [SerializeField] public float maxForce = 5;
    [SerializeField] public float turnRate = 5;

}
