using System.Collections.Generic;
using UnityEngine;

public class PlayGameContext 
{    
    public ClassData playClassData;
    public int playLevel;
    public Difficulty difficulty;
    /// <summary>
    /// 직업 데이터, 재화, 플레이레벨
    /// </summary>
    /// <param name="classData"></param>    
    /// <param name="newPlayLevel"></param>
    public PlayGameContext(ClassData classData, int newPlayLevel )
    {
        playClassData = classData;
        playLevel = newPlayLevel;        
    }

    public void SetDifficulty(Difficulty playDiff)
    {
        this.difficulty = playDiff;
    }

    public void SetClass(ClassData playClass)
    {
        this.playClassData = playClass;
    }
    
}
