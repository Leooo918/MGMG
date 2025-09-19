using MGMG.Entities;
using System;
using UnityEngine;

namespace MGMG.Core
{
    public class PlayerManager : MonoSingleton<PlayerManager>
    {
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
        public int EnemyKillCount { get; set; }


        public void AddKillCount()
        {
            EnemyKillCount++;
            AddExp(20);
        }

        private void LevelUp()
        {
            CurrentPlayerLevel++;
            OnChangedPlayerLevelEvent?.Invoke(CurrentPlayerLevel);
            MaxExp += (int)(MathF.Log(2, CurrentPlayerLevel) * 100);
        }
        public void AddExp(int exp)
        {
            CurrentExp += exp;
            UIManager.Instance.XpApply(exp);
            if (CurrentExp > MaxExp)
            {
                CurrentExp -= MaxExp;
                LevelUp();
            }
            OnExpChangedEvent?.Invoke(CurrentExp);
        }
    }
}

