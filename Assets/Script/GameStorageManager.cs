using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStorageManager
{
    private static GameResource _gameResource;
    public static GameResource gameResource
    {
        get
        {
            if (_gameResource == null)
            {
                StorageHandler storageHandler = new StorageHandler();
                _gameResource = (GameResource)storageHandler.LoadData("gameResource");
                if (_gameResource == null)
                {
                    _gameResource = new GameResource();
                }
            }
            return _gameResource;
        }
        set { _gameResource = value; }
    }
    public static void Save()
    {
        StorageHandler storageHandler = new StorageHandler();
        storageHandler.SaveData(_gameResource, "gameResource");
    }
}
