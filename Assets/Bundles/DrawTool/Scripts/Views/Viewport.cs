using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Bundles.SpriteEditor {
    public class Viewport : MonoBehaviour
    {
        #region Public Fields
        [Header("Relations")]
        public Canvas canvas;
        public RectTransform viewport;
        public RectTransform container;

        [Header("Zoom Settings")]
        [SerializeField] float zoom = 10;
        [SerializeField] float zoomMargin = 10;

        [Header("Content Settings")]
        [SerializeField] int canvasMargin = 10;
        #endregion

        #region Properties
        public RectTransform rectTransform { get; private set; }

        public float Zoom
        {
            get => zoom;
            set
            {
                zoom = value;
                UpdateZoom();
            }
        }
        #endregion

        #region Public Methods
        public void ResetZoom() {
            float minDim = rectTransform.rect.height <= rectTransform.rect.width ? rectTransform.rect.height : rectTransform.rect.width;
            minDim -= zoomMargin;

            Vector2 canvasDim = canvas.RectTransform.sizeDelta;
            Zoom = minDim / (canvasDim.x >= canvasDim.y ? canvasDim.x : canvasDim.y);
        }
        #endregion

        #region Private Methods
        void UpdateZoom()
        {
            container.transform.localScale = new Vector3(zoom, zoom, 1);
        }
        #endregion

        #region Event Methods
        public void OnCanvasDimensionsChanged(Canvas canvas, Vector2Int dimensions)
        {
            container.sizeDelta = new Vector2(dimensions.x + canvasMargin, dimensions.y + canvasMargin);
            ResetZoom();
        }
        #endregion

        #region Runtime Methods
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvas.onDimensionsChanged.AddListener(OnCanvasDimensionsChanged);
        }
        #endregion

        #region Editor Methods
        private void OnValidate()
        {
#if UNITY_EDITOR
            if(container != null)
            {
                UpdateZoom();
            }
#endif
        }
        #endregion
    }
}
