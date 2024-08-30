using System;
using UnityEngine;

namespace Script
{
    public class PointerConsumer : MonoBehaviour
    {
        public float rotationOffset;
        public bool flipSprite = false;
        
        [SerializeField, InspectorName("PointerService")]
        private PointerService ps;

        private void FixedUpdate()
        {
            Vector2 direction = ((Vector2)ps.GetPointerPosition() - (Vector2)transform.position).normalized;
            transform.right = direction;
            
            bool flip = direction.x < 0;
            
            FlipSprite(flip);
            
            if(!flipSprite) transform.Rotate(0, 0, rotationOffset);
            else
            {
                transform.Rotate(0, 0,  flip ? -rotationOffset : rotationOffset);
            }
        }

        private void FlipSprite(bool flip)
        {
            if (!flipSprite) return;
            Vector2 scale = transform.localScale;
            if (flip)
            {
                scale.y = Math.Min(scale.y, scale.y * -1);
            }
            else
            {
                scale.y = Math.Abs(scale.y);
            }
            transform.localScale = scale;
        }
    }
}