using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Racing
{
    public class UIGearbox : MonoBehaviour
    {
        [SerializeField] private Text gear;
        [SerializeField] private Car car;

        private void Start()
        {
            car.GearChanged += OnGearChanged;
        }
        private void OnDestroy()
        {
            car.GearChanged -= OnGearChanged;
        }

        private void OnGearChanged(string gearName)
        {
            gear.text = gearName;
        }
    }
}