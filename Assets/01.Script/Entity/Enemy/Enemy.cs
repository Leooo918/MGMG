using DG.Tweening;
using MGMG.Core;
using MGMG.Core.ObjectPooling;
using MGMG.Entities;
using MGMG.Entities.Component;
using MGMG.FSM;
using MGMG.StatSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGMG.Enemies
{
    public class Enemy : Entity, IPoolable
    {
        public LayerMask whatIsTarget;
        #region FSM
        public EntityStateListSO enemyFSM;

        private StateMachine _stateMachine;
        private Dictionary<StateSO, EntityState> _stateDictionary;
        #endregion

        #region Entity
        private Player _player;
        protected EntityRenderer _renderer;
        protected EntityStat _statCompo;
        protected EntityHealth _healthCompo;
        #endregion

        #region Stat
        [SerializeField] protected float _attackRange;
        [HideInInspector] public float lastAttackTime;
        [field: SerializeField] private StatElementSO _damageSO, _attackCooldownSO;
        public StatElement damageStat { get; private set; }
        public StatElement attackCooldownStat { get; private set; }
        #endregion

        #region Pooling
        public GameObject GameObject { get => gameObject; set { } }

        [SerializeField] private EnemyPoolingType _poolingType;
        public Enum PoolEnum => _poolingType;
        public void OnPop()
        {
            _healthCompo.Resurrection();
            _stateMachine.Initialize(GetState(enemyFSM[FSMState.Chase]));
        }

        public void OnPush()
        {
        }
        #endregion


        protected override void Awake()
        {
            base.Awake();
            #region StateMachine
            _stateMachine = new StateMachine();
            _stateDictionary = new Dictionary<StateSO, EntityState>();

            foreach (StateSO state in enemyFSM.states)
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
            #endregion

            _renderer = GetCompo<EntityRenderer>();

            _statCompo = GetCompo<EntityStat>();
            _healthCompo = GetCompo<EntityHealth>();
            _healthCompo.OnDieEvent += HandleDieEvent;
            _player = PlayerManager.Instance.Player;
            damageStat = _statCompo.StatDictionary[_damageSO];
            attackCooldownStat = _statCompo.StatDictionary[_attackCooldownSO];
        }

        protected virtual void HandleDieEvent()
        {
            
            ChangeState(enemyFSM[FSMState.Die]);
        }


        private void Start()
        {
            if (enemyFSM[FSMState.Chase] != null)
                _stateMachine.Initialize(GetState(enemyFSM[FSMState.Chase]));
            else
                _stateMachine.Initialize(GetState(enemyFSM[FSMState.Idle]));
        }

        public void ChangeState(StateSO newState) => _stateMachine.ChangeState(GetState(newState));
        private EntityState GetState(StateSO stateSo) => _stateDictionary.GetValueOrDefault(stateSo);
        public bool AttackRangeInPlayer() => Vector3.Distance(_player.transform.position, transform.position) < _attackRange;
        public Vector3 PlayerDirection() => (_player.transform.position - transform.position).normalized;

        protected virtual void Update()
        {
            _stateMachine.UpdateStateMachine();

            _renderer.SetRotation(PlayerDirection());
        }


        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
        
        
    }
}

