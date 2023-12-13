using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]

public class NavigationComponent : MonoBehaviour
{
    // Public var
    public float maxWalkDistance;

    // PRivate var
    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    public void Move2Position(Vector3 _position)
    {
        NavMeshPath navMeshPath = new NavMeshPath();

        if(navMeshAgent.CalculatePath(_position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            navMeshAgent.SetPath(navMeshPath);
        }
    }


    public void Move2RandomPoint(Vector3 _center)
    {
        Vector3 _direction = (Random.insideUnitSphere * maxWalkDistance) + _center;
        _direction.y = transform.position.y;

        NavMeshPath navMeshPath = new NavMeshPath();

        if (navMeshAgent.CalculatePath(_direction, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            navMeshAgent.SetPath(navMeshPath);
        }
        else
        {
            Debug.Log("Can't reach");
        }
    }

    public void SetStopMovementState(bool _state)
    {
        navMeshAgent.isStopped = _state;
    }

    public void SetAgentSpeed(float _speed)
    {
        navMeshAgent.speed = _speed;
    }

    public float RemainingDistance()
    {
        return navMeshAgent.remainingDistance;
    }

}
