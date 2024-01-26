using UnityEngine;

public class DontDestroyTest : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
