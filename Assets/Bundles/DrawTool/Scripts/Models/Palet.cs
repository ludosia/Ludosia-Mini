using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bundles.SpriteEditor {
    public class Palet
    {
        #region Public Fields
        public PaletColor[] colors;
        #endregion

        #region Constructors
        public Palet(params Color[] defaultColors) {
            colors = new PaletColor[defaultColors.Length];

            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = new PaletColor(defaultColors[i]);
            }
        }
        #endregion

        #region Public Methods
        public Color GetColorFromId(PaletID paletID) {
            return colors[paletID.colorID].GetColorFromMode(paletID.colorMode);
        }
        #endregion
    }
}
