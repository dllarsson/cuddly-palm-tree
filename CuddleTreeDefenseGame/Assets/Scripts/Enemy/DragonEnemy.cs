using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using StateMachine.Behaviours;

public class DragonEnemy : Enemy, ITargeter
{
    [Header("Settings")]
    [MinMaxRange(0, 25, 1), SerializeField]
    private Vector2 attackRange = new Vector2();
    [SerializeField] private float scanInterval = 2f;

    [Header("Targeting Settings")]
    [SerializeField] private float changeTargetDelay = 0.5f;
    [TagSelector, SerializeField]
    private string[] targetTags = { "Tower" };

    //ITargeter
    public List<Collider2D> AvaliableTargets { get; set; } = new List<Collider2D>();
    public GameObject TargetObject { get; set; }
    public GameObject CurrentGameObject { get; set; }

    // - State Machine - \\
    private StateController stateMachine;
    //Enum for easy swapping of behaviours
    private enum States { idle, targeting, attacking }
    private Dictionary<States, IState> dictStates = new Dictionary<States, IState>();
    private Dictionary<string, IState> dictBehaviours = new Dictionary<string, IState>();

    private void Start()
    {
        CurrentGameObject = gameObject;

        SetupBehaviours();
        ResetStates();

        Health = MaxHealth;
    }
    private void Update()
    {
        stateMachine.Update();
    }
    private void SetupBehaviours()
    {
        //Setup all behaviours that can be used for the current object

        //For testing - remove behaviour declarations that will be unused during runtime
        //Eventually make static somewhere
        dictBehaviours.Add("Idle_NoRotation", new Idle_NoRotation(this, targetTags, attackRange, scanInterval));
        //Debug
        dictBehaviours.Add("Turn_Red", new Turn_Red(this));

        //Set initial behaviours for each state for easy swapping
        dictStates.Add(States.idle, dictBehaviours["Idle_ScanForTargetsSporatic"]);
        //dictStates.Add(States.targeting, dictBehaviours["Target_Group"]);
        //dictStates.Add(States.attacking, dictBehaviours["Attack_Default"]);
    }
    private void ResetStates()
    {
        //Initiate state machine
        stateMachine = new StateController();

        //Set transitions
        //stateMachine.AddTransition(dictStates[States.idle], dictStates[States.targeting], () => AvaliableTargets.Any());

        //stateMachine.AddTransition(dictStates[States.targeting], dictStates[States.idle], () => !AvaliableTargets.Any());

        //Set substates that do not end current state
        //stateMachine.Substate(dictStates[States.attacking], () => TargetObject != null);
        //Debug change color to red
        //stateMachine.Substate(dictBehaviours["Turn_Red"], () => TargetObject != null);

        //Initial state
        stateMachine.SetState(dictStates[States.idle]);
    }
}