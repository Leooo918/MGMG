using System;
using UnityEngine;

namespace MGMG.Magic
{
    [CreateAssetMenu(menuName = "SO/Magic")]
    public class MagicSO : ScriptableObject
    {
        public string magicName;
        [SerializeReference] public PlayerMagic magic;
        [SerializeReference] public MagicData magicData;

        private void OnValidate()
        {
            if (magic != null && magicData != null) return;

            try
            {
                string magicStr = $"MGMG.Magic.{magicName}Magic"; 
                string dataStr = $"MGMG.Magic.{magicName}MagicData";

                Type magicType = Type.GetType(magicStr);
                magic = (PlayerMagic)Activator.CreateInstance(magicType);

                Type dataType = Type.GetType(dataStr);
                magicData = (MagicData)Activator.CreateInstance(dataType);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }

        }
    }
}
