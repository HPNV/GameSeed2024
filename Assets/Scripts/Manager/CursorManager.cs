using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class CursorManager
    {
        private Dictionary<CursorType, Texture2D> cursorTextures;
        
        public void Initialize()
        {
            cursorTextures = new Dictionary<CursorType, Texture2D>
            {
                { CursorType.Arrow, Resources.Load<Texture2D>("Cursors/Arrow") },
                { CursorType.Hand, Resources.Load<Texture2D>("Cursors/Hand") }
            };
        }

        public void ChangeCursor(CursorType type)
        {
            Cursor.SetCursor(cursorTextures[type], Vector2.zero, CursorMode.Auto);
        }
    }
    
    public enum CursorType
    {
        Arrow,
        Hand
    }
}