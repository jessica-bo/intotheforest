using UnityEngine;
using Unity.FPS.Game;

namespace Unity.FPS.Gameplay
{
    public class HeadBobber : MonoBehaviour
    {
        public float walkingBobbingSpeed = 14f;
        public float bobbingAmount = 0.08f;
        public PlayerCharacterController controller;

        float defaultPosY = 0;
        float timer = 0;

        // Start is called before the first frame update
        void Start()
        {
            defaultPosY = transform.localPosition.y;
        }

        // Update is called once per frame
        void Update()
        {
            if (Mathf.Abs(controller.CharacterVelocity.x) > 0.1f || Mathf.Abs(controller.CharacterVelocity.z) > 0.1f)
            {
                //Player is moving
                timer += Time.deltaTime * walkingBobbingSpeed;
                transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
            }
            else
            {
                //Idle
                timer = 0;
                transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * walkingBobbingSpeed), transform.localPosition.z);
            }
        }
    }
}