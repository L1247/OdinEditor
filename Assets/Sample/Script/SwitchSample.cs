#region

using rStar.Odin.Attributes;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Sample
{
    [CreateAssetMenu]
    public class SwitchSample : ScriptableObject
    {
    #region Private Variables

        [SerializeField]
        [Switch(DefaultColor.Yellow)]
        [ToggleLeft]
        private bool toggle;

    #endregion
    }
}