#region

using rStar.Odin.Attributes;
using UnityEngine;

#endregion

namespace Sample
{
    [CreateAssetMenu]
    public class ColorFoldoutGroupSample : ScriptableObject
    {
    #region Public Variables

        [ColorFoldoutGroup("Group1" , 0.15f , .7f , 1f)]
        [Switch]
        public bool aa;

        [ColorFoldoutGroup("Group2" , 0.65f , .3f , 1f)]
        [Switch(DefaultColor.Red)]
        public bool cc;

        [ColorFoldoutGroup("Group1")]
        public int bb;

        [ColorFoldoutGroup("Group2")]
        public int ddd;

    #endregion
    }
}