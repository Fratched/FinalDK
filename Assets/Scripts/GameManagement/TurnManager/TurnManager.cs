using benjohnson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : Singleton<TurnManager>
{
    // States
    public Dictionary<string, TurnBaseState> states;
    public TurnBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    TurnBaseState _currentState;

    [Header("Enemy Attacks")]
    public float timeBetweenAttacks;

    // Variables
    public bool PlayerUsedAction { get { return _playerUsedAction; } set { _playerUsedAction = value; } }
    bool _playerUsedAction;

    protected override void Awake()
    {
        base.Awake();

        InitializeStates();
    }



    public void StartTurnManager()
    {
        _currentState = states["Idle"];
        _currentState.EnterState();
    }

    public void StopTurnManager()
    {
        _currentState?.ExitState();
        _currentState = null;
    }

    void Update()
    {
        if (_currentState != null)
            _currentState.UpdateState();

        // Toggle cheat to disable enemies' turn when the '3' key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameCheats.EnemiesDisabled = !GameCheats.EnemiesDisabled;

            Debug.Log($"Enemy turns are now {(GameCheats.EnemiesDisabled ? "disabled" : "enabled")}.");
        }
    }

    void InitializeStates()
    {
        states = new Dictionary<string, TurnBaseState>();

        states.Add("Idle", new TurnStateIdle(this));
        states.Add("Player", new TurnStatePlayer(this));
        states.Add("Enemy", new TurnStateEnemy(this));
    }

    public bool StateIs(string id)
    {
        if (states[id] == null) return false;

        return _currentState == states[id];
    }
}
