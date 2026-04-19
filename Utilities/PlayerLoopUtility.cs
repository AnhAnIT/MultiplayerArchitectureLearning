using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using UnityEngine.LowLevel;
namespace MultiplayCore
{
    public static partial class PlayerLoopUtility
    {
        //Public static methods
        public static void SetDefaultPlayerLoopSystem()
        {
            PlayerLoop.SetPlayerLoop(PlayerLoop.GetDefaultPlayerLoop());
        }



        public static bool HasPlayerLoopSystem(Type PlayerLoopSystemType)
        {
            if (PlayerLoopSystemType == null)
                return false;
            PlayerLoopSystem loopSystem = PlayerLoop.GetCurrentPlayerLoop();

            for (int i = 0, sysSystemCount = loopSystem.subSystemList.Length; i < sysSystemCount; ++i)
            {
                PlayerLoopSystem subSystem = loopSystem.subSystemList[i];
                List<PlayerLoopSystem> subSubSystems = new List<PlayerLoopSystem>(subSystem.subSystemList);
                for (int j = 0; j < subSubSystems.Count; ++j)
                {
                    if (subSubSystems[j].type == PlayerLoopSystemType)
                        return true;
                }
            }
            return false;
        }

        public static bool AddPlayerLoopSystem(Type playerLoopSystemType, Type targetLoopSystem, PlayerLoopSystem.UpdateFunction updateFunction, int position = -1)
        {
            if (playerLoopSystemType == null || targetLoopSystem == null || updateFunction == null)
            {
                return false;
            }
            PlayerLoopSystem loopSystem = PlayerLoop.GetCurrentPlayerLoop();
            for (int i = 0, subSystemCount = loopSystem.subSystemList.Length; i < subSystemCount; ++i)
            {
                PlayerLoopSystem subSystem = loopSystem.subSystemList[i];
                if (subSystem.type == targetLoopSystem)
                {
                    PlayerLoopSystem targetSystem = new PlayerLoopSystem();
                    targetSystem.type = playerLoopSystemType;
                    targetSystem.updateDelegate = updateFunction;

                    List<PlayerLoopSystem> subSubSystems = new List<PlayerLoopSystem>(subSystem.subSystemList);
                    if (position >= 0)
                    {
                        if (position > subSubSystems.Count)
                            throw new ArgumentOutOfRangeException(nameof(position));
                        subSubSystems.Insert(position, targetSystem);

                    }
                    else
                    {
                        subSubSystems.Add(targetSystem);
                    }
                    subSystem.subSystemList = subSubSystems.ToArray();
                    loopSystem.subSystemList[i] = subSystem;
                    PlayerLoop.SetPlayerLoop(loopSystem);
                    return true;


                }
            }
            Debug.LogErrorFormat("Failed to add player loop system {0} to :{1}", playerLoopSystemType.FullName, targetLoopSystem.FullName);
            return false;

        }


        public static bool AddPlayerLoopSystem(Type playerLoopSystemType , Type targetSubSystemType , PlayerLoopSystem.UpdateFunction updateFunctionBefore, PlayerLoopSystem.UpdateFunction updateFunctionAfter)
        {
            if (playerLoopSystemType == null || targetSubSystemType == null || (updateFunctionBefore == null && updateFunctionAfter == null))
                return false;

            PlayerLoopSystem loopSystem = PlayerLoop.GetCurrentPlayerLoop();
            for (int i = 0, subSystemCount = loopSystem.subSystemList.Length; i < subSystemCount; ++i)
            {
                PlayerLoopSystem subSystem = loopSystem.subSystemList[i];

                for(int j = 0 , subSubSystemCount = subSystem.subSystemList.Length; j < subSubSystemCount; ++j)
                {
                    PlayerLoopSystem subSubSystem = subSystem.subSystemList[j];

                    if (subSubSystem.type == targetSubSystemType)
                    {
                        List<PlayerLoopSystem> subSubSystems = new List<PlayerLoopSystem>(subSubSystem.subSystemList);
                        int currentPosition = j;
                        if (updateFunctionBefore != null)
                        {
                            PlayerLoopSystem playerLoopSytem = new PlayerLoopSystem();
                            playerLoopSytem.type = targetSubSystemType;
                            playerLoopSytem.updateDelegate = updateFunctionBefore;

                            subSubSystems.Insert(currentPosition, playerLoopSytem);
                            ++currentPosition;
                        }
                        if(updateFunctionAfter != null)
                        {
                            ++currentPosition;
                            PlayerLoopSystem playerLoopSystem= new PlayerLoopSystem();
                            playerLoopSystem.type = playerLoopSystemType;
                            playerLoopSystem.updateDelegate = updateFunctionAfter;

                            subSubSystems.Insert(currentPosition, playerLoopSystem);
                        }

                        subSystem.subSystemList = subSubSystems.ToArray();
                        loopSystem.subSystemList[i] = subSystem;
                        PlayerLoop.SetPlayerLoop(loopSystem);
                        return true;

                    }

                }

            }
            Debug.LogErrorFormat("Failed to add player loop system :{0}", playerLoopSystemType.FullName);
            return false;

        }

        public static bool RemovePlayerLoopSystems(Type playerLoopSystemType)
        {
            if (playerLoopSystemType == null)
                return false;
            bool setplayerLoop = false;
            PlayerLoopSystem loopSystem = PlayerLoop.GetCurrentPlayerLoop();
            for (int i = 0, subSystemCount = loopSystem.subSystemList.Length; i < subSystemCount; ++i)
            {
                PlayerLoopSystem subSystem = loopSystem.subSystemList[i];
                if (subSystem.subSystemList == null)
                    continue;
                bool removedFromSubSystem = false;

                List<PlayerLoopSystem> subSubSystems = new List<PlayerLoopSystem>(subSystem.subSystemList);
                for (int j = subSubSystems.Count - 1; j >= 0; --j)
                {
                    if (subSubSystems[j].type == playerLoopSystemType)
                    {
                        subSubSystems.RemoveAt(j);
                        removedFromSubSystem = true;
                    }
                }
                if (removedFromSubSystem == true)
                {
                    setplayerLoop = true;
                    subSystem.subSystemList = subSubSystems.ToArray();
                    loopSystem.subSystemList[i] = subSystem;
                }
            }
            if (setplayerLoop == true)
            {
                PlayerLoop.SetPlayerLoop(loopSystem);
            }

            return setplayerLoop;
        }

        public static void DumpPlayerLoopSystems()
        {
            Debug.LogWarning("====================================================================================================");
            PlayerLoopSystem loopSystem = PlayerLoop.GetCurrentPlayerLoop();
            for (int i = 0, subSystemCount = loopSystem.subSystemList.Length; i < subSystemCount; ++i)
            {
                PlayerLoopSystem subSystem = loopSystem.subSystemList[i];
                Debug.LogWarning(subSystem.type.FullName);
                List<PlayerLoopSystem> subSubSystems = new List<PlayerLoopSystem>(subSystem.subSystemList);
                for (int j = 0; j < subSubSystems.Count; ++j)
                {
                    Debug.Log(" " + subSubSystems[j].type.FullName);
                }
            }
            Debug.LogWarning("====================================================================================================");
        }
    }
}