using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;

/*命名
 * 类型、方法用帕斯卡命名法
 * 变量用驼峰命名法
 */

public class NewMonoBehaviourScript : MonoBehaviour
{
    //在代码的最上方都要“申明变量”
    //格式是：类型名称+具体的变量的名字（自己取）
    //类型名称遵循帕斯卡命名法，变量命遵循驼峰命名法

    [SerializeField] private InputAction thrust;
    //InputAction 是 Unity 的 Input System 中的一个类，用于定义输入行为（例如按键、鼠标点击等）。

    [SerializeField] private InputAction rotation;

    [SerializeField] private float thrustStrength = 100f;
    //这是基于数字的一个变量，所以要用float

    [SerializeField] private float rotationStrength = 100f;

    private Rigidbody rb;
    //Rigidbody：是 Unity 中的一个组件，用于为游戏对象添加物理特性（如重力、碰撞、力等）。


    //“()” 用于表示：这是一个方法,加了这个()，Unity才知道这个是在调用一个预定俗成的方法；
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    //表示：在开始的时候要调取当前的rigidboby，并且让变量rb的初始值=这个当前的值；
    //GetComponent<T>()：是Unity提供的方法，用于获取当前游戏对象上挂载的指定类型的组件;

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    //FixedUpdate:固定时间间隔调用，处理和“物理引擎”相关的内容；Update：固定帧率调用
    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }
    //这里的方法是将下面自己写的代码提取成了一个新的方法，目的是让代码整齐一点


    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        }
    }
    //AddRelativeForce（）是Rigidbody 的一个方法，用于在对象的局部坐标系中施加力；
    //Vector3 是一个结构体，表示一个物体的三维向量；Vector3.up=(0,1,0)

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput < 0)
        {
            ApplyRotation(rotationStrength); // 调用方法的地方，括号里，是方法的“实际参数”
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationStrength);
        }
        //else if 的语法表示和if中的情况相互排斥，不能同时发生
    }

    private void ApplyRotation(float rotationThisFrame) // 括号里，是方法的“形式参数”
    {
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        //在这个代码中，“.”可以理解为“的”，而括号中就是填具体的参数，括号内的逻辑决定了物体如何旋转
    }
}