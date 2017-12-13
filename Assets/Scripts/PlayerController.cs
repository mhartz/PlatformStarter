using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour {
        public float MaxMoveSpeed;
        public float MoveForce;
        public float JumpSpeed;
        public bool AllowDoubleJump;
        public bool AllowRun;

        private Vector3 _playerControls;
        private Rigidbody2D _rb;
        private bool _isGrounded;
        private int _jumpCount = 0;
        private float _resetMoveSpeed;
        private float _runSpeed;

        void Start () {
            _rb = GetComponent<Rigidbody2D>();
            _resetMoveSpeed = MaxMoveSpeed;
            _runSpeed = MaxMoveSpeed * 2f;
        }
	
        void Update () {
            _playerControls = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);

            if (AllowDoubleJump)
                PlayerDoubleJump();
            else
                PlayerJump();
        }

        void FixedUpdate()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            //Running when shift it held
            if (AllowRun && Input.GetButton("Run"))
                MaxMoveSpeed = _runSpeed;
            else
                MaxMoveSpeed = _resetMoveSpeed;

            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, MaxMoveSpeed);
            _rb.AddForce(_playerControls.normalized * MoveForce);
            Debug.Log(_playerControls.normalized * MoveForce);
        }

        private void PlayerJump()
        {
            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _rb.velocity = new Vector3(_rb.velocity.x, JumpSpeed, 0f);
            }
        }

        private void PlayerDoubleJump()
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (_jumpCount < 2)
                {
                    _rb.velocity = new Vector3(_rb.velocity.x, JumpSpeed, 0f);
                    _jumpCount++;
                }                
            }
        }

        private void Instakill()
        {
            //Todo : Need to implement a respawn based on the Game Manager

            transform.position = new Vector3(0, 0, 0);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                _isGrounded = true;

                if (AllowDoubleJump)
                    _jumpCount = 0;
            }

            if (collision.gameObject.tag == "Instakill")
                Instakill();
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                _isGrounded = false;
            }
        }
    }
}
