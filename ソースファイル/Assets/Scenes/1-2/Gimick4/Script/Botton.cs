using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botton : MonoBehaviour
{
    // �����Ă邩�ǂ�����n��.
    // �g���K�[����p.
    private bool _isTrigger { get; set; }
    // ���݃{�^���������Ă��邩�ǂ���
    private bool _isTriggerActive = false;

    // Start is called before the first frame update.
    void Start()
    {
        // �����Ă��Ȃ�.
        _isTrigger = false;
    }

    // Update is called once per frame.
    void Update()
    {
        // �����Ă��Ȃ�.
        _isTrigger = false;
        // B������
        if (Input.GetKey(KeyCode.JoystickButton1)) // B.
        {
            // �����Ă��Ȃ��ꍇ��.
            if (!_isTriggerActive)
            {
                // ����������.
                _isTriggerActive = true;
                _isTrigger = true;
            }
        }
        else
        {
            // �{�^���������Ă��Ȃ�������.
            _isTriggerActive = false;
        }
    }
    // �������u�Ԃ̔����n��.
    public bool GetButtonB()
    {
        return _isTrigger;
    }
}
