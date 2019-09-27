using UnityEngine;
using System;

namespace Bundles.SettingsManagement
{
    /// <summary>
    /// This is the parent of every settings and is used as reference, don't override it to create your own settings (override <see cref="Settings{TSettings}"/> instead)
    /// </summary>
    public abstract class Settings : ScriptableObject {
        #region Constants
        /// <summary>
        /// The resource folder for default settings
        /// </summary>
        public const string DEFAULT_FOLDER_PATH = "Settings/Default";
        #endregion
    }

    /// <summary>
    /// Override this class to create you own settings
    /// </summary>
    /// <typeparam name="TSettings">The type of the settings</typeparam>
    public abstract class Settings<TSettings> : Settings where TSettings : Settings
    {
        #region Private Fields
        static TSettings defaultSettings;
        #endregion

        #region Properties
        /// <summary>
        /// Returns the default settings for this Settings Type
        /// </summary>
        public static TSettings Default {
            get
            {
                if (defaultSettings == null) {
                    if(SettingsManager.Active != null)
                    {
                        defaultSettings = SettingsManager.Active.GetDefaultSettings<TSettings>();
                    }

                    if (defaultSettings == null)
                    {
                        defaultSettings = GetDefaultPathSettings();
                    }
                }

                LastUsed = defaultSettings;
                return defaultSettings;
            }
        }

        /// <summary>
        /// Returns the last settings used for this Settings Type
        /// </summary>
        public static TSettings LastUsed { get; private set; }
        #endregion

        #region Static Methods
        /// <summary>
        /// Returns <see cref="Settings"/> of type <typeparamref name="TSettings"/> from <see cref="SettingsContext"/> name
        /// </summary>
        /// <param name="context"><see cref="SettingsContext"/> name</param>
        /// <returns><see cref="Settings"/> of type <typeparamref name="TSettings"/></returns>
        public static TSettings Context(string context) {
            if(SettingsManager.Active == null)
            {
                throw new Exception("You have to add a <b>SettingsManager</b> to your scene in order to use contexts");
            }

            TSettings settings = SettingsManager.Active.GetSettingsFromContext<TSettings>(context);

            if (settings != null)
                LastUsed = settings;

            return settings;
        }

        static TSettings GetDefaultPathSettings() {
            TSettings[] settings = Resources.LoadAll<TSettings>(DEFAULT_FOLDER_PATH);

            if (settings.Length == 0)
            {
                throw new Exception(string.Format("No default settings of type {0} found in default folder ({1}), you have to create one in that folder or add a SettingsManager to your scene", typeof(TSettings), DEFAULT_FOLDER_PATH));
            }

            if (settings.Length > 1)
            {
                Debug.Log(string.Format("Multiple settings of type {0} were found in the default folder, only the first one ({1}) will be taken into account as default setting", typeof(TSettings), settings[0].name));
            }

            return settings[0];
        }
        #endregion
    }
}
