using UnityEngine;

public class AutonomousAgent : AIAgent
{
    [Header("Wanderer")]
    public AutonomousAgentData data;
    public Perception seekPerception;
    public Perception fleePerception;
    public Perception flockPerception;
    public Perception obstaclePerception;
    
    private void Update()
    {
        //movement.ApplyForce(Vector3.forward * 10);
        transform.position = Utils.Wrap(transform.position, new Vector3( -10, -10, -10),new Vector3( 10, 10, 10));

        //Debug.DrawRay(transform.position, transform.forward * perception.maxDistance, Color.cyan);
        if (seekPerception != null) {
            var gameObjects = seekPerception.getGameObjects();
            if (gameObjects.Length > 0)
            {
                Vector3 force = Seek(gameObjects[0]);
                movement.ApplyForce(force);
            }
        }
        if (fleePerception != null){
            var gameObjects = fleePerception.getGameObjects();
            if (gameObjects.Length > 0)
            {
                Vector3 force = Flee(gameObjects[0]);
                movement.ApplyForce(force);
            }
        }
        if (flockPerception != null)
        {
            var gameObjects = flockPerception.getGameObjects();
            if (gameObjects.Length > 0)
            {
                movement.ApplyForce(Cohesion(gameObjects) * data.cohesionWeight);
                movement.ApplyForce(Separation(gameObjects, data.separationRadius) * data.separationWeight);
                movement.ApplyForce(Alignment(gameObjects) * data.alignmentWeight);
                
            }
        }
        if (obstaclePerception != null)
        {
            if(obstaclePerception.CheckDirection(Vector3.forward))
            {
                Debug.DrawRay(transform.position, transform.rotation * Vector3.forward, Color.red, 0.5f);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.rotation * Vector3.forward, Color.yellow, 0.5f);

            }
        }

        if (movement.Acceleration.sqrMagnitude == 0)
        {
            Vector3 force = Wander();
            movement.ApplyForce(force);
        }

        Vector3 acceleration = movement.Acceleration;
        acceleration.y = 0;
        movement.Acceleration = acceleration;
        if(movement.Direction.sqrMagnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(movement.Direction);

        }

        //foreach (var go in gameObjects)
        //{
        //    Debug.DrawLine(transform.position, go.transform.position, Color.red);
        //}

        float size = 25;
        transform.position = Utils.Wrap(transform.position, new Vector3(-size, -size, -size), new Vector3(size, size, size));
    }
    private Vector3 Seek(GameObject go)
    {
        Vector3 direction = go.transform.position - transform.position;
        Vector3 force = GetSteeringForce(direction);

        return force;
    }
    private Vector3 Flee(GameObject go)
    {
        Vector3 direction = transform.position - go.transform.position;
        Vector3 force = GetSteeringForce(direction);

        return force;
    }

    private Vector3 Cohesion(GameObject[] gameObjects)
    {
        Vector3 positions = Vector3.zero;
        foreach (var gameObject in gameObjects)
        {
            positions += gameObject.transform.position;
        }
        Vector3 center = positions / gameObjects.Length;
        Vector3 direction = center - transform.position;

        Vector3 force = GetSteeringForce(direction);
        return force;
    }
    private Vector3 Separation(GameObject[] gameObjects, float redius)
    {
        Vector3 separation = Vector3.zero; ;
        foreach (var gameObject in gameObjects)
        {
            Vector3 direction = transform.position - gameObject.transform.position;
            Vector3 distance = direction / movement.Acceleration.sqrMagnitude;
        }
        return GetSteeringForce(separation);
    }
    private Vector3 Alignment(GameObject[] gameObjects)
    {
        return Vector3.zero;
    }
    private Vector3 Wander()
    {
        data.angle += Random.Range(-data.displacement, data.displacement);
        Quaternion rotation = Quaternion.AngleAxis(data.angle, Vector3.up);
        Vector3 point = rotation * (Vector3.forward * data.radius);
        Vector3 forward = transform.forward + movement.Direction;
        Vector3 force = GetSteeringForce(forward + point);
        return force;
    }

    private Vector3 GetSteeringForce(Vector3 direction)
    {
        Vector3 desired = direction.normalized * movement.data.maxSpeed;
        Vector3 steer = desired - movement.Velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, movement.data.maxForce);

        return force;
    }
}
