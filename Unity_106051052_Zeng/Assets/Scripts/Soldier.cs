using UnityEngine;

public class Soldier : MonoBehaviour
{
    #region 欄位區域
     
    [Header("移動速度")]
    [Range(1, 2000)]
    public int speed = 10;             // 整數 1, 9999, -100
    [Header("旋轉速度"), Tooltip("Soldier的旋轉速度"), Range(1.5f, 200f)]
    public float turn = 20.5f;         // 浮點數
    [Header("是否完成任務")]
    public bool mission;               // 布林值 true false
    [Header("玩家名稱")]
    public string _name = "Soldier";      // 字串 ""
    public Transform tran;
    public Rigidbody rig;
    public Animator ani;
    public AudioSource aud;

    public AudioClip soundBark;
    #endregion

    [Header("檢物品位置")]
    public Rigidbody rigCatch;

    private void Update()
    {
        Turn();
        Run();
        Bark();
        Catch();
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.name == "Santa" && ani.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {
            
            Physics.IgnoreCollision(other, GetComponent<Collider>());
            other.GetComponent<HingeJoint>().connectedBody = rigCatch;
        }


    }

    #region 方法區域
    /// <summary>
    /// 跑步
    /// </summary>
    private void Run()
    {
        // 如果 動畫 為 撿東西 就 跳出
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Shoot")) return;

        float v = Input.GetAxis("Vertical");        // W 上 1、S 下 -1、沒按 0
        // rig.AddForce(0, 0, speed * v);           // 世界座標
        // tran.right   區域座標 X 軸
        // tran.up      區域座標 Y 軸
        // tran.forward 區域座標 Z 軸
        // Time.deltaTime 當下裝置一幀的時間
        rig.AddForce(tran.forward * speed * v * Time.deltaTime);     // 區域座標

        ani.SetBool("走路開關", v != 0);
    }

    /// <summary>
    /// 旋轉
    /// </summary>
    private void Turn()
    {
        float h = Input.GetAxis("Horizontal");    // A 左 -1、D 右 1、沒按 0
        tran.Rotate(0, turn * h * Time.deltaTime, 0);
    }

    /// <summary>
    /// 亂叫
    /// </summary>
    private void Bark()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 按下空白鍵拍翅膀
            ani.SetTrigger("死去觸發器");
            // 音源.播放一次音效(音效，音量)
            aud.PlayOneShot(soundBark, 0.6f);
        }
    }

    /// <summary>
    /// 撿東西
    /// </summary>
    private void Catch()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // 按下左鍵撿東西
            ani.SetTrigger("開槍觸發器");
        }
    }

    /// <summary>
    /// 檢視任務
    /// </summary>
    private void Task()
    {

    }
    #endregion
}
