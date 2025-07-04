using System.Collections.Generic;
using UnityEngine;

public class PlayGameContext 
{    
    public ClassData playClassData;
    public int playLevel;

    /// <summary>
    /// 직업 데이터, 재화, 플레이레벨
    /// </summary>
    /// <param name="classData"></param>    
    /// <param name="newPlayLevel"></param>
    public PlayGameContext(ClassData classData, int newPlayLevel)
    {
        playClassData = classData;
        this.playLevel = newPlayLevel;
    }
}
