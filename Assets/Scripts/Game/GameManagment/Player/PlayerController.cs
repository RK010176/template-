using System;
using System.Collections;
using UnityEngine;
using Common;
using System.Collections.Generic;

namespace Game
{
    public class PlayerController : MonoBehaviour, IPlayer
    {
        [SerializeField] private Animator _animator;
        public PlayerSpecs PlayerSpecs { get; set; }        
        private float _movingSpeed;
        private List<AudioClip> _sounds;
        private AudioSource _audioSource;
        
        [SerializeField] private Item _prefabs;
                                
        private IPlayerInput _playerInput;
        private float _resetTime = 0;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _playerInput = GetComponent<IPlayerInput>();
            _playerInput.OnFire += Fire;
        }

        private void Start()
        {
            GetLavelData();
        }
        public void GetLavelData()
        {
            _movingSpeed = PlayerSpecs.MovingSpeed;                                                        
            _sounds = PlayerSpecs.Sounds;
        }



        private void Update()
        {
            #region Up
            if (_playerInput.Up)
            {
                _animator.SetBool("Run", true);
                transform.position += transform.forward * _movingSpeed * Time.deltaTime *1;
                Play1();
            }
            else
                _animator.SetBool("Run", false);
            #endregion

            #region Down
            if (_playerInput.Down)
                _animator.SetBool("Eat", true);
            else
                _animator.SetBool("Eat", false);
            #endregion

            #region Right
            if (_playerInput.Right)
                transform.Rotate(transform.up, 1f);
            #endregion

            #region Left
            if (_playerInput.Left)
                transform.Rotate(transform.up, -1f);
            #endregion

            #region Jump
            if (_playerInput.SpaceBar)
            {
                _animator.SetBool("Jump", true);
                transform.position += transform.forward * _movingSpeed / 2 * Time.deltaTime;
            }
            else
                _animator.SetBool("Jump", false);
            #endregion

            #region if Player falls over 
            //if (transform.eulerAngles.z < 300 && transform.eulerAngles.z > 60 
            //    || transform.eulerAngles.x < 300 && transform.eulerAngles.x > 60)
            //{
            //    _resetTime += Time.deltaTime;
            //    if (_resetTime > 3)
            //    {
            //        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
            //        transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
            //        _resetTime = 0f;
            //    }
            //}        
            #endregion
        }

        private bool _oneShot = true;
        private void Fire()
        {
            if (_oneShot)
            {
                Instantiate(_prefabs.GameObject, transform.position, transform.rotation);
                StartCoroutine(FinishAction());
                Play2();
            }
        }

        private IEnumerator FinishAction()
        {
            _oneShot = false;
            yield return new WaitForSeconds(1);
            _oneShot = true;
        }

        #region Sounds
        private void Play1()
        {
            _audioSource.clip = _sounds[0];
            _audioSource.loop = false;
            _audioSource.Play();
        }
        private void Play2()
        {
            _audioSource.clip = _sounds[1];
            _audioSource.loop = false;
            _audioSource.Play();
        }
        private void Play3()
        {
            _audioSource.clip = _sounds[2];
            _audioSource.loop = false;
            _audioSource.Play();
        }
        #endregion
    }
}