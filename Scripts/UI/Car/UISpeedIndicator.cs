using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UISpeedIndicator : MonoBehaviour
    {
        [SerializeField] private Text speedText;
        [SerializeField] private Car car;

        
        private void Update()
        {
            speedText.text = car.LinearVelocity.ToString("F0");
        }
    }
}