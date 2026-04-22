using UnityEngine;

public class AIDeathState : AIState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AIDeathState(StateAgent agent) : base(agent)
    {
        
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
    }


}
