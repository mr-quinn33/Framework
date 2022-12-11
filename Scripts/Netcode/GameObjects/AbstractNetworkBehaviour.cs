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
            OnNetworkSpawnClient(IsClient, IsOwner);
        }

        public override void OnNetworkDespawn()
        {
            OnNetworkDespawnServer(IsServer);
            OnNetworkDespawnClient(IsClient, IsOwner);
        }

        private void OnNetworkSpawnServer(bool isServer)
        {
            if (!isServer) return;
            OnNetworkSpawnServer();
            NetworkManager.NetworkTickSystem.Tick += ServerOnTick;
        }

        private void OnNetworkDespawnServer(bool isServer)
        {
            if (!isServer) return;
            NetworkManager.NetworkTickSystem.Tick -= ServerOnTick;
            OnNetworkDespawnServer();
        }

        private void OnNetworkSpawnClient(bool isClient, bool isOwner)
        {
            if (!isClient) return;
            OnNetworkSpawnClient();
            NetworkManager.NetworkTickSystem.Tick += ClientOnTick;
            OnNetworkSpawnOwner(isOwner);
        }

        private void OnNetworkDespawnClient(bool isClient, bool isOwner)
        {
            if (!isClient) return;
            OnNetworkDespawnOwner(isOwner);
            NetworkManager.NetworkTickSystem.Tick -= ClientOnTick;
            OnNetworkDespawnClient();
        }

        private void OnNetworkSpawnOwner(bool isOwner)
        {
            if (!isOwner) return;
            OnNetworkSpawnOwner();
            NetworkManager.NetworkTickSystem.Tick += OwnerOnTick;
        }

        private void OnNetworkDespawnOwner(bool isOwner)
        {
            if (!isOwner) return;
            NetworkManager.NetworkTickSystem.Tick -= OwnerOnTick;
            OnNetworkDespawnOwner();
        }

        protected abstract void OnNetworkSpawnServer();

        protected abstract void OnNetworkDespawnServer();

        protected abstract void OnNetworkSpawnClient();

        protected abstract void OnNetworkDespawnClient();

        protected abstract void OnNetworkSpawnOwner();

        protected abstract void OnNetworkDespawnOwner();

        protected abstract void ServerOnTick();

        protected abstract void ClientOnTick();

        protected abstract void OwnerOnTick();
    }
#endif
}