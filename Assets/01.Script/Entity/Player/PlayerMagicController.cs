using MGMG.Magic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MGMG.Entities
{
    public class PlayerMagicController : MonoBehaviour, IEntityComponent
    {
        private List<PlayerMagic> _containMagic;
        private List<float> _prevMagicUseTime;
        private Player _owner;

        [SerializeField] private MagicSO _debugMagic;
        [SerializeField] private int _debugUpgradeIndex;

        public void Initialize(Entity entity)
        {
            _containMagic = new List<PlayerMagic>();
            _prevMagicUseTime = new List<float>();
            _owner = entity as Player;
        }

        public void GetMagic(MagicSO magic)
        {
            PlayerMagic playerMagic = magic.magic.GetInstance();
            playerMagic.Initialize(_owner, magic.magicData);
            _containMagic.Add(playerMagic);
            _prevMagicUseTime.Add(Time.time);
        }

        public void UpgradeMagic(int index)
        {
            if (index < 0 || index >= _containMagic.Count) return;
            _containMagic[index].OnLevelUp();
        }

        private void Update()
        {
            for(int i = 0; i < _containMagic.Count; i++)
            {
                _containMagic[i].OnUpdate();

                if (_prevMagicUseTime[i] + _containMagic[i].GetCoolTime() < Time.time )
                {
                    _prevMagicUseTime[i] = Time.time;
                    _containMagic[i].OnUseSkill();
                }
            }

            if (Keyboard.current.pKey.wasPressedThisFrame)
            {
                GetMagic(_debugMagic);
            }
            if(Keyboard.current.oKey.wasPressedThisFrame)
            {
                UpgradeMagic(_debugUpgradeIndex);
            }
        }
    }
}
