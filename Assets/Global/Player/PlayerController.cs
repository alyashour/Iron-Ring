using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

namespace Global.Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        [SerializeField] private float interactionDistance = 1f;
        
        // Start is called before the first frame update
        void Start()
        {
            _playerMovement = gameObject.GetComponent<PlayerMovement>();

            gameObject.GetComponent<SpriteRenderer>().color = PlayerAttributes.PlayerColor;
        }

        // when the player interacts with an object
        private void OnInteract(InputValue inputValue)
        {
            // get a reference to the object
            AttackBehaviour.PlayerDirection direction = _playerMovement.playerDirection; // get direction

            // convert direction to vector
            Vector2 directionVector;
            switch (direction)
            {
                case AttackBehaviour.PlayerDirection.Left:
                    directionVector = Vector2.left;
                    break;
                case AttackBehaviour.PlayerDirection.Right:
                    directionVector = Vector2.right;
                    break;
                case AttackBehaviour.PlayerDirection.Up:
                    directionVector = Vector2.up;
                    break;
                case AttackBehaviour.PlayerDirection.Down:
                    directionVector = Vector2.down;
                    break;
                default:
                    directionVector = Vector2.zero;
                    break;
            }

            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionVector, interactionDistance);

            if (hit.collider != null)
            {
                var interactable = hit.collider.GetComponent<IInteractable>();
                
                if (interactable != null)
                    interactable.Interact();
            }
        }
    }
}