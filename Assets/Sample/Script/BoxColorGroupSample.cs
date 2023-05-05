#region

using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using rStar.Odin.Attributes;
using Sirenix.Utilities.Editor;
#endif

#endregion

namespace Sample
{
    [CreateAssetMenu]
    public class BoxColorGroupSample : ScriptableObject
    {
    #if UNITY_EDITOR

    #region Public Variables

        [ShowInInspector]
        [ColoredBoxGroup("Box1")]
        public Texture2D iconForBox1
        {
            get
            {
            #if UNITY_EDITOR
                return EditorIcons.Tag.Raw;
            #else
                return null;
            #endif
            }
        }

        [ColoredBoxGroup("Box2")]
        public Color group2Color = Color.green;

        [ColoredBoxGroup("Box1" , R = .43f , G = .96f , B = .64f ,
                         ShowIcon = true , ColorText = false , Icon = "@iconForBox1")]
        [PropertyOrder(-1)]
        public string A;

        [ColoredBoxGroup("Box1")]
        public string B;

        [ColoredBoxGroup("Box2" , Color = "@group2Color" , ShowIcon = true , ColorText = true ,
                         Icon = "@iconForBox2" , UseLowSaturation = true)]
        [PropertyOrder(-1)]
        public string C;

        [PreviewField]
        [ColoredBoxGroup("Box2")]
        public Texture2D iconForBox2;

    #endregion

    #endif
    }
}