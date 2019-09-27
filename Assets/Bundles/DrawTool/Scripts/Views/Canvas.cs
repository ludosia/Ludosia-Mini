using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Bundles.SpriteEditor{
    [RequireComponent(typeof(RectTransform))]
    
    public class Canvas : MonoBehaviour, IPointerClickHandler, IDragHandler
    {
        #region Properties
        public RectTransform RectTransform { get; private set; }
        public RawImage Image { get; private set; }
        public Texture Texture => Image.texture;
        public Texture2D Texture2D => Texture as Texture2D;
        public Vector2Int Dimensions => Texture2D.GetDimensions();
        #endregion

        #region Events
        public class CanvasEvent<TArg> : UnityEvent<Canvas, TArg> { }
        public CanvasEvent<Vector2Int> onDimensionsChanged = new CanvasEvent<Vector2Int>();
        public CanvasEvent<Texture2D> onTextureChanged = new CanvasEvent<Texture2D>();
        #endregion

        #region Public Methods
        public Vector2Int GetPixelFromEventDatas(PointerEventData eventData) {
            Vector2 localCursor;
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out localCursor))
                return Vector2Int.zero;

            localCursor += RectTransform.sizeDelta / 2;

            return new Vector2Int(
                Mathf.FloorToInt(localCursor.x),    
                Mathf.FloorToInt(localCursor.y)    
            );
        }

        public void LinkTexture(Texture2D tex)
        {
            Image.texture = tex;
            RectTransform.sizeDelta = Dimensions;

            onDimensionsChanged.Invoke(this, Dimensions);
            onTextureChanged.Invoke(this, Texture2D);
        }
        #endregion

        #region Event Methods
        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Hit Pixel:" + GetPixelFromEventDatas(eventData));
        }
        #endregion

        #region Runtime Methods
        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            Image = GetComponent<RawImage>();
        }
        #endregion
    }
}
