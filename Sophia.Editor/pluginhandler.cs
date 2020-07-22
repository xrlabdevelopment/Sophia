using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Sophia.Editor
{
    public class PluginHandler
    {
        //--------------------------------------------------------------------------------------
        // Constants
        public static readonly string UNITY_PLUGIN_LOCATION = Application.dataPath + "\\Plugins\\";
        public static readonly string SOPHIA_PLUGIN_LOCATION = "sophia\\";

        private static readonly float PLUGIN_FLUSH_INTERVAL = 4.0f;
        private static readonly float PLUGIN_VALIDATE_INTERVAL = 3600.0f;

        //--------------------------------------------------------------------------------------
        // Fields
        private bool refresh_assets;

        private Guid loader_refresh_timer_id;
        private Guid validator_timer_id;

        private TimerManager timer_manager;

        private PluginLoader plugin_loader;
        private PluginValidator plugin_validator;

        private string install_location;

        //--------------------------------------------------------------------------------------
        public PluginHandler()
        {
            refresh_assets = false;
        }

        //--------------------------------------------------------------------------------------
        public bool initialize(string installLocation)
        {
            if(!Directory.Exists(UNITY_PLUGIN_LOCATION + SOPHIA_PLUGIN_LOCATION))
            {
                Debug.LogWarning("Please move \"Sophia\" plugins inside a \"Sophia\" subdirectory folder. eg: \"Assets/Plugins/sophia/sophia_core.dll\"");
                return false;
            }

            install_location = installLocation;

#if DEBUG
            PluginType plugin_type = PluginType.DEBUG;
#else
            PluginType plugin_type = PluginType.RELEASE;
#endif

            if (!Directory.Exists(install_location))
            {
                UnityEngine.Debug.LogWarning("Install location not found: " + install_location);
                UnityEngine.Debug.Log("Installing loaded plugins ...");

                Directory.CreateDirectory(install_location);

                foreach (string file_path in Directory.GetFiles(UNITY_PLUGIN_LOCATION + SOPHIA_PLUGIN_LOCATION))
                {
                    if (plugin_type == PluginType.DEBUG)
                    {
                        if (!IO.Helpers.isDebugFile(file_path))
                            continue;
                    }

                    Debug.Log("SOURCE: " + file_path);
                    Debug.Log("DEST: " + Path.Combine(installLocation, System.IO.Path.GetFileName(file_path)));

                    File.Copy(file_path, Path.Combine(installLocation, System.IO.Path.GetFileName(file_path)));
                }

                UnityEngine.Debug.Log("Installation finished!");
                foreach (string file_path in Directory.GetFiles(installLocation))
                {
                    if (plugin_type == PluginType.DEBUG)
                    {
                        if (!IO.Helpers.isDebugFile(file_path))
                            continue;

                        UnityEngine.Debug.Log("Installed file: " + file_path);
                    }
                }
            }

            plugin_validator = new PluginValidator(UNITY_PLUGIN_LOCATION + SOPHIA_PLUGIN_LOCATION, plugin_type);
            plugin_loader = new PluginLoader(install_location, UNITY_PLUGIN_LOCATION + SOPHIA_PLUGIN_LOCATION, plugin_type);
            plugin_loader.onFinishedLoading += onPluginsLoaded;
            plugin_loader.onFileDeleted += onPluginRemoved;

            timer_manager = new TimerManager();

            loader_refresh_timer_id = timer_manager.createTimer<CountDownTimer>(createPluginLoaderTimerInfo());
            validator_timer_id = timer_manager.createTimer<CountDownTimer>(createPluginValidatorTimerInfo());

            return true;
}

        //--------------------------------------------------------------------------------------
        public void update(float dTime)
        {
            timer_manager.update(dTime);

            if (refresh_assets)
            {
                plugin_validator.validate();

                AssetDatabase.Refresh();

                refresh_assets = false;
            }
        }

        //--------------------------------------------------------------------------------------
        public void stop()
        {
            plugin_loader.onFinishedLoading -= onPluginsLoaded;
            plugin_loader.onFileDeleted -= onPluginRemoved;

            refresh_assets = false;

            timer_manager.stopTimer(loader_refresh_timer_id);
            timer_manager.stopTimer(validator_timer_id);
        }

        //--------------------------------------------------------------------------------------
        private void flushPluginLoader(Guid timerId)
        {
            if (!plugin_loader.IsDirty || timerId != loader_refresh_timer_id)
                return;

            plugin_loader.flush();

            UnityEngine.Debug.Log("Flushing plugins ...");
        }
        //--------------------------------------------------------------------------------------
        private void validatePluginValidator(Guid timerId)
        {
            if (timerId != validator_timer_id)
                return;

            plugin_validator.validate();

            UnityEngine.Debug.Log("Validating plugins ...");
        }

        //--------------------------------------------------------------------------------------
        private void onPluginsLoaded()
        {
            refresh_assets = true;

            UnityEngine.Debug.Log("Plugins Loaded!");
        }
        //--------------------------------------------------------------------------------------
        private void onPluginRemoved()
        {
            refresh_assets = true;

            UnityEngine.Debug.Log("Plugin Removed!");
        }

        //--------------------------------------------------------------------------------------
        private TimerCreationInfo createPluginLoaderTimerInfo()
        {
            TimerCreationInfo info = new TimerCreationInfo();
            info.finished_delegate += flushPluginLoader;
            info.finish_behaviour = TimerFinishedBehaviour.RESET_START_ON_FINSHED;
            info.start_on_creation = true;
            info.start_time = PLUGIN_FLUSH_INTERVAL;

            return info;
        }
        //--------------------------------------------------------------------------------------
        private TimerCreationInfo createPluginValidatorTimerInfo()
        {
            TimerCreationInfo info = new TimerCreationInfo();
            info.finished_delegate += validatePluginValidator;
            info.finish_behaviour = TimerFinishedBehaviour.RESET_START_ON_FINSHED;
            info.start_on_creation = true;
            info.start_time = PLUGIN_VALIDATE_INTERVAL;

            return info;
        }
    }
}
