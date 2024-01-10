using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(NavigationComponent))]

public class SCP173 : MonoBehaviour
{

    public float wanderingSpeed, attackSpeed, detectionDistance, maxDistanceFromPlay;
    public MeshRenderer meshRenderer;
    public UnityEvent onPlayerReached;

    private NavigationComponent navigation;
    private bool isWandering, isAttacking;
    private float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        navigation = GetComponent<NavigationComponent>();
        isWandering = true;
        isAttacking = false;
        currentSpeed = wanderingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDetection();
        Attack();
        Wandering();
        IsBeingWatched();
    }

    public void IsBeingWatched()
    {
        if (!meshRenderer.isVisible)
        {
            navigation.SetAgentSpeed(currentSpeed);
        }
        else
        {
            if (Eyes.Instance.IsBlinking())
            {
                navigation.SetAgentSpeed(currentSpeed);

            }
            else
            {
                navigation.SetAgentSpeed(0f);
            }
        }
    }

    public void PlayerDetection()
    {
        if (Vector3.Distance(transform.position, Eyes.Instance.gameObject.transform.position) <= detectionDistance)
        {
            isAttacking = true;
            isWandering = false;
            currentSpeed = attackSpeed;
        }
        else
        {
            isAttacking = false;
            isWandering = true;
            currentSpeed = wanderingSpeed;
        }
    }

    public void Attack()
    {
        if(isAttacking && !isWandering)
        {
            navigation.Move2Position(Eyes.Instance.gameObject.transform.position);

            if(Vector3.Distance(transform.position, Eyes.Instance.gameObject.transform.position) < 1.3f)
            {
                onPlayerReached.Invoke();
            }
        }
    }

    public void Wandering()
    {
        if(isWandering && !isAttacking)
        {
            if(navigation.RemainingDistance() == 0f)
            {
                if(Vector3.Distance(transform.position, Eyes.Instance.gameObject.transform.position) > maxDistanceFromPlay)
                {
                    navigation.Move2RandomPoint(Eyes.Instance.gameObject.transform.position);
                }
                else
                {
                    navigation.Move2RandomPoint(transform.position);
                }
            }
        }
    }
}
