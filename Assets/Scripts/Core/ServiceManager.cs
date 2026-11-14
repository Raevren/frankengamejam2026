using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core {
    public class ServiceManager {
        public static ServiceManager Instance { get; private set; }
        public static bool AllRegistered { get; private set; }

        readonly Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        internal ServiceManager() {
            Instance ??= this;
        }
        
        public void RegisterService<T>() where T : IService {
            if (_services.ContainsKey(typeof(T))) {
                Debug.LogWarning($"[ServiceManager] {typeof(T).Name} is already registered");
                return;
            }
            IService service = Activator.CreateInstance<T>();
            _services.Add(typeof(T), service);
            service.OnRegister();
        }

        public void UnregisterService(IService service) {
            if (!_services.ContainsKey(service.GetType())) {
                Debug.LogWarning($"[ServiceManager] {service.GetType().Name} is not registered");
                return;
            }
            IService serviceInstance = _services[service.GetType()];
            serviceInstance.OnUnregister();
            _services.Remove(service.GetType());
        }
        
        public T GetService<T>() where T : IService {
            if (_services.TryGetValue(typeof(T), out IService service)) {
                return (T)service;
            }
            throw new NullReferenceException($"[ServiceManager] {typeof(T).Name} is not registered");
        }

        internal void OnAllRegistered() {
            foreach (var service in _services) {
                service.Value.OnAllRegistered();
            }
            AllRegistered = true;
        }
    }
}