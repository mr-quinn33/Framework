#if UNITY_NETCODE_GAMEOBJECTS
using Unity.Netcode;
#endif

namespace Framework.Scripts.Netcode.GameObjects
{
#if UNITY_NETCODE_GAMEOBJECTS
    public abstract class AbstractNetworkBehaviour : NetworkBehaviour
    {
        public override void OnNetworkSpawn()
        {
            OnNetworkSpawnServer(IsServer);
            OnNetworkSpawnClient(IsClient);
            OnNetworkSpawnOwner(IsOwner);
        }

        public override void OnNetworkDespawn()
        {
            OnNetworkDespawnOwner(IsOwner);
            OnNetworkDespawnClient(IsClient);
            OnNetworkDespawnServer(IsServer);
        }

        private void OnNetworkSpawnServer(bool isServer)
        {
            if (!isServer) return;
            OnNetworkSpawnServer();
            NetworkManager.NetworkTickSystem.Tick += OnTickServer;
        }

        private void OnNetworkDespawnServer(bool isServer)
        {
            if (!isServer) return;
            NetworkManager.NetworkTickSystem.Tick -= OnTickServer;
            OnNetworkDespawnServer();
        }

        private void OnNetworkSpawnClient(bool isClient)
        {
            if (!isClient) return;
            OnNetworkSpawnClient();
            NetworkManager.NetworkTickSystem.Tick += OnTickClient;
        }

        private void OnNetworkDespawnClient(bool isClient)
        {
            if (!isClient) return;
            NetworkManager.NetworkTickSystem.Tick -= OnTickClient;
            OnNetworkDespawnClient();
        }

        private void OnNetworkSpawnOwner(bool isOwner)
        {
            if (!isOwner) return;
            OnNetworkSpawnOwner();
            NetworkManager.NetworkTickSystem.Tick += OnTickOwner;
        }

        private void OnNetworkDespawnOwner(bool isOwner)
        {
            if (!isOwner) return;
            NetworkManager.NetworkTickSystem.Tick -= OnTickOwner;
            OnNetworkDespawnOwner();
        }

        protected abstract void OnNetworkSpawnServer();

        protected abstract void OnNetworkDespawnServer();

        protected abstract void OnNetworkSpawnClient();

        protected abstract void OnNetworkDespawnClient();

        protected abstract void OnNetworkSpawnOwner();

        protected abstract void OnNetworkDespawnOwner();

        protected abstract void OnTickServer();

        protected abstract void OnTickClient();
        
        protected abstract void OnTickOwner();
    }
#endif
}