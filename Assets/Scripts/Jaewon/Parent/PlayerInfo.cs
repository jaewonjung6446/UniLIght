using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StatInfo
{
    _playerHp = 0,
    _playerSpd,
    _playerDfn,
    _playerAtk
}

public abstract class PlayerInfo : MonoBehaviour
{
    #region ������
    protected GameObject _player;
    #endregion

    //�÷��̾��� ������ ��ųʸ��� �����մϴ�. �������� �ռ� �������� �������� �ε���, float�� �������� ���Դϴ�. �⺻���� ���� 100�Դϴ�.
    public Dictionary<int,float> _playerStat =  new Dictionary<int, float>();

    //���� playerPrefab�� �÷��̾�� ���������� �Ұ����� �����ϴ� �޼ҵ�
    protected bool IsPlayablePrefab(GameObject playerPrefab) 
    {
        return true;
    }

    //GetPlayablePrefab�� ���� ���̶��, ���� �� �����տ� ī�޶� �ڽ����� �����մϴ�.

    //��Ʈ��ũ ����� ���� �÷��̾� ������ ����� �޼ҵ带 �߻�ȭ�Ͽ����ϴ�.
    #region ���� ����
    protected void InitPlayerDic()
    {
        _playerStat.Add(0, 10);
        _playerStat.Add(1, 100);
        _playerStat.Add(2, 100);
        _playerStat.Add(3, 100);
    }
    protected void SetStat(int stat, float a)
    {
        if (_playerStat.ContainsKey(stat))
        {
             _playerStat[stat] = a;
        }
    }
    #endregion
}