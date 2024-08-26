using UnityEngine;

namespace Racing
{
    public class WheelEffect : MonoBehaviour
    {
        [SerializeField] private WheelCollider[] wheels;
        [SerializeField] private ParticleSystem[] wheelsSmoke;

        [SerializeField] private float forwardSlipLimit;
        [SerializeField] private float sidewaySlipLimit;

        [SerializeField] private GameObject skidPrefab;
        [SerializeField] private new AudioSource audio;

        private WheelHit wheelHit;
        private Transform[] skidTrail;

        private void Start()
        {
            skidTrail = new Transform[wheels.Length];
        }

        private void Update()
        {
            bool isSlip = false;

            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].GetGroundHit(out wheelHit);

                if (wheels[i].isGrounded)
                {
                    if (wheelHit.forwardSlip > forwardSlipLimit || wheelHit.sidewaysSlip > sidewaySlipLimit)
                    {
                        if (skidTrail[i] == null)
                            skidTrail[i] = Instantiate(skidPrefab).transform;

                        if (audio.isPlaying == false)
                            audio.Play();

                        if (skidTrail[i] != null)
                        {
                            skidTrail[i].position = wheels[i].transform.position - wheelHit.normal * wheels[i].radius;
                            skidTrail[i].forward = -wheelHit.normal;

                            wheelsSmoke[i].transform.position = skidTrail[i].position;
                            wheelsSmoke[i].Emit(1);
                        }

                        isSlip = true;

                        continue;
                    }
                }

                skidTrail[i] = null;
                wheelsSmoke[i].Stop();
            }

            if (isSlip == false)
                audio.Stop();
        }
    }
}