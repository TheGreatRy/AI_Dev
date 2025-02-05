using UnityEngine;

public class NavAgent : AIAgent
{
    public Waypoint waypoint {  get; set; }

    private void Start()
    {
        var objects = GameObject.FindObjectsByType<Waypoint>(FindObjectsSortMode.None);
        if (objects.Length > 0)
        {
            waypoint = objects[Random.Range(0, objects.Length)];
        }
    }
    private void Update()
    {
        if (waypoint != null)
        {
            movement.MoveTowards(waypoint.transform.position);
        }

        transform.forward = movement.Direction;

    }
}
