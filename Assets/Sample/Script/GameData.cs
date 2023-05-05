#region

using rStar.Odin.Attributes;
using UnityEngine;

#endregion

namespace Sample
{
    [CreateAssetMenu]
    public class GameData : ScriptableObject
    {
    #region Private Variables

        [SerializeField]
        [Switch(DefaultColor.Yellow)]
        private bool toggle;

    #endregion
    }
}