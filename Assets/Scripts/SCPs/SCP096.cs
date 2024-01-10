using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SCP096 : MonoBehaviour
{
    // Variables publicas
    public float wanderingSpeed, attackSpeed, maxDistanceFromPlayer, timeToPanic, timeToAttack;
    public SkinnedMeshRenderer meshRenderer;
    public Transform facePoint;
    public UnityEvent onPlayerReached;

    // Variables privadas
    private NavigationComponent navigation;
    private bool isAttacking, inPanic, isBeingWatched;
    private float currentPanic;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Inicializacion de variables
        navigation = GetComponent<NavigationComponent>();
        animator = GetComponent<Animator>();
        isAttacking = false;
        inPanic = false;
        isBeingWatched = false;
        currentPanic = 0f;
        navigation.SetAgentSpeed(wanderingSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        // Si no estamos en panico podemos deambular y verificamos deteccion de rostro y panico
        if (!inPanic)
        {
            Panic();
            IsBeenWatched();
            Wandering();
        }
    }

    // Funcion para checar si el jugador esta viendo la cara a SCP 096
    public void IsBeenWatched()
    {
        // Si el SCP esta siendo renderizado
        if (meshRenderer.isVisible)
        {
            RaycastHit _hit;
            // Se verifica si hay un objeto entre la camara y la cara de scp 096
            if (Physics.Raycast(facePoint.position, Camera.main.transform.position - facePoint.position, out _hit, 1000f))
            {
                // Si ese objeto fue el jugador entonces esta siendo observado
                if (_hit.collider.CompareTag("Player"))
                {
                    isBeingWatched = true;
                }
                // Si no entonces no esta siendo observado
                else
                {
                    isBeingWatched = false;
                }
            }
        }
        // Si no entonces no esta siendo observado
        else
        {
            isBeingWatched = false;
        }
    }

    // Funcion para atacar
    void Attack()
    {
        // Si estamos atacando
        if (isAttacking)
        {
            // Nos movemos hacia el jugador
            navigation.Move2Position(Eyes.Instance.gameObject.transform.position);

            // Si llegamos a el entonces activamos el evento
            if (Vector3.Distance(transform.position, Eyes.Instance.gameObject.transform.position) < 0.75f)
            {
                onPlayerReached.Invoke();
            }
        }
    }

    // Funcion para manejar el panico
    void Panic()
    {
        // Si esta siendo observado y aun no estamos en panico, aumentamos el panico
        if (isBeingWatched)
        {
            // Si aun no llegamos al tiempo de panico
            if(currentPanic < timeToPanic)
            {
                currentPanic += 1f * Time.deltaTime;
            }
            // Si llegamos entonces entramos en panico
            else
            {
                inPanic = true;
                animator.SetBool("inPanic", inPanic);
                navigation.SetStopMovementState(true);
                StartCoroutine(AttackCoroutine(timeToAttack));
            }
        }
        // Si no esta siendo observado lo reducimos
        else
        {
            // Si aun no llega a cero lo seguimos bajando
            if(currentPanic >= 0f)
            {
                currentPanic -= 1f * Time.deltaTime;
            }
        }
    }

    // Funcion para deambular
    void Wandering()
    {
        // si no estamos atacando ni estamos en panico
        if (!isAttacking && !inPanic)
        {
            // Si llegamos al objetivo
            if (navigation.RemainingDistance() == 0f)
            {
                // Si la distancia al player es muy lejana entonces nos acercamos a un punto random cerca del player
                if (Vector3.Distance(transform.position, Eyes.Instance.gameObject.transform.position) > maxDistanceFromPlayer)
                {
                    navigation.Move2RandomPoint(Eyes.Instance.gameObject.transform.position);
                }
                // Si no entonces vamos hacia otro punto random en nuestra cercania
                else
                {
                    navigation.Move2RandomPoint(transform.position);
                }
            }
        }
    }

    // Corrutina para el ataque
    IEnumerator AttackCoroutine(float _timeToAttack)
    {
        // Despues del tiempo de espera activamos el ataque y reactivamos la navegacion
        yield return new WaitForSeconds(_timeToAttack);
        navigation.SetStopMovementState(false);
        isAttacking = true;
        navigation.SetAgentSpeed(attackSpeed);
        animator.SetBool("isAttacking", isAttacking);
    }
}
