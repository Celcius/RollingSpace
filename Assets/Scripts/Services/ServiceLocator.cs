using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public interface ServiceEntity
{
    string serviceIdentifier();

}

public class ServiceLocator
{

    static ServiceLocator _sharedLocator = null;
    private Dictionary<string, ServiceEntity> _services;

    public static ServiceLocator getServiceLocator()
    {
        if (_sharedLocator == null)
        {
            _sharedLocator = new ServiceLocator();

        }

        return _sharedLocator;
    }

    private ServiceLocator()
    {
        _services = new Dictionary<string, ServiceEntity>();
    }

    public void registerService(ServiceEntity entity)
    {
        string identifier = entity.serviceIdentifier();

        _services[identifier] = entity;

        Debug.Log("[ServiceLocator] Registering " + identifier);
    }

    public ServiceEntity getService(string identifier)
    {
        return _services[identifier];
    }

}
