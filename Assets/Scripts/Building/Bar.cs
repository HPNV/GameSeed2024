using UnityEngine;
using UnityEngine.UI;

namespace Building
{
    public class Bar : MonoBehaviour
    {
        [SerializeField] private Slider currentExpBar;

        private float currentValue;

        public float Exp{
            get => currentValue;
            set {
                currentValue = value;
            }
        }

        void Start()
        {
            currentExpBar.value = 0;
        }

        void Update()
        {
            currentExpBar.value = currentValue;
        }

        public void setMaxValue(float maxExp) {
            currentExpBar.maxValue = maxExp;
        }
    }
}
