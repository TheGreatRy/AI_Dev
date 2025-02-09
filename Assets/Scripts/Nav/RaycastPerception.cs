using UnityEngine;

public class RaycastPerception : Perception
{
    [SerializeField] int numRaycast = 2;
    public override GameObject[] getGameObjects()
    {
       return new GameObject[numRaycast];
    }
}
