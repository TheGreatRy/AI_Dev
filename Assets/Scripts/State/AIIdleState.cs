using UnityEngine;

public class AIIdleState : AIState
{

    public AIIdleState(StateAgent agent) : base(agent)
    {
        CreateTransition(nameof(AIPatrolState))
            .AddCondition(agent.timer, Condition.Predicate.LessOrEqual, 0)
            .AddCondition(agent.enemySeen, false);

        CreateTransition(nameof(AIChaseState))
            .AddCondition(agent.enemySeen, true);
    }
    public override void OnEnter()
    {
        agent.timer.value = Random.Range(1, 3);
        agent.movement.Stop();
    }
    public override void OnUpdate()
    {
    }
    public override void OnExit()
    {
        
    }
}
