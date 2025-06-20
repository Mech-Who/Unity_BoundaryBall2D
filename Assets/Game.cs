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
    
    private float _leftMoveSpeed = 0, _rightMoveSpeed = 0; // 挡板移动速度 
    
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
