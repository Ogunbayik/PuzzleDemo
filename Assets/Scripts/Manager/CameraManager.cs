using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public static CameraManager Instance;

    [Header("Game Cameras")]
    [SerializeField] private CinemachineVirtualCameraBase cameraGame;
    [SerializeField] private CinemachineVirtualCameraBase cameraHelper;
    [Header("Offset Settings")]
    [SerializeField] private Vector3 lookOffset;
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
        cameraGame.Priority = Consts.CameraPriority.ACTIVE_PRIORITY;
        cameraHelper.Priority = Consts.CameraPriority.INACTIVE_PRIORITY;
    }
    public void ToggleGameCamera()
    {
        bool isActiveGameCamera = cameraGame.Priority > cameraHelper.Priority;

        if (isActiveGameCamera)
        {
            cameraHelper.Priority = Consts.CameraPriority.ACTIVE_PRIORITY;
            cameraGame.Priority = Consts.CameraPriority.INACTIVE_PRIORITY;
        }
        else
        {
            cameraHelper.Priority = Consts.CameraPriority.INACTIVE_PRIORITY;
            cameraGame.Priority = Consts.CameraPriority.ACTIVE_PRIORITY;
        }
    }
    public void SetHelperCameraPosition(Vector3 position)
    {
        cameraHelper.transform.position = position + lookOffset;
    }
}
