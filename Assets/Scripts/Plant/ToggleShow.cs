using System.Collections;
using Plant;
using Plant.States;
using UnityEngine;
using UnityEngine.UI;

namespace Plant
{
    public class ToggleShow : MonoBehaviour
    {
        [SerializeField] private Collider2D plantCollider;
        [SerializeField] private Plant plant;
        [SerializeField] private Collider2D upgradeButton;
        [SerializeField] private Collider2D removeButton;
        [SerializeField] private Collider2D waterButton;
        [SerializeField] private GameObject plantObject;
        private Vector3 originalScale;
        private Vector3 hiddenScale = new Vector3(0.01f, 0.01f, 0.01f);
        private float animationDuration = 0.5f;
        private Camera mainCamera;
        private Coroutine scalingCoroutine;
        private bool onShow = false;
        private bool firstClick = false;

        private void Start()
        {
            originalScale = transform.localScale;
            transform.localScale = hiddenScale;
            mainCamera = Camera.main;
        }

        private void Update()
        {
            Debug.Log(plant.CurrentState);
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                if (plantCollider.OverlapPoint(mousePos))
                {
                    if (!firstClick)
                    {
                        firstClick = true;
                    }
                    else
                    {
                        Toggle();
                    }
                } else if(onShow){
                    Toggle();
                }

                if (upgradeButton.OverlapPoint(mousePos))
                {
                    Upgrade();
                }

                if (removeButton.OverlapPoint(mousePos))
                {
                    Destroy(plantObject);
                }

                if (waterButton.OverlapPoint(mousePos))
                {
                    Water();
                }
            }
        }

        public void Upgrade() {
            if(SingletonGame.Instance.homeBase.sun < 5) return;
            if(plant.Data.level == 3) return;
            PlantData plantData = plant.Data;
            plantData.health += plantData.health * 0.3f;
            plantData.damage += plantData.damage * 0.3f;
            plantData.level++;
            SingletonGame.Instance.homeBase.sun -= 5;
            plant.Data = plantData; 
        }

        public void Water()
        {
            if(SingletonGame.Instance.homeBase.water < 5) 
                return;
            plant.Grow();
        }

        public void Toggle()
        {
            if (scalingCoroutine != null)
            {
                StopCoroutine(scalingCoroutine);
            }

            if(plant.CurrentState == EPlantState.Grow) {
                waterButton.gameObject.SetActive(true);
            } else {
                waterButton.gameObject.SetActive(false);
            }

            Vector3 targetScale = onShow ? hiddenScale : originalScale;
            scalingCoroutine = StartCoroutine(ScaleObject(targetScale));
        }

        private IEnumerator ScaleObject(Vector3 targetScale)
        {
            onShow = targetScale == originalScale;
            Vector3 currentScale = transform.localScale;
            float timeElapsed = 0;

            while (timeElapsed < animationDuration)
            {
                timeElapsed += Time.deltaTime;
                transform.localScale = Vector3.Lerp(currentScale, targetScale, timeElapsed / animationDuration);
                yield return null;
            }

            transform.localScale = targetScale;
        }
    }
}
