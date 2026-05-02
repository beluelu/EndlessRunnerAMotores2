using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.RemoteConfig;
using System.Threading.Tasks;

public class RemoteConfigManager : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }

    public static RemoteConfigManager Instance;

    public float playerSpeed;
    public float jumpForce;
    public float gravity;
    public bool spawnZombie;
    public int coinsAmount;

    async void Awake()
    {
        if (Instance == null)
            Instance = this;

        await InitializeServices();
    }

    async Task InitializeServices()
    {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteConfig;
        RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
    }

    void ApplyRemoteConfig(ConfigResponse response)
    {
        playerSpeed = RemoteConfigService.Instance.appConfig.GetFloat("player_speed", 10f);
        jumpForce = RemoteConfigService.Instance.appConfig.GetFloat("jump_force", 7f);
        gravity = RemoteConfigService.Instance.appConfig.GetFloat("gravity", -20f);
        spawnZombie = RemoteConfigService.Instance.appConfig.GetBool("spawn_zombie", true);
        coinsAmount = RemoteConfigService.Instance.appConfig.GetInt("coins_amount", 5);

        Debug.Log("REMOTE CONFIG APLICADO");
    }
}
