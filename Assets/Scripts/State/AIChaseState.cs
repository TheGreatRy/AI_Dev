using UnityEngine;

public class AIChaseState : AIState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AIChaseState(StateAgent agent) : base(agent)
    {
        CreateTransition(nameof(AIIdleState))
            .AddCondition(agent.destDistance, Condition.Predicate.LessOrEqual, 3.0f)
            .AddCondition(agent.enemySeen, false);
        
        CreateTransition(nameof(AIPatrolState))
            .AddCondition(agent.destDistance, Condition.Predicate.LessOrEqual, 3.0f)
            .AddCondition(agent.enemySeen, false);
    }

    public override void OnEnter()
    {
        agent.movement.data.maxSpeed *= 2;
    }
    public override void OnUpdate()
    {
        agent.movement.Destination = agent.enemy.transform.position;
    }
    public override void OnExit()
    {
        agent.movement.data.maxSpeed /= 2;
    }
}
