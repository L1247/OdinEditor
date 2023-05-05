#region

using System;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
#endif

#endregion

namespace rStar.Odin.Attributes
{
#if UNITY_EDITOR
    public class ColoredBoxGroupDrawer : OdinGroupDrawer<ColoredBoxGroupAttribute>
    {
    #region Protected Methods

        /// <summary>
        ///     Draw the stuff
        /// </summary>
        /// <param name="label">Label string</param>
        protected override void DrawPropertyLayout(GUIContent label)
        {
            ValueResolver.DrawErrors(colorResolver);
            var useColorText = Attribute.ColorText;
            GUILayout.Space(Attribute.MarginTop);

            var headerLabel = Attribute.LabelText;

            if (Attribute.ShowLabel)
            {
            #if ODIN_INSPECTOR_3
                labelGetter.DrawError();
                headerLabel = labelGetter.GetValue();
            #endif

                if (string.IsNullOrEmpty(headerLabel)) headerLabel = "";
            }

            // color resolve
            Color groupColor;
            if (string.IsNullOrEmpty(Attribute.Color)) groupColor = new Color(Attribute.R , Attribute.G , Attribute.B , Attribute.A);
            else groupColor                                       = colorResolver.GetValue();
            if (Attribute.UseLowSaturation)
            {
                Color.RGBToHSV(groupColor , out var h , out var s , out var v);
                s = 0.45f;
                var hsvToRGB = Color.HSVToRGB(h , s , v);
                groupColor = hsvToRGB;
            }

            // icon resolve
            var icon = iconResolver.GetValue();

            GUIHelper.PushColor(groupColor);
            SirenixEditorGUI.BeginBox();
            SirenixEditorGUI.BeginBoxHeader();
            {
                if (Attribute.ShowLabel)
                {
                    if (Attribute.ShowIcon) GUILayout.Label(icon , GUILayout.Width(20) , GUILayout.MaxHeight(20));

                    if (Attribute.CenterLabel) SirenixEditorGUI.Title(headerLabel , null , TextAlignment.Center , false);
                    else SirenixEditorGUI.Title(headerLabel , null , TextAlignment.Left , false);
                }
            }
            SirenixEditorGUI.EndBoxHeader();
            if (useColorText == false) GUIHelper.PopColor();

            for (var i = 0 ; i < Property.Children.Count ; i++) Property.Children[i].Draw();

            SirenixEditorGUI.EndBox();
            if (useColorText) GUIHelper.PopColor();

            GUILayout.Space(Attribute.MarginBottom);
        }

    #endregion

    #if ODIN_INSPECTOR_3
        private ValueResolver<string>    labelGetter;
        private ValueResolver<Color>     colorResolver;
        private ValueResolver<Texture2D> iconResolver;

        /// <summary>
        ///     initialize values for colors, labels, etc
        /// </summary>
        protected override void Initialize()
        {
            labelGetter   = ValueResolver.GetForString(Property , Attribute.LabelText ?? Attribute.GroupName);
            colorResolver = ValueResolver.Get(Property , Attribute.Color , Color.white);
            iconResolver  = ValueResolver.Get(Property , Attribute.Icon , EditorIcons.Tag.Raw);
        }
    #endif
    }
#endif

    [AttributeUsage(AttributeTargets.All , AllowMultiple = true)]
    public class ColoredBoxGroupAttribute : BoxGroupAttribute
    {
    #region Public Variables

        public bool ColorText = true;

        public bool  ShowIcon;
        public bool  UseLowSaturation;
        public float R = 1 , G = 1 , B = 1 , A = 1;

        public string Color;
        public string Icon;
        public int    MarginBottom { get; set; }
        public int    MarginTop    { get; set; }

    #endregion

    #region Constructor

        public ColoredBoxGroupAttribute(
                string group ,
                float  r , float g , float b , float a ,
                float  order = 0) : base(group , true , false , order)
        {
            R         = r;
            G         = g;
            B         = b;
            A         = a;
            ShowLabel = true;
        }

        public ColoredBoxGroupAttribute(
                string group ,
                float  order = 0) : base(group , true , false , order)
        {
            ShowLabel = true;
        }

    #endregion
    }
}