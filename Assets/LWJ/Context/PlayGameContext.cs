using System.Collections.Generic;
using UnityEngine;

public class PlayGameContext 
{    
    ClassData playClassData;    
    int playLevel;

    /// <summary>
    /// ���� ������, ��ȭ, �÷��̷���
    /// </summary>
    /// <param name="classData"></param>    
    /// <param name="newPlayLevel"></param>
    public PlayGameContext(ClassData classData, int newPlayLevel)
    {
        playClassData = classData;        
        this.playLevel = newPlayLevel;
    }
}
