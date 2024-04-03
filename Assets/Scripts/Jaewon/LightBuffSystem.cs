using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KinematicCharacterController;
using KinematicCharacterController.Examples;

public enum State{
    Red = 0,
    Green = 1,
    Blue = 2
}
public class LightBuffSystem : MonoBehaviour
{
    Transform tr;
    Rigidbody rb;
    ExampleCharacterController exampleCharacterController;
    PlayerManager playerManager;

    private List<GameObject> _lightList = new List<GameObject>();

    public bool isEnemy;
    public bool isAlly;
    private bool isOnArea;
    Color Neutrality = new Color(1, 1, 1, 1);

    //�ε��� 0�� �Ʊ�, �ε��� 1�� �����Դϴ�. �ε��� 2�� �߸�(�Ʊ��� ������ �ƴ� ����)
    private int[] _stateCheck = new int[] { 0, 0, 0 };

    //public Image stateCheck;

    #region ������(SerializeField)
    [SerializeField] private float _defaultVelocity = 6;
    [SerializeField] private float _buffVelocity = 10;
    [SerializeField] private float _debuffVelocity = 3;
    [SerializeField] private float _defaultJump = 10;
    [SerializeField] private float _buffJump = 15;
    [SerializeField] private float _debuffJump = 6;
    [SerializeField] private float _buffTime=1.0f;
    #endregion
    private void Start()
    {
        exampleCharacterController = GetComponent<ExampleCharacterController>();
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        playerManager = this.gameObject.GetComponent<PlayerManager>();
        Debug.Log(playerManager.teamColor);
    }
    #region LateUpdate
    //RGB���ÿ� ����ϴϱ� ���� ���̾ȵǴ� �κ��� �־ ���� �� ������ ������ ������ �޵��� �Ͽ����ϴ�.
    //�����, ���� �޼ҵ�� �ϴ� �Ѵ� ��������.
    //���� ����Ʈ���� ���� ���Ͽ� ���� ���� �������¸� ���մϴ�.
    private void LateUpdate()
    {
        Buff();
        DeBuff();
    }
    #endregion LateUpdate
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            //���� ���� �÷��� ����� �÷��� ���մϴ�.
            if (other.CompareTag("Red"))
            {
                _stateCheck[(int)State.Red]++;
                Debug.Log("���� ����");
            }
            else if (other.CompareTag("Blue"))
            {
                _stateCheck[(int)State.Blue]++;
                Debug.Log("�Ķ� ����");
            }
            else if (other.CompareTag("Green"))
            {
                _stateCheck[(int)State.Green]++;
            }
            GetIsOnArea();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            //���� ���� �÷��� ����� �÷��� ���մϴ�.
            if (other.CompareTag("Red"))
            {
                _stateCheck[(int)State.Red]--;
            }
            else if (other.CompareTag("Green"))
            {
                _stateCheck[(int)State.Green]--;
            }
            else if (other.CompareTag("Blue"))
            {
                _stateCheck[(int)State.Blue]--;
            }
            GetIsOnArea();
        }
    }

    private Color GetState()
    {
        Color _state;
        if(_stateCheck[(int)State.Red] != 0 && _stateCheck[(int)State.Green] == 0 && _stateCheck[(int)State.Blue] == 0)
        {
            _state = Color.red;
        }
        else if (_stateCheck[(int)State.Red] == 0 && _stateCheck[(int)State.Green] != 0 && _stateCheck[(int)State.Blue] == 0)
        {
            _state = Color.green;
        }
        else if (_stateCheck[(int)State.Red] == 0 && _stateCheck[(int)State.Green] == 0 && _stateCheck[(int)State.Blue] != 0)
        {
            _state = Color.blue;
        }
        else
        {
            _state = Neutrality;
        }
        return _state;
    }
    private void GetIsOnArea()
    {
        if (_stateCheck == new int[] { 0, 0, 0 })
        {
            isOnArea = false;
        }
        else isOnArea = true;
    }
    #region Buff
    private void Buff()
    {
        if (playerManager.teamColor == GetState() && isOnArea)
        {
            StartCoroutine(EndBuff());
        }
        else if(playerManager.teamColor == GetState() && !isOnArea)
        {
            Debug.Log("���� ��");
        }
    }
    private IEnumerator EndBuff()
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            exampleCharacterController.MaxStableMoveSpeed = _buffVelocity;
            exampleCharacterController.JumpUpSpeed = _buffJump;
            if (time >= _buffTime)
            {
                exampleCharacterController.MaxStableMoveSpeed = _defaultVelocity;
                exampleCharacterController.JumpUpSpeed = _defaultJump;
                time = 0;
                break;
            }
            yield return null;
        }
    }
    #endregion Buff
    #region DeBuff
    private void DeBuff()
    {
        if (playerManager.teamColor != GetState() && isOnArea)
        {
            if(GetState() == Neutrality)
            {
                return;
            }
            StartCoroutine(EndDeBuff());
        }
        else if (playerManager.teamColor != GetState() && !isOnArea)
        {
            return;        
        }
    }
    private IEnumerator EndDeBuff()
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            exampleCharacterController.MaxStableMoveSpeed = _debuffVelocity;
            exampleCharacterController.JumpUpSpeed = _debuffJump;
            if (time >= _buffTime)
            {
                exampleCharacterController.MaxStableMoveSpeed = _defaultVelocity;
                exampleCharacterController.JumpUpSpeed = _defaultJump;
                time = 0;
                break;
            }
            yield return null;
        }
    }
    #endregion Buff
}
