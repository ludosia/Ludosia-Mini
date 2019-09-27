using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Bundles.SpriteEditor {
    [Serializable]
    public class PaletID
    {
        public int colorID;
        public ColorMode colorMode;

        public PaletID(int colorID, ColorMode mode) {
            this.colorID = colorID;
            colorMode = mode;
        }
    }
}
