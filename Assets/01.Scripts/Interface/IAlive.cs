using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAlive
{
    public bool IsAlive();   //사망한 상태인지 체크
    public void IsDie();     //사망 처리
}
