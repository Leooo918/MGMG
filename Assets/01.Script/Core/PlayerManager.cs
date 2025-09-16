using MGMG.Entities;
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
    }
}

