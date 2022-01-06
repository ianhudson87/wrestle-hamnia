using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState{
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{
    private CharacterAnimations enemy_anim;
    private NavMeshAgent nav_agent;
    public Transform player_target;
    public float move_speed = 3.5f;
    public float attack_distance = 1f;
    public float chase_after_attack_distance = 1f;
    public float wait_before_attack_time = 3f;
    public float attack_timer;
    private EnemyState enemy_state;

    // Start is called before the first frame update
    void Awake()
    {
        enemy_anim = GetComponent<CharacterAnimations>();
        nav_agent = GetComponent<NavMeshAgent>();
    }
    
    void Start() {
        player_target = GameObject.FindGameObjectWithTag(Tag.PLAYER_TAG).transform;

        enemy_state = EnemyState.CHASE;

        attack_timer = wait_before_attack_time;
        
    }

    // Update is called once per frame
    void Update()
    {
        // print(player_target);
        if(enemy_state == EnemyState.CHASE){
            print("Here:");
            ChasePlayer();
        }
        else if(enemy_state == EnemyState.ATTACK){
            AttackPlayer();
        }
    }

    void ChasePlayer() {
        print("here");
        nav_agent.SetDestination(player_target.position);
        nav_agent.speed = move_speed;
        
        if(nav_agent.velocity.sqrMagnitude == 0){
            enemy_anim.Walk(false);
        }
        else {
            enemy_anim.Walk(true);
        }
        print(transform.position);
        print(player_target.position);
        print("distance, " + Vector3.Distance(transform.position, player_target.position));
        if(Vector3.Distance(transform.position, player_target.position) < attack_distance){
            
            enemy_state = EnemyState.ATTACK;
        }
    }

    void AttackPlayer() {

    }
}
