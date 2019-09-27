using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bundles.SpriteEditor {
    public class PaletColor
    {
        #region Public Fields
        public Color color;
        public float darkRatio;
        public float lightRatio;
        #endregion

        #region Properties
        public Color MainColor => color;
        public Color DarkColor => Color.Lerp(MainColor, Color.black, darkRatio);
        public Color LightColor => Color.Lerp(MainColor, Color.white, lightRatio);
        #endregion

        #region Constructors
        public PaletColor() : this(Color.white) { }

        public PaletColor(Color mainColor, float defaultRatios = 0.1f)
        {
            color = mainColor;
            darkRatio = defaultRatios;
            lightRatio = defaultRatios;
        }
        #endregion

        #region Public Methods
        public Color GetColorFromMode(ColorMode mode)
        {
            switch (mode)
            {
                case ColorMode.main:   return MainColor;
                case ColorMode.dark:   return DarkColor;
                case ColorMode.light:  return LightColor;
                default:               return MainColor;
            }
        }
        #endregion
    }
}
