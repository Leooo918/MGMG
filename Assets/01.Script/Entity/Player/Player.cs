using MGMG.Entities.Component;
using MGMG.FSM;
using MGMG.Input;
using MGMG.StatSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGMG.Entities
{
    public class Player : Entity
    {
        public EntityStateListSO playerFSM;
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }

        private StateMachine _stateMachine;
        private Dictionary<StateSO, EntityState> _stateDictionary;

        //public AnimParamSO comboCounterParam; 만약 근접공격이면 추가

        private EntityRenderer _renderer;

        [SerializeField] private float _dashCooltime = 2f;
        private float _currentCooltime;

        private EntityHealth _health;
        private StatElement _healthStat;
        private StatElement _reproductionStat;
        private StatElement _damageStat;
        private StatElement _critical;

        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new StateMachine();
            _stateDictionary = new Dictionary<StateSO, EntityState>();
            _currentCooltime = Time.time;

            foreach (StateSO state in playerFSM.states)
            {
                try
                {
                    Type t = Type.GetType(state.className);
                    var playerState = Activator.CreateInstance(t, this, state.animParam) as EntityState;
                    _stateDictionary.Add(state, playerState);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"{state.className} loading Error, Message : {ex.Message}");
                }
            }

            _renderer = GetCompo<EntityRenderer>();
        }

        private void OnDestroy()
        {
            //PlayerInput.DashEvent -= HandleDash;
            _health.OnDieEvent -= HandleDieEvent;
        }

        protected override void AfterInitComponents()
        {
            base.AfterInitComponents();


            _health = GetCompo<EntityHealth>();
            _health.OnDieEvent += HandleDieEvent;

            _damageStat = GetCompo<EntityStat>().StatDictionary["AttackPower"];
            _healthStat = GetCompo<EntityStat>().StatDictionary["Health"];
            _critical = GetCompo<EntityStat>().StatDictionary["Critical"];

        }

        private void HandleDieEvent()
        {
            //_dirImage.SetActive(false);
            ChangeState(playerFSM[FSMState.Die]);

            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = Time.timeScale / 200;

            _health.OnDieEvent -= HandleDieEvent;
        }

        private void Start()
        {
            _stateMachine.Initialize(GetState(playerFSM[FSMState.Idle]));
        }

        public void ChangeState(StateSO newState) => _stateMachine.ChangeState(GetState(newState));
        private EntityState GetState(StateSO stateSo) => _stateDictionary.GetValueOrDefault(stateSo);

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }
    }
}

