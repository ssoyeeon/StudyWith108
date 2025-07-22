using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 옵저버 인터페이스 - 이벤트를 받을 수 있는 객체들이 구현해야 함
public interface ILevelUpObserver
{
    void OnPlayerLevelUp(int newLevel);
}
