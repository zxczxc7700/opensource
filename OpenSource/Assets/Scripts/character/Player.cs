using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MaxHP = 100f;
    public float CurHP = 100f;
    public float speed = 5f;
    public int coin;
    public GameObject[] myWeapon;
    public int[] hasWeapon;

    float hAxis;
    float vAxis;

    bool jDown;
    bool shDown;
    bool eDown;
    bool down1;
    bool down2;
    bool getHit;
    bool isJump = false;
    bool isDodge = false;

    int weaponIndex = 0;
    int hasWeaponCount = 0;
    int hasCount = 0;

    GameObject nearObject;
    GameObject equipWeapon;

    //�÷��̾�ݽ���

    WeaponManager CurWeapon;
    //�������ι���
    //�������� �迭
    //�÷��̾�ݳ�

    //pistol���� ��� �ִϸ��̼��� �޶�� �� �� �����ǹǷ� �ش������ ���� ����
    //������ ź��������� �޶���ϹǷ� �ش���� ���� ���� ����

    private float rotY;
    Vector3 moveVec;
    Rigidbody rigid;
    Animator anim;

    void Awake()
    {
        rotY = transform.localRotation.eulerAngles.y;
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        CurHP = MaxHP;
        StartCoroutine(Dead());

        //����ҷ�����
        CurWeapon = GameObject.Find("Rifle 1").GetComponent<WeaponManager>();
    }

    void Update()
    {
        if (playerdead) return;

        GetInput();
        Dir();
        Move();
        Turn();
        Jump();
        Dodge();
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

        //�̵�
        if (!isDodge)
        {
            transform.Translate(moveVec * speed * Time.deltaTime, Space.Self);
            //transform.position += moveVec * speed * Time.deltaTime;
        }

        //�ִϸ��̼�
        anim.SetBool("isrun", moveVec != Vector3.zero);
    }

    void Turn()
    {
        rotY += Input.GetAxis("Mouse X") * 300f * Time.deltaTime;

        Quaternion rot = Quaternion.Euler(0, rotY, 0);
        transform.rotation = rot;
        //transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        if (jDown && !isJump && !isDodge)
        {
            //����
            rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
            isJump = true;

            //�ִϸ��̼�
            anim.SetTrigger("dojump");
            anim.SetBool("isjump", true);
        }
    }

    public bool DodgeDelay = false;
    IEnumerator DelayedDodge() //�� �ڷ�ƾ
    {
        isDodge = true;
        isDodgeAndReload = true;
        DodgeDelay = true;
        Invoke("DodgeStart", 0f);

        //�ִϸ��̼�
        anim.SetTrigger("dododge");
        anim.SetBool("reloadcancle", true);

        Invoke("DodgeEnd", 0.65f);
        Invoke("ReloadDelay", 1f);


        yield return new WaitForSeconds(5); /*�ӽ÷� 5��*/
        DodgeDelay = false;
    }

    bool isDodgeAndReload = false; // �߰��� ����

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

        if (moveVec == Vector3.zero) //���ڸ� ȸ�ǽ� �ձ�����
        {
            anim.SetBool("moveFWD", true);
            rigid.AddRelativeForce(Vector3.forward * 2.4f * speed, ForceMode.Impulse);
        }
    }

    void DodgeEnd()
    {
        rigid.velocity = Vector3.zero;
        anim.SetBool("moveFWD", false);
        anim.SetBool("reloadcancle", false); //�߰���
        isDodge = false;
    }

    void ReloadDelay() //�߰��� �Լ�
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
                    hasWeaponCount++;

                    if (hasWeaponCount >= 1)
                        hasWeaponCount = 1;

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
        if (down1 && !isDodge)  //jump, dodge�� �ൿ�߿� ���� ���ǵ� �ֱ�
            weaponIndex = 0;
        if (down2 && !isDodge)  //jump, dodge�� �ൿ�߿� ���� ���ǵ� �ֱ�
            weaponIndex = 1;

        if ((down1 || down2 && !isDodge))    //jump, dodge�� �ൿ�߿� ���� ���ǵ� �ֱ�
        {
            if (equipWeapon != null)
                equipWeapon.SetActive(false);
            equipWeapon = myWeapon[hasWeapon[weaponIndex]];
            equipWeapon.SetActive(true);
        }

        //ȸ�ǵ��߿� �Ұ����ؾ�
        //�ִϸ��̼�.����
        //������ ����.SetActive(false)
        //������ ���� = ����迭[�ε���(��������)]
        //������ ����.SetActive(true)
    }


    private void OnCollisionEnter(Collision collision) //�ٴ�����Ȯ��
    {
        if (collision.gameObject.tag == "Floor") //�ٴ�(Floor)
        {
            isJump = false;

            //�ִϸ��̼�
            anim.SetBool("isjump", false);
        }
    }

    private void OnTriggerEnter(Collider other) //�÷��̾� �ǰ�
    {
        if (other.tag == "EnemyBullet")
        {
            if (!getHit)
            {
                //Debug.Log("EnemyBullet");
                EnemyBullet enemyBullet = other.GetComponent<EnemyBullet>();
                if (CurHP <= 0)
                    CurHP = 0f;
                CurHP -= 10;    //���� �������� �����ϱ�
                StartCoroutine(OnDamage());
            }
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

    //����
    void aim()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if (Input.GetMouseButtonDown(0) && !isreload && !isDodge && CurWeapon.CurAmmo > 0) { anim.SetBool("isshot", true); isshot = true; }
        if (Input.GetMouseButtonUp(0)) { anim.SetBool("isshot", false); isshot = false; }

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            Vector3 point = ray.GetPoint(777f);
            Debug.DrawLine(ray.origin, point, Color.red);
            MouseWorldPosition = raycastHit.point;

            //��
            if (Input.GetMouseButton(0) && shotdelay == false && !isreload && !isDodge && CurWeapon.CurAmmo > 0)
            {
                if (!isshot) { anim.SetBool("isshot", true); isshot = true; } //Ÿ�̹��� �������� �ִϸ��̼� ������Ǵ� ��찡 �־
                StartCoroutine(shoot());
            }
        }
    }


    bool shotdelay = false;

    IEnumerator shoot()
    {
        shotdelay = true;
        Vector3 aimDir = (MouseWorldPosition - muzzle.position).normalized;
        Instantiate(bullet, muzzle.position, Quaternion.LookRotation(aimDir));
        CurWeapon.CurAmmo -= 1;
        yield return new WaitForSeconds(CurWeapon.FireRate); //WeaponManager.FireRate

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
                //�÷��̾� ��� ���� ó�� ���� �����ʿ�
            }
            yield return null;
        }
    }
}