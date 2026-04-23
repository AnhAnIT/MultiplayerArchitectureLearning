using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;


namespace MultiplayCore
{
    public interface IGlobalService
    {
        void Initialize();
        void Tick();
        void Deinitialize();
    }

    public static class Global
    {
        //Public members
        public static GlobalSettings Settings { get; private set; }

        public static RuntimeSettings RuntimeSettings { get; private set; }

        public static PlayerService PlayerService { get; private set; }

        public static Networking Networking { get; private set; }

        public static MultiplayerManager MultiplayerManager { get; private set; }

        //Private static members 
        private static bool _isInitialized;
        private static List<IGlobalService> _services;

        //Public methods
        public static void Quit()
        {
            Deinitialize();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
     Application.Quit();
#endif

        }
        //Private methods
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void InitializeSubSystem()
        {
            if (Application.isBatchMode == true)
            {
                AudioListener.volume = 0f;
                PlayerLoopUtility.RemovePlayerLoopSystems(typeof(PostLateUpdate.UpdateAudio));
            }
#if UNITY_EDITOR
            if (Application.isPlaying == false)
                return;
#endif
            if (PlayerLoopUtility.HasPlayerLoopSystem(typeof(Global)) == false)
            {
                PlayerLoopUtility.AddPlayerLoopSystem(typeof(Global), typeof(Update.ScriptRunBehaviourUpdate), BeforeUpdate, AfterUpdate);
            }

            Application.quitting -= OnApplicationQuit;
            Application.quitting += OnApplicationQuit;

            _isInitialized = true;
        }
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeBeforeSceneLoad()
        {

        }
    }
}