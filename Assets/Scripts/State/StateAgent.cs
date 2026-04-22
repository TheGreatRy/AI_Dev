using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class StateAgent : AIAgent
{
    [SerializeField] public Perception perception;
    
    public StateMachine stateMachine = new StateMachine();
    public ValueRef<float> timer = new ValueRef<float>();
    public ValueRef<float> health = new ValueRef<float>();
    public ValueRef<float> destDistance = new ValueRef<float>();
    public ValueRef<float> enemyDistance = new ValueRef<float>();
    public ValueRef<bool> enemySeen = new ValueRef<bool>();
    public ValueRef<bool> agentHit = new ValueRef<bool>();

    Animator animator;
    public StateAgent enemy;
    public string enemyTag;
    private void Start()
    {
        enemy = GetComponent<StateAgent>();
        
        animator = GetComponentInChildren<Animator>();
        stateMachine.AddState(nameof(AIIdleState), new AIIdleState(this));  
        stateMachine.AddState(nameof(AIPatrolState), new AIPatrolState(this));
        stateMachine.AddState(nameof(AIChaseState), new AIChaseState(this));
        stateMachine.AddState(nameof(AIHitState), new AIHitState(this));
        stateMachine.AddState(nameof(AIDeathState), new AIDeathState(this));
        stateMachine.AddState(nameof(AIAttackState), new AIAttackState(this));


        stateMachine.SetState(nameof(AIIdleState));
        animator.SetBool("Walking", false);
        animator.SetBool("Running", false);

    }

    private void Update()
    {

        transform.rotation = Quaternion.LookRotation(movement.Direction, Vector3.up);
        
        timer.value -= Time.deltaTime;
        if (perception != null)
        {
            var gameObjects = perception.GetGameObjects();
            enemySeen.value = gameObjects.Length > 0;

            if (gameObjects.Length > 0)
            {
                gameObjects[0].TryGetComponent<StateAgent>(out enemy); 

                enemyDistance.value = transform.position.DistanceXZ(gameObjects[0].transform.position);
            }   
        }
        
        destDistance.value = transform.position.DistanceXZ(movement.Destination);

        stateMachine.CurrentState?.CheckTransitions();

        stateMachine.Update();

        if (stateMachine.CurrentState.GetType() == typeof(AIIdleState))
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
        }
        else if (stateMachine.CurrentState.GetType() == typeof(AIPatrolState))
        {
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
        }
        else if (stateMachine.CurrentState.GetType() == typeof(AIChaseState))
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
        }
        else if (stateMachine.CurrentState.GetType() == typeof(AIAttackState))
        {
            if (timer.value <= 0)
            {
                Attack();
                timer.value = 5;
            }
        }
    }
    private void OnGUI()
    {
        // draw label of current state above agent
        GUI.backgroundColor = Color.black;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        Rect rect = new Rect(0, 0, 100, 20);
        // get point above agent
        Vector3 point = Camera.main.WorldToScreenPoint(transform.position);
        rect.x = point.x - (rect.width / 2);
        rect.y = Screen.height - point.y - rect.height - 20;
        // draw label with current state name
        GUI.Label(rect, stateMachine.CurrentState.Name);
    }
    public void OnDamage( float damage)
    {
        agentHit.value = true;
        health.value -= damage;

        print("Damage Taken!");

        if (health > 0) stateMachine.SetState(nameof(AIHitState));
        else stateMachine.SetState(nameof(AIDeathState));
    }
    private void Attack()
    {
        // check for collision with surroundings
        var colliders = Physics.OverlapSphere(transform.position, 20);
        foreach (var collider in colliders)
        {
            // enable collision only with enemy
            if (collider.gameObject.CompareTag(enemyTag)) continue;

            // check if collider object is a state agent, damage agent
            if (collider.gameObject.TryGetComponent<StateAgent>(out var agent))
            {
                
                agent.OnDamage(Random.Range(20, 50));
            }
        }
    }
}
