using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public VariableJoystick Joystick;
    public float MoveSpeed = 5f;
    public GameObject Bullet;
    public Transform FirePoint;
    public HealthBar healthBar;
    
    #region  Private Feilds

    private int Health = 100;
    private bool isLoosingHealth;
    private Rigidbody body;
    private Vector3 MoveDir;
    private GameManager gm;
    private Vector3 playerPos;
    private InputManager input;
    private float YRotation;
    private float smooth = 15f;
    
    #endregion
    
    #region Static Instance

    private static Player instance;

    public static Player Instance
    {
        get => instance;
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        gm = Manager.Instance.manager;
        playerPos = gameObject.transform.position;
        input = Manager.Instance.input;
        healthBar.SetMaxHealth(Health);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!gm.IsGameEnded && input.SwipeLeft || input.SwipeRight || input.isSwipping)
        {
            YRotation = input.Dist.x;
            RotatePlayer();
        }*/
        if(Health <= 0)
            gm.GameLose();
    }

    void RotatePlayer()
    {
        float Y = YRotation * smooth * Time.fixedDeltaTime;
        transform.Rotate(new Vector3(0f,Y));
    }
    
    void FixedUpdate()
    {
        if(!gm.IsGameEnded)
        {
            MoveDir = transform.forward * Joystick.Vertical + transform.right * Joystick.Horizontal;
            if (Mathf.Abs(MoveDir.x) > 0.1 || Mathf.Abs(MoveDir.z) > 0.1)
                body.AddForce(MoveDir * MoveSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            else
                body.velocity = Vector3.zero;
        }
    }

    public void FireBullet()
    {
        if(gm.IsGameEnded)
            return;
        Quaternion rot = transform.rotation;
        GameObject curBullet = Instantiate(Bullet, FirePoint.position,rot);
    }

    void OnCollisionEnter(Collision col)
    {
        if(gm.IsGameEnded)
            return;
        
        if (col.gameObject.tag.Equals("Enemy"))
        {
            isLoosingHealth = true;
            StartCoroutine(LoseHealth());
            //gm.GameLose();
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            isLoosingHealth = false;
        }
    }
    IEnumerator LoseHealth()
    {
        while (isLoosingHealth)
        {
            Health -= 20;
            healthBar.SetSlider(Health);
            yield return new WaitForSeconds(1f);
            yield return null;
        }
    }
    
    public void Reset()
    {
        transform.position = playerPos;
        Health = 100;
        isLoosingHealth = false;
        healthBar.Reset();
    }
}
