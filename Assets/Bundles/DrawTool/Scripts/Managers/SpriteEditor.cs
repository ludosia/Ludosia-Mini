using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Bundles.SpriteEditor {
    public class SpriteEditor : MonoBehaviour
    {
        #region Public Fields
        [SerializeField] Viewport viewport;
        #endregion

        #region Properties
        public static SpriteEditor Active { get; private set; }

        public Canvas Canvas => viewport.canvas;

        public Viewport Viewport => viewport;

        public EditableSprite EditedSprite { get; private set; }
        #endregion

        #region Static Methods
        public static void Open(EditableSprite editableSprite) {
            if (Active == null)
            {
                Active = Instantiate(DrawToolSettings.Default.editorPrefab, FindObjectOfType<UnityEngine.Canvas>().transform);
            }
            else
            {
                //Save current TextureDatas and load this one?
                throw new NotImplementedException();
            }

            Active.EditedSprite = editableSprite;

            Texture2D tex = null;
            if (Active.EditedSprite.TextureDatas == null || !Active.EditedSprite.TextureDatas.TryGenerateTexture(out tex))
            {
                Active.EditedSprite.GenerateNewTextureDatas().TryGenerateTexture(out tex);
            }

            Active.Canvas.LinkTexture(tex);
        }
        #endregion

        #region Public Methods
        public void LoadTexture(TextureDatas textureDatas) {
            Texture2D tex = null;

            if(!textureDatas.TryGenerateTexture(out tex))
            {
                Active.OpenNewTexturePanel();
            }

            Canvas.LinkTexture(tex);
        }

        public void OpenNewTexturePanel(Vector2Int dimensionsConstraint)
        {
            //Open new Texture Menu
        }

        public void OpenNewTexturePanel(Vector2Int minDimensions, Vector2Int maxDimensions)
        {
            //Open new Texture Menu
        }

        public void OpenNewTexturePanel()
        {
            //Open new Texture Menu
        }
        #endregion
    }
}
