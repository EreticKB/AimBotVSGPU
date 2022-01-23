using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private bool _isTiltEnabled = false;
    public bool GetTiltStatus()
    {
        SaveHandler.LoadProperty(Game.IndexTiltActivation, out int state, 1);
        return state == 1 ? true : false;
    }
    public bl_Joystick JoyStick; //���� ��� ��������� �������� �� �������
    private Strafe _strafe;
    

    private List<IFollower> _followers = new List<IFollower>();//������ ��� �������� �������, ��� �� �������������, �.�. ������ ��������� ����� ���������� � ������ � ������ ������������ ������ ��� 
    private int _followersIndex = -1;//����, ����� ������� "����� ������" �� ���������� ��������.
    private Transform _transform;
    public float StrafeSpeed;
    public float FieldRadius;
    public Material[] Materials; //������ ������������ � ������������ � ����� ������ ����������, ����������� �������������� "��������" �������.
    private Vector2[] _lerp; //������ ��� �������� �������� Strafe._lerp. ������������ ��� ���������� ��������� "����� ������" � ��������� �� ������.
    private Loop _loop;
    private void Awake()
    {
        for (int i = 0; i < Materials.Length; i++)
        {
            BendMaterial(Materials[i], 0f, 0f, Vector2.zero); ;
        }
        _transform = transform;        
        SaveHandler.LoadProperty(Game.IndexControlSensitivity, out float sensitivity, 0.2f);
        _strafe = new Strafe(_transform, StrafeSpeed, FieldRadius, JoyStick, sensitivity);
        _loop = new Loop(0, 100, 10);
        _lerp = new Vector2[100];
    }

    private void Update()
    {
        for (int i = 0; i <= _followersIndex; i++) _followers[i].TakeMyPosition(_transform.position);

        int offSet;
        if (!_isTiltEnabled)
        {
            if (Input.anyKey) _lerp[_loop.Next(out offSet)] = _strafe.UpdateFromKey();
            else return;
        }
        else
        {
            _lerp[_loop.Next(out offSet)] = _strafe.UpdateFromTilting(Game.TiltOffSet);
        }
        for (int i = 0; i < Materials.Length; i++)
        {
            BendMaterial(Materials[i], 0f, 0.0005f, _lerp[offSet]); ;
        }

    }

    public void AddFollower(IFollower follower)
    {
        _followers.Add(follower);
        _followersIndex++;
    }
    public void RemoveFollower(IFollower follower)
    {
        _followers.Remove(follower);
        _followersIndex--;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{getFollowerByName("WarpTunnel")} crash...");
    }

    private void enableFollowByName(string name, bool set)
    {
        _followers[getFollowerByName(name)].IsFollow = set;
    }

    private int getFollowerByName(string name)
    {
        foreach (IFollower follower in _followers)
        {
            if (follower.Name.Equals(name)) return _followers.IndexOf(follower);

        }
        return -1;
    }

    private void BendMaterial(Material material, float minLimit, float maxLimit, Vector2 bend)
    {
        BendingShaderController.TrySetHorizontalBending(material, -Mathf.LerpUnclamped(minLimit, maxLimit, bend.x));
        BendingShaderController.TrySetVerticalBending(material, -Mathf.LerpUnclamped(minLimit, maxLimit, bend.y));
    }
}
