using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bundles.SpriteEditor
{
    public static class Texture2DExtensions
    {
        public static Vector2Int GetDimensions(this Texture2D tex)
        {
            return new Vector2Int(tex.width, tex.height);
        }
    }
}
