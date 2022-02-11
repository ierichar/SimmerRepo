using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simmer.SceneManagement
{
    [CreateAssetMenu(fileName = "New SceneData"
        , menuName = "Scenes/SceneData")]

    public class SceneData : ScriptableObject
    {
        public string sceneName;
    }
}
