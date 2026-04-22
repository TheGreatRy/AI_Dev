using UnityEngine;

public class AIHitState : AIState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AIHitState(StateAgent agent) : base(agent)
    {
        CreateTransition(nameof(AIDeathState))
            .AddCondition(agent.health, Condition.Predicate.LessOrEqual, 0f);

        CreateTransition(nameof(AIPatrolState))
            .AddCondition(agent.destDistance, Condition.Predicate.GreaterOrEqual, 2.5f);

    }

    public override void OnEnter()
    {
        agent.movement.Stop();
    }
    public override void OnUpdate()
    {
        
    }
    public override void OnExit()
    {
        agent.agentHit.value = false;
        agent.movement.Resume();
    }

}
