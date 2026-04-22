using UnityEngine;

public class AIChaseState : AIState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AIChaseState(StateAgent agent) : base(agent)
    {
        CreateTransition(nameof(AIPatrolState))
            .AddCondition(agent.destDistance, Condition.Predicate.GreaterOrEqual, 2.5f);

        CreateTransition(nameof(AIAttackState))
            .AddCondition(agent.destDistance, Condition.Predicate.LessOrEqual, 1.5f)
            .AddCondition(agent.enemySeen, true);
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
