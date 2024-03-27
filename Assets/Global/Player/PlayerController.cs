using UnityEngine;
using UnityEngine.InputSystem;

namespace Global.Player
{
    public class PlayerController : MonoBehaviour
    {
        private AttackBehaviour _attackBehaviour;
        // Start is called before the first frame update
        void Start()
        {
            _attackBehaviour = gameObject.GetComponent<AttackBehaviour>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnFire(InputValue inputValue)
        {
            //_attackBehaviour.Attack();
        }
    }
}
