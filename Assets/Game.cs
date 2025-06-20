#define DUBUG

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    // 关联所有游戏对象
    public Text leftScoreText;
    public Text rightScoreText;

    private GameObject _leftBlock;
    private GameObject _rightBlock;
    private GameObject _ball;

    private int _leftScore = 0, _rightScore = 0; // 比分
    
    // 挡板
    private float _leftMoveSpeed = 0, _rightMoveSpeed = 0; // 挡板移动速度 
    
    // 单位与像素坐标比例为1：100
    private float _pixelScale = 100.0f;

    //小球移动速度和方向
    private float _ballSpeed = 10.0f;
    private float _ballSpeedAngle = 0.25f * Mathf.PI;  //初始角度为pi/4
    
    // 碰撞时设置为true，防止重复触发事件
    private bool _isLeftBlockHit = false;
    private bool _isRightBlockHit = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; // 关闭垂直同步后，设置帧率
        // 有效提高游戏流畅度，降低输入延迟等

        _leftBlock = GameObject.Find("LeftBlock");
        _rightBlock = GameObject.Find("RightBlock");

        _ball = GameObject.Find("Ball");
    }

    // Update is called once per frame
    void Update()
    {
        ControlPanel(ref _leftBlock, ref _leftMoveSpeed, KeyCode.W, KeyCode.S);
        ControlPanel(ref _rightBlock, ref _rightMoveSpeed, KeyCode.UpArrow, KeyCode.DownArrow);
        CalculateBallMove();
        CalculateBallCollision();
        BallHitPanel();
    }

    private bool IsHitPanel(Vector2 ballPosition, Vector3 blockPosition, float pixelScale)
    {
        return ballPosition.x < blockPosition.x + 10 / pixelScale
               && ballPosition.x > blockPosition.x - 10 / pixelScale
               && ballPosition.y < blockPosition.y + 50 / pixelScale
               && ballPosition.y > blockPosition.y - 50 / pixelScale;
    }
    
    private void BallHitPanel()
    {
        Vector2 ballPosition = _ball.transform.position;
        Vector2 leftBlockPosition = _leftBlock.transform.position;
        Vector2 rightBlockPosition = _rightBlock.transform.position;
        float speedY = Mathf.Cos(_ballSpeedAngle) * _ballSpeed/_pixelScale;
        float speedX = Mathf.Sin(_ballSpeedAngle) * _ballSpeed/_pixelScale;
        
        //左边挡板碰撞
        //挡板的宽高分别是 20、100像素，所以计算碰撞区域时，按照宽高一半进行计算
        if (IsHitPanel(ballPosition, leftBlockPosition, _pixelScale))
        {
#if DEBUG
            Debug.Log("Hit left block!");
#endif
            if(!_isLeftBlockHit)
            {
                if (speedX < 0)
                {
                    _ballSpeedAngle = (leftBlockPosition.y - ballPosition.y) / 50.0f * _pixelScale + Mathf.PI / 2.0f;
                }
                _isLeftBlockHit = true;
            }
            else
            {
                _isLeftBlockHit = false;
            }
        }

        
        //挡板的宽高分别是 20、100像素，所以计算碰撞区域时，按照宽高一半进行计算
        if (IsHitPanel(ballPosition, rightBlockPosition, _pixelScale))
        {
#if DEBUG
            Debug.Log("Hit right block!");
#endif
            if (!_isRightBlockHit)
            {
                if (speedX > 0)
                {
                    _ballSpeedAngle = Mathf.PI * 3 / 2 - (rightBlockPosition.y - ballPosition.y) / 50.0f * _pixelScale;
                }
                _isRightBlockHit = true;
            }
        }
        else
        {
            _isRightBlockHit = false;
        }
    }
    
    private void CalculateBallMove()
    {
        //计算小球移动速度——极坐标
        float speedY = Mathf.Cos(_ballSpeedAngle) * _ballSpeed/_pixelScale;
        float speedX = Mathf.Sin(_ballSpeedAngle) * _ballSpeed/_pixelScale;

        //小球移动
        Vector2 ballPosition = _ball.transform.position;
        ballPosition.x += speedX;
        ballPosition.y += speedY;
        _ball.transform.position = ballPosition;
    }

    private void CalculateBallCollision()
    {
        Vector2 ballPosition = _ball.transform.position;
        //小球碰撞到左右边缘后反弹
        if (ballPosition.x > 6.6f )
        { // 右侧
            _ballSpeedAngle = -_ballSpeedAngle;
            _leftScore++;
            leftScoreText.text = _leftScore.ToString();
        } else if (ballPosition.x < -6.6f )
        { // 左侧
            _ballSpeedAngle = -_ballSpeedAngle;
            _rightScore++;
            rightScoreText.text = _rightScore.ToString();
        }

        if (ballPosition.y > 5.0f)
        { // 上方
            // 反弹
            _ballSpeedAngle = Mathf.PI - _ballSpeedAngle;
            // 上下联通
            // ballPosition.y = -5.0f;
            // _ball.transform.position = ballPosition;
        } else if (ballPosition.y < -5.0f)
        { // 下方
            // 反弹
            _ballSpeedAngle = Mathf.PI - _ballSpeedAngle;
            // 上下联通
            // ballPosition.y = 5.0f;
            // _ball.transform.position = ballPosition;
        }
    }

    private void ControlPanel(ref GameObject block, ref float moveSpeed, KeyCode upKeyCode, KeyCode downKeyCode)
    {
        // 控制左边挡板
        if (Input.GetKeyDown(upKeyCode))
        {
#if DEBUG
            Debug.Log("按下W");
#endif
            moveSpeed = 0.08f;
        }else if (Input.GetKeyDown(downKeyCode))
        {
#if DEBUG
            Debug.Log("按下S");
#endif
            moveSpeed = -0.08f;
        }else if (Input.GetKeyUp(upKeyCode))
        {
#if DEBUG
            Debug.Log("松开W");
#endif
            moveSpeed = 0;
        }
        else if (Input.GetKeyUp(downKeyCode))
        {
#if DEBUG
            Debug.Log("松开S");
#endif
            moveSpeed = 0;
        }

        // 计算左挡板位置
        Vector2 leftBlockPosition = block.transform.position;
        leftBlockPosition.y += moveSpeed;
        // 防止左挡板移动到屏幕外
        leftBlockPosition.y = Math.Clamp(leftBlockPosition.y, -5.0f, 5.0f);
        // 更新左挡板位置
        block.transform.position  = leftBlockPosition;
    }
}
