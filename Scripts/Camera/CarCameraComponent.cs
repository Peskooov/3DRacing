using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(CarCameraController))]
    public abstract class CarCameraComponent : MonoBehaviour
    {
        protected Car car;
        protected new Camera camera;

        public virtual void SetProperties(Car car, Camera camera)
        {
            this.car = car;
            this.camera = camera;
        }
    }
}