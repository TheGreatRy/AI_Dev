using UnityEngine;

public class AIAttackState : AIState
{
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AIAttackState(StateAgent agent) : base(agent)
    {
        CreateTransition(nameof(AIIdleState))
            .AddCondition(agent.destDistance, Condition.Predicate.GreaterOrEqual, 2.5f);

        CreateTransition(nameof(AIHitState))
            .AddCondition(agent.agentHit, true);
    }

    public override void OnEnter()
    {
        agent.movement.Stop();
    }
    public override void OnUpdate()
    {
        agent.movement.Destination = agent.enemy.transform.position;
    }
    public override void OnExit()
    {
        agent.movement.Resume();
    }

}
