using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    private enum State
    {
        Roaming,
        ChaseTarget,
        DeadEnemy,
    }

    [SerializeField] private PlayerController player;

    private CharacterPathfindingMovementHandler characterPathfinding;
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private State state;
    private float nextShoottime;

    private float reachedPositionDistance = 3.5f;


    private void Awake()
    {
        Pathfinding pathfinding = new Pathfinding(20, 20);
        characterPathfinding = GetComponent<CharacterPathfindingMovementHandler>();
        state = State.Roaming;
    }
    // Start is called before the first frame update
    private void Start()
    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        switch (state)
        {
            default:
                break;
            case State.Roaming:
                characterPathfinding.SetSpeed(4f);
                characterPathfinding.MoveTo(roamPosition);
                if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance)
                {
                    characterPathfinding.StopMoving();
                    roamPosition = GetRoamingPosition();
                }
                FindTarget();
                break;
            case State.ChaseTarget:
                characterPathfinding.SetSpeed(8f);
                characterPathfinding.MoveTo(player.transform.position);

                float AttackRange = 4f;
                if(Vector3.Distance(transform.position, player.transform.position) < AttackRange)
                {
                    //Attack Range, Work Latter
                    if(Time.time > nextShoottime)
                    {
                        Debug.Log("Attack Range");
                        characterPathfinding.StopMoving();
                        nextShoottime = Time.time + 0.5f;
                    }
                }
                FindTarget();
                break;
                case State.DeadEnemy:
                characterPathfinding.SetSpeed(0f);
                break;
        }
        
        
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + (GetRandomDir() * Random.Range(5f, 35f));
    }

    private static Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void FindTarget()
    {
        float targetRange = 15f;
        if(Vector3.Distance(transform.position, player.transform.position) < targetRange)
        {
            //Player in Target Range
            state = State.ChaseTarget;
        }
        else
        {
            state = State.Roaming;
        }
    }

    public void Dead()
    {
        state = State.DeadEnemy;
    }
}
