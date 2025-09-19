using MGMG.Entities;
using System;
using UnityEngine;

namespace MGMG.Core
{
    public class PlayerManager : MonoSingleton<PlayerManager>
    {
        public event Action<int> OnAddExp;
        private Player _player;
        public Player Player
        {
            get
            {
                if (_player == null)
                {
                    _player = FindFirstObjectByType<Player>();
                    if (_player == null)
                        Debug.Log("플레이어의 영압이 사라졌다!!");
                }
                return _player;
            }
        }
        public int CurrentPlayerLevel { get; private set; } = 1;
        public event Action<int> OnChangedPlayerLevelEvent;
        public int CurrentExp { get; private set; } = 0;
        public event Action<int> OnExpChangedEvent;
        public int MaxExp { get; private set; } = 100;
        [SerializeField] private Vector2Int _minMaxPlayerLevel = new Vector2Int(1, 33);
        public int EnemyKillCount { get; set; }


        public void AddKillCount()
        {
            EnemyKillCount++;
            AddExp(20);
        }

        private void LevelUp()
        {
            CurrentPlayerLevel++;
            if (CurrentPlayerLevel > _minMaxPlayerLevel.y)
                CurrentPlayerLevel = _minMaxPlayerLevel.y;

            OnChangedPlayerLevelEvent?.Invoke(CurrentPlayerLevel);
            if(CurrentPlayerLevel % 7 == 0)
            {
                UIManager.Instance.CardSelectPanel.Open();
            }
            else
            {
                UIManager.Instance.MagicSelectPanel.Open();
            }

            MaxExp += (int)(MathF.Log(2, CurrentPlayerLevel) * 100);
        }
        public void AddExp(int exp)
        {
            CurrentExp += exp;
            OnAddExp?.Invoke(exp);
            if (CurrentExp > MaxExp)
            {
                CurrentExp -= MaxExp;
                LevelUp();
            }
            OnExpChangedEvent?.Invoke(CurrentExp);
        }
    }
}

