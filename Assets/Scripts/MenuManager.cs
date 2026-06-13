using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _ipInput;
    

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene("Game",LoadSceneMode.Single);
    }

    public void StartClient()
    {
        UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();

        transport.ConnectionData.Address = _ipInput.text;

        NetworkManager.Singleton.StartClient();
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
