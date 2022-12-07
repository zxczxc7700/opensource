using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public float MaxHP = 100f;
    public float CurHP = 100f;
    public float speed = 5f;
    public int coin;
    public int totalcoin;
    public int kill;
    public int[] hasWeapon;
    public GameObject[] myWeapon;
    public GameObject shootEffect;
    public AudioClip audioShoot;
    public AudioClip audioCoin;
    public AudioClip audioJump;

    float hAxis;
    float vAxis;

    bool jDown;
    bool shDown;
    bool eDown;
    bool down1;
    bool down2;
    bool getHit;
    public bool isJump = false;
    public bool isDodge = false;

    int weaponIndex = 0;
    int hasWeaponCount = 1;
    int hasCount = 0;

    GameObject nearObject;
    GameObject equipWeapon;
    AudioSource audiosource;

    public WeaponManager CurWeapon;

    private float rotY;
    Vector3 moveVec;
    Rigidbody rigid;
    Animator anim;

    public float sensitiviy = 300f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audiosource = GetComponent<AudioSource>();
        rotY = transform.localRotation.eulerAngles.y;
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        CurHP = MaxHP;
        StartCoroutine(Dead());
        sensitiviy = GameObject.Find("MouseSensitivity").GetComponent<MouseSensitivity>().Sens * 300f;
        CurWeapon = GameObject.Find("Pistol1_N").GetComponent<WeaponManager>();
    }

    void Update()
    {
        if (playerdead)
            return;

        GetInput();
        Dir();
        Move();
        Turn();
        Jump();
        Dodge();
        Qskill();
        aim();
        Reload();
        GetItem();
        SwapWeapon();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        jDown = Input.GetButtonDown("Jump");
        shDown = Input.GetButtonDown("Dodge");
        eDown = Input.GetButtonDown("getItem");
        down1 = Input.GetButtonDown("Swap1");
        down2 = Input.GetButtonDown("Swap2");
    }

    void Dir()
    {
        if (Input.GetKey(KeyCode.W)) anim.SetBool("moveFWD", true);
        else if (Input.GetKey(KeyCode.S)) anim.SetBool("moveBWD", true);
        else if (Input.GetKey(KeyCode.A)) anim.SetBool("moveLEFT", true);
        else if (Input.GetKey(KeyCode.D)) anim.SetBool("moveRIGHT", true);

        if (Input.GetKeyUp(KeyCode.W)) anim.SetBool("moveFWD", false);
        if (Input.GetKeyUp(KeyCode.S)) anim.SetBool("moveBWD", false);
        if (Input.GetKeyUp(KeyCode.A)) anim.SetBool("moveLEFT", false);
        if (Input.GetKeyUp(KeyCode.D)) anim.SetBool("moveRIGHT", false);
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        //이동
        if (!isDodge)
        {
            transform.Translate(moveVec * speed * Time.deltaTime, Space.Self);
            //transform.position += moveVec * speed * Time.deltaTime;
        }

        //애니메이션
        anim.SetBool("isrun", moveVec != Vector3.zero);
    }


    void Turn()
    {
        rotY += Input.GetAxis("Mouse X") * sensitiviy * Time.deltaTime;

        Quaternion rot = Quaternion.Euler(0, rotY, 0);
        transform.rotation = rot;
        //transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        if (jDown && !isJump && !isDodge)
        {
            //점프
            rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
            isJump = true;

            //애니메이션
            anim.SetTrigger("dojump");
            anim.SetBool("isjump", true);
            PlaySound("Jump");
        }
    }

    bool Qdelay = false;
    void Qskill()
    {
        if (Input.GetKey(KeyCode.Q) && !Qdelay)
        {
            StartCoroutine(Qskillcool());
        }
    }
    IEnumerator Qskillcool()
    {
        Qdelay = true;
        if (CurHP + 10 >= MaxHP) CurHP = MaxHP;
        else CurHP += 10;
        yield return new WaitForSeconds(5.0f);
        Qdelay = false;
    }

    public bool DodgeDelay = false;
    IEnumerator DelayedDodge()
    {
        isDodge = true;
        isDodgeAndReload = true;
        DodgeDelay = true;
        Invoke("DodgeStart", 0f);

        anim.SetTrigger("dododge");
        anim.SetBool("reloadcancle", true);

        Invoke("DodgeEnd", 0.65f);
        Invoke("ReloadDelay", 1f);


        yield return new WaitForSeconds(5.0f);
        DodgeDelay = false;
    }

    bool isDodgeAndReload = false;

    void Dodge()
    {
        if (shDown && !isDodge && !isJump && !DodgeDelay)
        {
            StartCoroutine(DelayedDodge());
        }
    }

    void DodgeStart()
    {
        rigid.AddRelativeForce(moveVec * 2.4f * speed, ForceMode.Impulse);

        if (moveVec == Vector3.zero) //제자리 회피시 앞구르기
        {
            anim.SetBool("moveFWD", true);
            rigid.AddRelativeForce(Vector3.forward * 2.4f * speed, ForceMode.Impulse);
        }
    }

    void DodgeEnd()
    {
        rigid.velocity = Vector3.zero;
        anim.SetBool("moveFWD", false);
        anim.SetBool("reloadcancle", false); //추가됨
        isDodge = false;
    }

    void ReloadDelay()
    {
        isDodgeAndReload = false;
    }

    void GetItem()
    {
        if (eDown && nearObject != null)
        {
            if (nearObject.tag == "Weapon")
            {
                if (hasCount <= 2)
                {
                    Item item = nearObject.GetComponent<Item>();
                    hasWeapon[hasWeaponCount] = item.value;

                    hasCount++;
                    hasWeaponCount--;

                    if (hasWeaponCount != 0)
                        hasWeaponCount = 0;

                    Destroy(nearObject);
                }
                else
                {
                    Item item = nearObject.GetComponent<Item>();
                    hasWeapon[weaponIndex] = item.value;

                    Destroy(nearObject);
                }
            }
        }
    }

    void SwapWeapon()
    {
        if (down1 && !isDodge && !isreload)
            weaponIndex = 0;
        if (down2 && !isDodge && !isreload)
            weaponIndex = 1;

        if ((down1 || down2 && !isDodge && !isreload)) //장전중 무기변경불가로 바꿈
        {
            if (equipWeapon != null)
                equipWeapon.SetActive(false);
            equipWeapon = myWeapon[hasWeapon[weaponIndex]];
            equipWeapon.SetActive(true);//순서
            CurWeapon = equipWeapon.GetComponent<WeaponManager>();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJump = false;

            anim.SetBool("isjump", false);
        }
        else if(collision.gameObject.tag == "Coin")
        {
            coin++;
            totalcoin++;
            PlaySound("Coin");
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) //플레이어 피격
    {
        if (other.tag == "EnemyBullet")
        {
            if (!getHit)
            {
                EnemyBullet enemyBullet = other.GetComponent<EnemyBullet>();
                if (CurHP <= 0)
                    CurHP = 0f;
                CurHP -= enemyBullet.damage;
                
                StartCoroutine(OnDamage());
            }
            if (other.GetComponent<Rigidbody>() != null)
                Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = null;
    }

    public Transform muzzle;
    Vector3 MouseWorldPosition;
    public Transform bullet;

    bool isshot = false;

    //에임
    void aim()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if (Input.GetMouseButtonDown(0) && !isreload && !isDodge && CurWeapon.CurAmmo > 0) { anim.SetBool("isshot", true); isshot = true; }
        if (Input.GetMouseButtonUp(0)) { anim.SetBool("isshot", false); isshot = false; }

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            Vector3 point = ray.GetPoint(777f);
            Debug.DrawLine(ray.origin, point, Color.blue);
            Debug.DrawLine(MouseWorldPosition, muzzle.position, Color.red);
            MouseWorldPosition = raycastHit.point;

            //샷
            if (Input.GetMouseButton(0) && shotdelay == false && !isreload && !isDodge && CurWeapon.CurAmmo > 0)
            {
                if (!isshot) { anim.SetBool("isshot", true); isshot = true; } //타이밍이 엇갈려서 애니메이션 부적용되는 경우가 있어서
                StartCoroutine(shoot());
            }
        }
    }


    bool shotdelay = false;

    IEnumerator shoot()
    {
        shotdelay = true;
        MouseWorldPosition -= new Vector3(0.0f, 0.2f, 0.0f);    //에임이 살짝 위인거 같아서 y축에서 0.2 뺌
        Vector3 aimDir = (MouseWorldPosition - muzzle.position).normalized;
        shootEffect.SetActive(true);    //샷 이펙트
        Instantiate(bullet, muzzle.position, Quaternion.LookRotation(aimDir));
        CurWeapon.CurAmmo -= 1;
        
        yield return new WaitForSeconds(0.1f);  //샷 이펙트 보여주기 위해

        shootEffect.SetActive(false);
        PlaySound("Shoot");

        yield return new WaitForSeconds(CurWeapon.FireRate - 0.1f); //WeaponManager.FireRate

        if (CurWeapon.CurAmmo <= 0)
            anim.SetBool("isshot", false);
        
        shotdelay = false;

    }

    public bool isreload = false;
    void Reload()
    {
        if ((Input.GetKeyDown(KeyCode.R) || (Input.GetMouseButtonDown(0) && CurWeapon.CurAmmo <= 0)) && !isreload && !isDodge && CurWeapon.MaxAmmo != CurWeapon.CurAmmo)
        {
            isreload = true;
            anim.SetTrigger("reload");
            Invoke("ReloadEnd", 1f);
        }
    }

    void ReloadEnd()
    {
        if (!isDodgeAndReload) { CurWeapon.CurAmmo = CurWeapon.MaxAmmo; }
        else anim.SetBool("reloadcancle", false);
        isreload = false;
    }

    bool playerdead = false;

    IEnumerator OnDamage()
    {
        getHit = true;

        //Debug.Log("palyer gethit");

        yield return new WaitForSeconds(1f);

        getHit = false;
    }

    IEnumerator Dead()
    {
        while (true)
        {
            if (CurHP <= 0)
            {
                playerdead = true;
                anim.SetBool("isdead", true);
                anim.SetTrigger("dead");
                CurHP = 0f;
                yield return new WaitForSeconds(3f);
                SceneManager.LoadScene("OverScence");
            }
            yield return null;
        }
    }

    void PlaySound(string action)
    {
        switch(action)
        {
            case "Shoot":
                audiosource.clip = audioShoot;
                break;
            case "Jump":
                audiosource.clip = audioJump;
                break;
            case "Coin":
                audiosource.clip = audioCoin;
                break;
        }

        audiosource.Play();
    }
}
