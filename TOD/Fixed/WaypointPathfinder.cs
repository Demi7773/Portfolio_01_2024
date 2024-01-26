using UnityEditor;
using UnityEngine;

public class WaypointPathfinder : MonoBehaviour
{
    [SerializeField] Transform[] points;
    public int numberOfChildren;
    bool isPlaying = false;

    private void Start()
    {
        isPlaying = true;

        foreach(Transform point in points)
        {
            point.gameObject.SetActive(false);
        }
    }

    private void OnValidate()
    {
        AddDebugPoints();
    }

    private void OnDrawGizmos()
    {
        if (!isPlaying)
        {
            DrawDebugPathLine();
            this.gameObject.GetComponentInParent<EnemyBehaviour>().OnValidate();

            foreach (Transform point in points)
            {
                Gizmos.color = Color.white;
                if (point != null)
                {
#if UNITY_EDITOR
                    var icon = EditorGUIUtility.IconContent("sv_icon_dot8_pix16_gizmo");
                    EditorGUIUtility.SetIconForObject(point.gameObject, (Texture2D)icon.image);
#endif
                }
            }
        }
    }

    void AddDebugPoints()
    {
        numberOfChildren = transform.childCount;
        points = new Transform[numberOfChildren];

        for (int i = 0; i < numberOfChildren; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }

    void DrawDebugPathLine()
    {
        for (int i = 0; i < points.Length; i++)
        {

            if ((i + 1) == points.Length)
            {
                Debug.DrawLine(points[i].position, points[0].position, Color.white);
                return;
            }

            Debug.DrawLine(points[i].position, points[i + 1].position, Color.white);
        }
    }

}
