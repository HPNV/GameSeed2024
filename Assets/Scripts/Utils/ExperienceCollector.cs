using UnityEngine;

namespace Utils
{
    public class ExperienceCollector : MonoBehaviour
    {
        private void Update()
        {
            var camera = Camera.main;
            var cursorPosition = camera!.ScreenToWorldPoint(Input.mousePosition);
            cursorPosition.z = -0.8f;    
        
            transform.position = cursorPosition;
        }
    }
}