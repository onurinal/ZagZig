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
            UpdateColorTimer();
            LerpGroundColor();
        }

        private void UpdateColorTimer()
        {
            currentTime += Time.deltaTime;

            if (currentTime >= colorChangeTime)
            {
                currentColorIndex = (currentColorIndex + 1) % groundColors.Length;
                currentTime = 0;
            }
        }

        private void LerpGroundColor()
        {
            groundMaterial.color = Color.Lerp(groundMaterial.color, groundColors[currentColorIndex], colorChangeSpeed * Time.deltaTime);
        }

        private void OnDestroy()
        {
            groundMaterial.color = groundColors[0];
        }
    }
}