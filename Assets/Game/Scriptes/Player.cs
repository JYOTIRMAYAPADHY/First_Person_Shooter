using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _speed = 3.5f;
    private float _gravity = 9.81f;
    [SerializeField]
    private GameObject _muzzle;
    [SerializeField]
    private GameObject _hitMarker;
    [SerializeField]
    private AudioSource _weaponAudio;
    [SerializeField]
    private int currentAmmino;
    private int maxAmmino = 100;  //total bulllets
    private bool isReloading = false;
    private UIManager _uiManager;

    //check if player has coin or not
    public bool hasCoin=false;

    [SerializeField]
    private GameObject _weapon;

        // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        //hide mouse
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentAmmino = maxAmmino;

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Raycasting and clicking / shooting
        if (Input.GetMouseButton(0) && currentAmmino>0)
        {
            shoot();
        }
        else
        {
            _muzzle.SetActive(false);
            _weaponAudio.Stop();
        }

        if (Input.GetKeyDown(KeyCode.R) && isReloading== false)
        {
            StartCoroutine(Reload());
            isReloading = true;
        }

        //if eascape key pressed then unlock
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        CalculateMovement();        
    }

    void shoot()
    {
        //check for audio
        if (_weaponAudio.isPlaying == false)
        {
            _weaponAudio.Play();
        }
        currentAmmino--;
        _uiManager.UpdateAmmo(currentAmmino);
        _muzzle.SetActive(true);
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            Debug.Log("Hit: " + hitInfo.transform.name);
            GameObject hitmarker = Instantiate(_hitMarker, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject; //TypeCasting
            Destroy(hitmarker, 1f);

            //check for destruction
            Destructible crate = hitInfo.transform.GetComponent<Destructible>();
            if (crate != null)
            {
                crate.DestroyCrate();
            }
        }
    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;
        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);
        currentAmmino = maxAmmino;
        _uiManager.UpdateAmmo(currentAmmino);
        isReloading = false;
    }
    public void EnableWeapon()
    {
        _weapon.SetActive(true);
    }
}
