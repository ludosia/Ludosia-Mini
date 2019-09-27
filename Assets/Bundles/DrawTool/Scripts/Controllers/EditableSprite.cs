using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bundles.SpriteEditor
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class EditableSprite : MonoBehaviour
    {
        #region Public Fields
        [SerializeField] Vector2Int dimensions;
        [SerializeField] SpriteRenderer targetRenderer;
        #endregion

        #region Private Fields
        TextureDatas textureDatas;
        #endregion

        #region Properties
        public Sprite sprite => targetRenderer.sprite;
        public TextureDatas TextureDatas => textureDatas;
        public Vector2Int Dimensions => dimensions;
        #endregion

        #region Public Methods
        public void EditSprite() {
            SpriteEditor.Open(this);
        }

        public TextureDatas GenerateNewTextureDatas() {
            textureDatas = new TextureDatas(dimensions);

            return textureDatas;
        }
        #endregion

        #region Editor Methods
        private void Reset()
        {
            targetRenderer = GetComponent<SpriteRenderer>();
        }
        #endregion

        //Temp
        private void Start()
        {
            EditSprite();
        }
    }
}
