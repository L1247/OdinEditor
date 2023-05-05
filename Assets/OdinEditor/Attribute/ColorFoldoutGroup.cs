#region

using System;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
#endif

#endregion

namespace rStar.Odin.Attributes
{
#if UNITY_EDITOR
    public class ColorFoldoutGroupAttributeDrawer : OdinGroupDrawer<ColorFoldoutGroupAttribute>
    {
    #region Private Variables

        private LocalPersistentContext<bool> isExpanded;

    #endregion

    #region Protected Methods

        protected override void DrawPropertyLayout(GUIContent label)
        {
            GUIHelper.PushColor(new Color(Attribute.R , Attribute.G , Attribute.B , Attribute.A));
            SirenixEditorGUI.BeginBox();
            SirenixEditorGUI.BeginBoxHeader();
            GUIHelper.PopColor();

            isExpanded.Value = SirenixEditorGUI.Foldout(isExpanded.Value , label);
            SirenixEditorGUI.EndBoxHeader();

            if (SirenixEditorGUI.BeginFadeGroup(this , isExpanded.Value))
                for (var i = 0 ; i < Property.Children.Count ; i++)
                    Property.Children[i].Draw();

            SirenixEditorGUI.EndFadeGroup();
            SirenixEditorGUI.EndBox();
        }

        protected override void Initialize()
        {
            isExpanded = this.GetPersistentValue("ColorFoldoutGroupAttributeDrawer.isExpanded" ,
                                                 GeneralDrawerConfig.Instance.ExpandFoldoutByDefault);
        }

    #endregion
    }
#endif
    public class ColorFoldoutGroupAttribute : PropertyGroupAttribute
    {
    #region Public Variables

        public float R , G , B , A;

    #endregion

    #region Constructor

        public ColorFoldoutGroupAttribute(string path) : base(path) { }

        public ColorFoldoutGroupAttribute(string path , float r , float g , float b , float a = 1f) : base(path)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

    #endregion

    #region Protected Methods

        protected override void CombineValuesWith(PropertyGroupAttribute other)
        {
            var otherAttr = (ColorFoldoutGroupAttribute)other;

            R = Math.Max(otherAttr.R , R);
            G = Math.Max(otherAttr.G , G);
            B = Math.Max(otherAttr.B , B);
            A = Math.Max(otherAttr.A , A);
        }

    #endregion
    }
}