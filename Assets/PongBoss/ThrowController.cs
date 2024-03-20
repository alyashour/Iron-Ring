using UnityEngine;
using UnityEngine.InputSystem;

namespace PongBoss
{
    public class ThrowController : MonoBehaviour
    {
        public float defaultSpeed = 1f;
        public GameObject sword;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnThrow(InputValue inputValue)
        {
            Rigidbody2D rb = Instantiate(sword, this.gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.up * defaultSpeed;
        }
    }
}
