using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class HeadBobber : MonoBehaviour
    {
        public float walkingBobbingSpeed = 14f;
        public float bobbingAmount = 0.08f;
        public PlayerCharacterController controller;

        float defaultPosY = 0;
        float timer = 0;

        void Start()
        {
            defaultPosY = transform.localPosition.y;
        }

        // Update is called once per frame
        void Update()
        {   
            // bob head up and down if moving
            if (Mathf.Abs(controller.CharacterVelocity.x) > 0.1f || Mathf.Abs(controller.CharacterVelocity.z) > 0.1f)
            {
                //Player is moving
                timer += Time.deltaTime * walkingBobbingSpeed;
                transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
            }
            // reset head to default y position
            else
            {
                //Idle
                timer = 0;
                transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * walkingBobbingSpeed), transform.localPosition.z);
            }
        }
    }
}