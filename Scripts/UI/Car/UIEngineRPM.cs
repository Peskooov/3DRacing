using Racing;
using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UIEngineRPM : MonoBehaviour
    {
        [SerializeField] private Image engineRpm;
        [SerializeField] private Car car;

        private void Update()
        {
            engineRpm.fillAmount = car.EngineRpm / car.EngineMaxRpm;
        }
    }
}