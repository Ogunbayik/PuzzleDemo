using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private const int ACTIVE_PRIORITY = 10;
    private const int INACTIVE_PRIORITY = 1;

    public static CameraManager Instance;

    [SerializeField] private CinemachineVirtualCameraBase cameraGame;
    [SerializeField] private CinemachineVirtualCameraBase cameraHelper;

    public GameObject testPrefab;

    [SerializeField] private Vector3 lookOffset;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private void Awake()
    {
        #region Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        #endregion
    }
    void Start()
    {
        cameraGame.Priority = ACTIVE_PRIORITY;
        cameraHelper.Priority = INACTIVE_PRIORITY;

        initialPosition = cameraGame.transform.position;
        initialRotation = cameraGame.transform.rotation;
    }
    public void ToggleGameCamera()
    {
        bool isActiveGameCamera = cameraGame.Priority > cameraHelper.Priority;

        if(isActiveGameCamera)
        {
            cameraHelper.Priority = ACTIVE_PRIORITY;
            cameraGame.Priority = INACTIVE_PRIORITY;
        }
        else
        {
            cameraHelper.Priority = INACTIVE_PRIORITY;
            cameraGame.Priority = ACTIVE_PRIORITY;
        }
    }

    public void SetHelperCameraPosition(Vector3 position)
    {
        cameraHelper.transform.position = position + lookOffset;
    }
}
