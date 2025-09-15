using System;
using UnityEngine;

namespace MGMG.Util
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class VisibleInspectorSOAttribute : PropertyAttribute
    {
        public VisibleInspectorSOAttribute() { }
    }
}
