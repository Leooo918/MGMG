using MGMG.Magic;
using MGMG.StatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MGMG.Entities
{
    public class PlayerMagicController : MonoBehaviour, IEntityComponent
    {
        private Dictionary<string, float> _coolDownDict;

        private Dictionary<MagicSO, PlayerMagic> _magicDict;
        private List<PlayerMagic> _containMagic;
        private List<float> _prevMagicUseTime;
        private Player _owner;

        [SerializeField] private MagicSO _debugMagic;
        [SerializeField] private int _debugUpgradeIndex;

        public event Action<MagicSO> OnGetMagic;
        public event Action<int, int> OnUpgradeMagic;

        public void Initialize(Entity entity)
        {
            _coolDownDict = new();
            _magicDict = new Dictionary<MagicSO, PlayerMagic>();
            _containMagic = new List<PlayerMagic>();
            _prevMagicUseTime = new List<float>();
            _owner = entity as Player;
        }

        public void GetMagic(MagicSO magic)
        {
            if(_magicDict.ContainsKey(magic))
            {
                UpgradeMagic(magic);
                return;
            }
            OnGetMagic?.Invoke(magic);

            PlayerMagic playerMagic = magic.magic.GetInstance();
            playerMagic.Initialize(_owner, magic.magicData);
            _magicDict.Add(magic, playerMagic);
            _containMagic.Add(playerMagic);
            _prevMagicUseTime.Add(Time.time);
        }

        public int GetMagicLevel(MagicSO magic)
        {
            if (_magicDict.ContainsKey(magic) == false) return 0;
            return (_magicDict[magic].CurrentLevel + 1);
        }
        public void UpgradeMagic(MagicSO magicSO)
        {
            if (_magicDict.TryGetValue(magicSO, out PlayerMagic magic))
            {
                magic.OnLevelUp();
                OnUpgradeMagic?.Invoke(_containMagic.IndexOf(magic), (magic.CurrentLevel + 1));
            }
        }

        private void Update()
        {
            for (int i = 0; i < _containMagic.Count; i++)
            {
                _containMagic[i].OnUpdate();

                if (_prevMagicUseTime[i] + (_containMagic[i].GetCoolTime() * GetCoolDown()) < Time.time)
                {
                    _prevMagicUseTime[i] = Time.time;
                    _containMagic[i].OnUseSkill();
                }
            }

            if (Keyboard.current.pKey.wasPressedThisFrame)
            {
                GetMagic(_debugMagic);
            }
            if (Keyboard.current.oKey.wasPressedThisFrame)
            {
                //UpgradeMagic(_debugUpgradeIndex);
            }
        }

        #region CoolDownFuntion

        private float GetCoolDown()
        {
            float value = 0;

            foreach (var key in _coolDownDict.Keys)
                value += _coolDownDict[key];

            return 1 - (value * 0.01f);
        }
        public void SetCoolDown(string key, float coolDown)
        {
            if (_coolDownDict.ContainsKey(key)) _coolDownDict[key] = coolDown;
            else _coolDownDict.Add(key, coolDown);
        }
        public void RemoveCoolDown(string key)
        {
            _coolDownDict.Remove(key);
        }

        #endregion
    }
}
