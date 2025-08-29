using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ResourceManager : DontDestroySingleton<ResourceManager>
{
    Dictionary<int, Sprite> itemSprites = new Dictionary<int, Sprite>();

    public async Task<Sprite> LoadToSprite(int id)
    {
        // sprite 변수선언
        Sprite newSprite = null;

        // dictionary에서 해당 key(id)에 따른 value 리턴 후 메소드 종료.
        if(itemSprites.TryGetValue(id, out newSprite))
        {
            return newSprite;
        }      
        
        // 만약 위의 조건을 만족하지 못했다면 스프라이트 로드
        newSprite = await SpriteLoad.LoadToSprite(id);


        // 만약 스프라이트를 로드했지만 null이라면
        if (newSprite == null)
        {
            // 캐싱하지 않고 null 리턴
            Debug.LogError($"Sprite 로드 실패: ID {id}");
            return null;
        }

        // 스프라이트 로드 성공적이라면 캐싱
        itemSprites[id] = newSprite;

        // 캐싱 후 리턴
        return newSprite;
    }

}
