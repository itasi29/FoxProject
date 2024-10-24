using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePos : MonoBehaviour
{
    // �v���C���[.
    GameObject _player;
    // �n���h���̍������݌�.
    [SerializeField] GameObject[] _handleWall;
    // �n���h���̉�]�t���[���𑪂�.
    private int _rotaFrameHandle;
    // Start is called before the first frame update.
    void Start()
    {
        _rotaFrameHandle = 0;
        // �v���C���[���擾����.
        _player = GameObject.Find("3DPlayer");
    }
    // �n���h�����v���C���[�̈ʒu�Ɉړ�.
    public void HandlePosIsPlayer()
    {
        Vector3 pos;
        pos.x = _player.transform.position.x;
        pos.y = _player.transform.position.y + 3.3f;
        pos.z = _player.transform.position.z;
        this.transform.position = pos;
    }

    // �n���h�����������݈ʒu�Ɉړ�.
    public void HandlePosIsHandleWall(int no)
    {
        Vector3 pos;
        pos.x = _handleWall[no].transform.position.x;
        pos.y = _handleWall[no].transform.position.y;
        pos.z = _handleWall[no].transform.position.z - 0.4f;
        this.transform.position = pos;
    }

    // �n���h���̉�].
    public void Rota(float speed)
    {
        this.transform.Rotate(0, 0, speed);
        _rotaFrameHandle++;
    }
    // �n���h���̉�]���I��������.
    public bool IsGetRotaTimeOver(int frame)
    {
        if(_rotaFrameHandle > frame)
        {
            return true;
        }

        return false;
    }
}
