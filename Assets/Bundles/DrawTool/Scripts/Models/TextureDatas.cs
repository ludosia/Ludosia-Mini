using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Bundles.SpriteEditor {
    public class TextureDatas
    {
        #region Public Fields
        public Palet palet;
        public PaletID[,] grid;
        public int xDimension;
        public int yDimension;
        public int cellRatio;
        public string renderPath;
        #endregion

        #region Properties
        public Vector2Int Dimensions => new Vector2Int(xDimension, yDimension);

        public bool IsValid {
            get
            {
                if (palet == null)
                    throw new Exception(string.Format("Invalid datas : No Palet datas"));

                if (grid == null)
                    throw new Exception(string.Format("Invalid datas : No Grid datas"));

                if(grid.GetLength(0) != xDimension * cellRatio || grid.GetLength(1) != yDimension * cellRatio)
                    throw new Exception(string.Format("Invalid datas : Invalid Grid datas"));

                return true;
            }
        }
        #endregion

        #region Constructors
        public TextureDatas(Vector2Int dimensions)
        {
            cellRatio = DrawToolSettings.Default.cellRatio;
            xDimension = dimensions.x;
            yDimension = dimensions.y;

            palet = new Palet(DrawToolSettings.Default.defaultPaletColors);
            grid = new PaletID[xDimension * cellRatio, yDimension * cellRatio];           
        }
        #endregion

        #region Public Methods
        public bool TryGenerateTexture(out Texture2D generatedTexture)
        {
            try
            {
                if (IsValid) {
                    generatedTexture = new Texture2D(grid.GetLength(0), grid.GetLength(1));

                    for (int x = 0; x < grid.GetLength(0); x++)
                    {
                        for (int y = 0; y < grid.GetLength(1); y++)
                        {
                            if(grid[x, y] == null)
                                generatedTexture.SetPixel(x, y, Color.red);
                            else
                                generatedTexture.SetPixel(x, y, palet.GetColorFromId(grid[x,y]));
                        }
                    }

                    generatedTexture.Apply();

                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

            generatedTexture = null;
            return false;
        }

        public Texture2D GetRenderedTexture() {
            throw new NotImplementedException();
        }
        #endregion
    }
}
