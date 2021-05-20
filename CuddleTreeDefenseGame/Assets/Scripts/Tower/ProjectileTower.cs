using System.Collections.Generic;
using System.Linq;
using StateMachine;
using StateMachine.Behaviours;
using UnityEngine;

public class ProjectileTower : Tower, ITargeter, IRangeIndicator
{
    [Header("Prefabs")]
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject muzzleFlashPrefab;

    [Header("Settings")]
    [MinMaxRange(0, 25, 1), SerializeField]
    private Vector2 attackRange = new Vector2();
    [SerializeField] private float scanInterval = 2f;

    [Header("Rotation Settings")]
    [SerializeField] private float searchRotationSpeed = 10f;
    private enum RotationDirection { left, right }
    [SerializeField] private RotationDirection rotationDirection;

    [Header("Targeting Settings")]
    //dont need
    [SerializeField] private float changeTargetDelay = 0.5f;
    [TagSelector, SerializeField]
    private string[] targetTags = { "Enemy" };

    [Header("Attack Settings")]
    [SerializeField] private Transform fireOrigin;
    [SerializeField] private float maxTurnSpeed = 80f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float projectileSpeed = 10f;

    //ITargeter
    public List<Collider2D> AvaliableTargets { get; set; } = new List<Collider2D>();
    public GameObject TargetObject { get; set; }
    public GameObject CurrentGameObject { get; set; }

    //IRangeIndicator
    public float RangeIndicatorSize => attackRange.y;


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

        //- Debug example - \\
        //Can change behaviour on-the-fly
        //Reset is required if behaviour is changed
        EventHandler.current.OnKeyPress += (key, _) =>
        {
            if(key == KeyCode.K)
            {
                dictStates[States.targeting] = dictBehaviours["Target_HighestHealth"];
                Debug.Log("Changed targeting state to Target_HighestHealth");
                ResetStates();
            }
            if(key == KeyCode.L)
            {
                dictStates[States.targeting] = dictBehaviours["Target_LowestHealth"];
                Debug.Log("Changed targeting state to Target_LowestHealth");
                ResetStates();
            }
        };
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
        dictBehaviours.Add("Idle_ScanForTargetsSporatic", new Idle_ScanForTargetsSporatic(this, turret, (int)rotationDirection, searchRotationSpeed, targetTags, attackRange, scanInterval));
        dictBehaviours.Add("Idle_NoRotation", new Idle_NoRotation(this, targetTags, attackRange, scanInterval));
        dictBehaviours.Add("Idle_ScanForTargetsRotation", new Idle_ScanForTargetsRotation(this, turret, (int)rotationDirection, searchRotationSpeed, targetTags, attackRange, scanInterval));
        dictBehaviours.Add("Target_Default", new Target_Default(this, attackRange, changeTargetDelay));
        dictBehaviours.Add("Target_HighestHealth", new Target_HighestHealth(this, attackRange, changeTargetDelay));
        dictBehaviours.Add("Target_LowestHealth", new Target_LowestHealth(this, attackRange, changeTargetDelay));
        dictBehaviours.Add("Target_Group", new Target_Group(this, attackRange, changeTargetDelay));
        dictBehaviours.Add("Attack_Default", new Attack_Default(this, turret, projectilePrefab, muzzleFlashPrefab, fireOrigin, maxTurnSpeed, fireRate, projectileSpeed, targetTags));
        //Debug
        dictBehaviours.Add("Turn_Red", new Turn_Red(this));

        //Set initial behaviours for each state for easy swapping
        dictStates.Add(States.idle, dictBehaviours["Idle_ScanForTargetsSporatic"]);
        dictStates.Add(States.targeting, dictBehaviours["Target_Group"]);
        dictStates.Add(States.attacking, dictBehaviours["Attack_Default"]);
    }
    private void ResetStates()
    {
        //Initiate state machine
        stateMachine = new StateController();

        //Set transitions
        stateMachine.AddTransition(dictStates[States.idle], dictStates[States.targeting], () => AvaliableTargets.Any());

        stateMachine.AddTransition(dictStates[States.targeting], dictStates[States.idle], () => !AvaliableTargets.Any());

        //Set substates that do not end current state
        stateMachine.Substate(dictStates[States.attacking], () => TargetObject != null);
        //Debug change color to red
        stateMachine.Substate(dictBehaviours["Turn_Red"], () => TargetObject != null);

        //Initial state
        stateMachine.SetState(dictStates[States.idle]);
    }
}