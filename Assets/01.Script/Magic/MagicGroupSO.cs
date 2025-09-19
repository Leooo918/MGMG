using MGMG.Magic;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/MagicGroup")]
public class MagicGroupSO : ScriptableObject
{
    public List<MagicSO> magicList;
}
