using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bundles.SettingsManagement;

namespace Bundles.SpriteEditor {
    [CreateAssetMenu(menuName ="Bundles/DrawTool/Settings")]
    public class DrawToolSettings : Settings<DrawToolSettings>
    {
        [Header("Editor")]
        public SpriteEditor editorPrefab;

        [Header("Texture")]
        [Tooltip("The number of pixel by columns/rows in a cell")]
        public int cellRatio = 32;
        [Tooltip("The path of the folder where the TextureDatas will be stored")]
        public string datasFolderPath;
        [Tooltip("The path of the folder where rendered textures will be stored")]
        public string renderFolderPath;

        [Header("Palet")]
        [Tooltip("The default colors for a new palet (the amount of colors here will be the amount of colors in the palet)")]
        public Color[] defaultPaletColors;
    }
}
