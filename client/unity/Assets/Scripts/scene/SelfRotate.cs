using BattleCity;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Tooltip("��ת�ٶȣ���/�룩")]
    public float rotationSpeed = 10f;

    public float changedSpeed;

    private void Start()
    {
        changedSpeed = rotationSpeed;
        TypeEventSystem.Global.Register<SpeedUpEvent>(e =>
        {
            SpeedUp();
        });
        TypeEventSystem.Global.Register<SpeedDownEvent>(e =>
        {
            SpeedDown();
        });
        TypeEventSystem.Global.Register<StopEvent>(e =>
        {
            Stop();
        });
        TypeEventSystem.Global.Register<ResumeEvent>(e =>
        {
            Resume();
        });
        TypeEventSystem.Global.Register<BattleEndEvent>(e =>
        {
            Resume();
        });
    }

    void Update()
    {
        // ��Y����ת���ɸ�����Ҫ������ת�ᣩ
        transform.Rotate(Vector3.up, changedSpeed * Time.deltaTime);
    }

    void SpeedUp()
    {
        changedSpeed *= 2;
    }

    void SpeedDown()
    {
        changedSpeed /= 2;
    }

    void Stop()
    {
        changedSpeed = 0;
    }

    void Resume()
    {
        changedSpeed = rotationSpeed;
    }
}
