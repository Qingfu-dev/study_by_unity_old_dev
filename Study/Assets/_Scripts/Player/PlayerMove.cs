using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour //挂到任何物体上的脚本都继承它
{
    [Header("跳跃参数")]
    [SerializeField] private float _jumpForce = 8;
    [Header("跳跃检测")]
    [SerializeField] private Transform _groundCheck;//脚下子物体
    [SerializeField] private LayerMask _groundLayer;//Ground层
    [SerializeField] private float _checkDistance = 0.15f; // 射线检测长度





    [SerializeField] private float _moveSpeed = 5f; //玩家速度编辑器可调
    private Rigidbody2D _rb;
    private bool _jumpRequestecd; //跳跃请求标记
    private float _horizontalInput; //输入缓存：Update 读,FixUpdata用



    private void Awake()
    {
        //Awake 在物体激活瞬间执行一次，缓存组件引用
        _rb = GetComponent<Rigidbody2D>();
        //防抖三件套
        _rb.interpolation = RigidbodyInterpolation2D.Interpolate; //渲染平滑
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;//放碰撞倾斜

    }

    void Start()
    {




    }


    void Update()//跟随刷新率
    {
        _horizontalInput = Input.GetAxis("Horizontal");//根据按钮输入AD改变数1或-1

        if (Input.GetButtonDown("Jump"))//检测按钮按下
            _jumpRequestecd = true;



    }


    private void FixedUpdate()
    {
        //物理处理跳跃帧，在地面才消费跳跃请求，IsGrounded执行条件有跳跃请求才检查地面
        if (_jumpRequestecd && IsGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);//执行跳跃逻辑
            _jumpRequestecd = false;//取消跳跃请求
        }

        //改变物理状态，默认50次/s
        _rb.velocity = new Vector2(_horizontalInput * _moveSpeed, _rb.velocity.y);
    }

    private bool IsGrounded()//地面射线检测
    {
        //从脚“GroundCheck”下打一条射线命中“Ground”层才算着地
        RaycastHit2D hit = Physics2D.Raycast(
            _groundCheck.position, Vector2.down, _checkDistance, _groundLayer);
        Debug.DrawRay(_groundCheck.position, Vector2.down * _checkDistance,
            hit.collider != null ? Color.green : Color.red);
        return hit.collider != null;




    }
}
