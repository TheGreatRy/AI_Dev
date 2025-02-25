using UnityEngine;
using UnityEngine.Animations;

public class AIPatrolState : AIState
{
    public AIPatrolState(StateAgent agent) : base(agent)
    {
        CreateTransition(nameof(AIIdleState))
            .AddCondition(agent.destDistance, Condition.Predicate.Less, 0.5f)
            .AddCondition(agent.enemySeen, false);

        CreateTransition(nameof(AIChaseState))
            .AddCondition(agent.enemySeen, true);
    }

    public override void OnEnter()
    {
        agent.movement.Destination = NavNode.GetRandomNavNode().transform.position;
        agent.movement.Resume();
    }
   
    public override void OnUpdate()
    {
    }

    public override void OnExit()
    {
        
    }

}
