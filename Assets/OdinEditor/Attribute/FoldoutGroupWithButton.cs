#region

using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ActionResolvers;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using UnityEditor;
#endif

#endregion

namespace rStar.Odin.Attributes
{
    public class FoldoutGroupWithButtonAttribute : PropertyGroupAttribute
    {
    #region Public Variables

        public string hideAction;
        public string showAction;

    #endregion

    #region Constructor

        public FoldoutGroupWithButtonAttribute(string groupId , string showAction , string hideAction) :
                base(groupId)
        {
            this.showAction = showAction;
            this.hideAction = hideAction;
        }

        public FoldoutGroupWithButtonAttribute(string groupId) : base(groupId) { }

    #endregion
    }

#if UNITY_EDITOR
    public class FoldoutGroupWithButtonAttributeDrawer : OdinGroupDrawer<FoldoutGroupWithButtonAttribute>
    {
    #region Private Variables

        private LocalPersistentContext<bool> isExpanded;
        private ValueResolver<string>        labelGetter;

        private readonly GUIContent     animationvisibilitytoggleon  = EditorGUIUtility.IconContent("animationvisibilitytoggleon@2x");
        private readonly GUIContent     animationvisibilitytoggleoff = EditorGUIUtility.IconContent("animationvisibilitytoggleoff@2x");
        private          ActionResolver showActionResolver;
        private          ActionResolver hideActionResolver;
        private          bool           visible;

    #endregion

    #region Protected Methods

        protected override void DrawPropertyLayout(GUIContent label)
        {
            showActionResolver.DrawError();
            var headerLabel = labelGetter.GetValue();

            SirenixEditorGUI.BeginBox();
            SirenixEditorGUI.BeginBoxHeader();

            var iconContent = visible ? animationvisibilitytoggleon : animationvisibilitytoggleoff;
            if (GUILayout.Button(iconContent , SirenixGUIStyles.IconButton , GUILayout.Width(20) , GUILayout.Height(20)))
            {
                visible = !visible;
                HandleAction(visible);
            }

            isExpanded.Value = SirenixEditorGUI.Foldout(isExpanded.Value , headerLabel);
            SirenixEditorGUI.EndBoxHeader();

            if (SirenixEditorGUI.BeginFadeGroup(this , isExpanded.Value))
                for (var i = 0 ; i < Property.Children.Count ; i++)
                    Property.Children[i].Draw();

            SirenixEditorGUI.EndFadeGroup();
            SirenixEditorGUI.EndBox();
        }

        protected override void Initialize()
        {
            isExpanded = this.GetPersistentValue("FoldoutGroupWithButtonAttributeDrawer.isExpanded" ,
                                                 GeneralDrawerConfig.Instance.ExpandFoldoutByDefault);
            labelGetter        = ValueResolver.GetForString(Property , Attribute.GroupName);
            visible            = true;
            showActionResolver = ActionResolver.Get(Property , Attribute.showAction);
            hideActionResolver = ActionResolver.Get(Property , Attribute.hideAction);
            HandleAction(visible);
        }

    #endregion

    #region Private Methods

        private void HandleAction(bool visible)
        {
            if (visible) showActionResolver.DoActionForAllSelectionIndices();
            else hideActionResolver.DoActionForAllSelectionIndices();
        }

    #endregion
    }
#endif
}