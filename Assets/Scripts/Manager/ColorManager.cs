using UnityEngine;

namespace ZagZig.Manager
{
    public class ColorManager : MonoBehaviour
    {
        [SerializeField] private Material groundMaterial;
        [SerializeField] private float colorChangeTime;
        [SerializeField] private Color[] groundColors;
        [SerializeField] [Range(0, 5)] private float colorChangeSpeed;

        private int currentColorIndex = 0;
        private float currentTime;

        private void Update()
        {
            CheckColorChangeTime();
            UpdateGroundColor();
        }

        private void CheckColorChangeTime()
        {
            if (currentTime >= colorChangeTime)
            {
                SetColorForGround();
                currentTime = 0;
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }

        private void SetColorForGround()
        {
            currentColorIndex++;

            if (currentColorIndex >= groundColors.Length)
            {
                currentColorIndex = 0;
            }
        }

        private void UpdateGroundColor()
        {
            groundMaterial.color = Color.Lerp(groundMaterial.color, groundColors[currentColorIndex], colorChangeSpeed * Time.deltaTime);
        }

        private void OnDestroy()
        {
            groundMaterial.color = groundColors[0];
        }
    }
}