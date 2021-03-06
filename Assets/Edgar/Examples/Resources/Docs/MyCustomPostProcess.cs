using UnityEngine;

namespace Edgar.Unity.Examples.Resources
{
    [CreateAssetMenu(menuName = "Edgar/Examples/Docs/My custom post process", fileName = "MyCustomPostProcess")]
    public class MyCustomPostProcess : DungeonGeneratorPostProcessBase
    {
        public override void Run(GeneratedLevel level, LevelDescription levelDescription)
        {
            GameObject.Find("Player").transform.position = level.GetRoomInstances()[0].Position;
        }
    }
}