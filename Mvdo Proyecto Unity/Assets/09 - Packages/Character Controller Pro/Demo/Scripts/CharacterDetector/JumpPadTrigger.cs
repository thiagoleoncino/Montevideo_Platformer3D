using UnityEngine;
using Lightbug.CharacterControllerPro.Core;
using Lightbug.Utilities;

namespace Lightbug.CharacterControllerPro.Demo
{
    public class JumpPadTrigger : MonoBehaviour
    {
        public bool useLocalSpace = true;
        public Vector3 direction = Vector3.up;
        public float jumpPadVelocity = 10f;

        private void OnTriggerEnter(Collider other)
        {
            CharacterActor characterActor = other.GetComponent<CharacterActor>();
            if (characterActor == null || characterActor.GroundObject != gameObject)
                return;

            ApplyJumpPadEffect(characterActor);
        }

        private void OnTriggerStay(Collider other)
        {
            CharacterActor characterActor = other.GetComponent<CharacterActor>();
            if (characterActor == null || characterActor.GroundObject != gameObject)
                return;

            ApplyJumpPadEffect(characterActor);
        }

        private void ApplyJumpPadEffect(CharacterActor characterActor)
        {
            characterActor.ForceNotGrounded();

            Vector3 appliedDirection = useLocalSpace ? transform.TransformDirection(direction) : direction;
            characterActor.Velocity += appliedDirection * jumpPadVelocity;
        }

        private void OnDrawGizmos()
        {
            Vector3 appliedDirection = useLocalSpace ? transform.TransformDirection(direction) : direction;
            CustomUtilities.DrawArrowGizmo(transform.position, transform.position + appliedDirection * 2f, Color.red);
        }
    }
}
