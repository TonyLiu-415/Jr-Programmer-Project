using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class OptimUnit : MonoBehaviour
{
    private Vector3 currentVelocity;
    private float timeToVelocityChange;
    private float currentAngularVelocity;
    private float timeToAngularVelocityChange;

    private Vector3 areaSize;

    public void SetAreaSize(Vector3 size)
    {
        areaSize = size;
    }

    private void Start()
    {
        PickNewVelocity();
        PickNewVelocityChangeTime();
        PickNewAngularVelocity();
        PickNewAngularVelocityChangeTime();
    }

    // Update is called once per frame
    void Update()
    {//测试性能
        Profiler.BeginSample("Handling Time"); // begin profiling a piece of code with a custom label
        HandleTime();
        Profiler.EndSample(); // ends the current profiling sample

        Profiler.BeginSample("Rotating"); // begin profiling
        var t = transform;
        float xRotation = currentAngularVelocity * Time.deltaTime;
        float zRotation = currentAngularVelocity * Time.deltaTime;

        if (transform.position.x > 0)
            xRotation *= -1;
        if (transform.position.z < 0)
            zRotation *= -1;

        transform.Rotate(xRotation, 0, zRotation);
        Profiler.EndSample(); // end profiling

        Profiler.BeginSample("Moving"); // begin profiling

        Move();

        Profiler.EndSample(); // end profiling

        Profiler.BeginSample("Boundary Check"); // begin profiling

        //check if we are moving away from the zone and invert velocity if this is the case
        if (transform.position.x > areaSize.x && currentVelocity.x > 0
            || (transform.position.x < -areaSize.x && currentVelocity.x < 0))
        {
            currentVelocity.x *= -1;
            PickNewVelocityChangeTime(); //we pick a new change time as we changed velocity
        }

        if (transform.position.z > areaSize.z && currentVelocity.z > 0
            || (transform.position.z < -areaSize.z && currentVelocity.z < 0))
        {
            currentVelocity.z *= -1;
            PickNewVelocityChangeTime(); //we pick a new change time as we changed velocity
        }
        Profiler.EndSample(); // end profiling
    }


    private void PickNewVelocity()
    {
        currentVelocity = Random.insideUnitSphere;
        currentVelocity.y = 0;
        currentVelocity *= 10.0f;
    }

    private void PickNewAngularVelocity()
    {
        currentAngularVelocity = Random.Range(-180.0f, 180.0f);
    }

    private void PickNewVelocityChangeTime()
    {
        timeToVelocityChange = Random.Range(2.0f, 5.0f);
    }

    private void PickNewAngularVelocityChangeTime()
    {
        timeToAngularVelocityChange = Random.Range(2.0f, 5.0f);
    }

    void Move()
    {
        //Vector3 position = transform.position;

        //float distanceToCenter = Vector3.Distance(Vector3.zero, position);
        //float speed = 0.5f + distanceToCenter / areaSize.magnitude;

        //int steps = Random.Range(1000, 2000);
        //float increment = Time.deltaTime / steps;
        //for (int i = 0; i < steps; ++i)
        //{
        //    position += currentVelocity * increment * speed;
        //}

        //transform.position = position;

        //Random.Range(1000, 2000) 生成一个介于 1000 到 2000 之间的随机整数，作为循环的步数 steps。
        //Time.deltaTime 表示上一帧到当前帧所经过的时间。increment = Time.deltaTime / steps 计算每一步的时间增量。
        //在 for 循环中，position += currentVelocity * increment * speed 不断更新 position 的值。
        //currentVelocity 是物体的当前速度向量，乘以时间增量 increment 和速度 speed 后，得到每一步物体移动的位移，然后累加到 position 上。通过多次循环，模拟出物体在一帧内的连续移动。
        //根本没必要这么计算！画蛇添足

        float distanceToCenter = Vector3.Distance(Vector3.zero, transform.position);
        float speed = 0.5f + distanceToCenter / areaSize.magnitude;
        transform.position = transform.position + currentVelocity * Time.deltaTime* speed;
        //这与上面逻辑没有区别
        //currentVelocity 是一个 Vector3 类型的私有成员变量，其用途在于表示 OptimUnit 这个游戏对象的当前移动速度和方向。
        //currentVelocity 最初是通过 PickNewVelocity 方法随机生成的，之后会在边界检查时根据物体是否超出边界来修改其分量，
        //并且会在 timeToVelocityChange 小于 0 时重新随机生成。
    }

    private void HandleTime()
    {
        timeToVelocityChange -= Time.deltaTime;
        if (timeToVelocityChange < 0)
        {
            PickNewVelocity();
            PickNewVelocityChangeTime();
        }

        timeToAngularVelocityChange -= Time.deltaTime;
        if (timeToAngularVelocityChange < 0)
        {
            PickNewAngularVelocity();
            PickNewAngularVelocityChangeTime();
        }
    }
}
