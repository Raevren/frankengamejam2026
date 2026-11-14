namespace Core
{
    public interface IService {
        public void OnRegister();
        public void OnUnregister();
        public void OnAllRegistered();
    }
}
