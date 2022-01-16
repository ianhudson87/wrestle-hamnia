using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;

public class SteamLobby : MonoBehaviour
{
    [SerializeField] private GameObject buttons;
    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEntered;
    private NetworkManager network_manager;
    private const string host_address_key = "host address";

    void Start()
    {
        network_manager = GetComponent<NetworkManager>();

        if (!SteamManager.Initialized){ return; }

        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
    }

    public void HostLobby(){
        buttons.SetActive(false);

        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, network_manager.maxConnections);

    }

    void OnLobbyCreated(LobbyCreated_t callback){
        if(callback.m_eResult != EResult.k_EResultOK){
            buttons.SetActive(true);
            return;
        }

        network_manager.StartHost();

        SteamMatchmaking.SetLobbyData(
            new CSteamID(callback.m_ulSteamIDLobby),
            host_address_key,
            SteamUser.GetSteamID().ToString());
    }

    void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback){
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    void OnLobbyEntered(LobbyEnter_t callback){
        NetworkManagerWIT.singleton.SetLocalUsername(SteamFriends.GetPersonaName());
        // print("username is" + SteamFriends.GetPersonaName());
        if (NetworkServer.active){ return; } // if is host

        string host_address = SteamMatchmaking.GetLobbyData(
            new CSteamID(callback.m_ulSteamIDLobby),
            host_address_key);


        network_manager.networkAddress = host_address;
        network_manager.StartClient();

        buttons.SetActive(false);

    }
}
