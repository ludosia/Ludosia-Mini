using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Bundles.SettingsManagement
{
    /// <summary>
    /// Add this class to you scene if you need to use specific default <see cref="Settings"/> or <see cref="SettingsContext"/> in your scene
    /// </summary>
    public class SettingsManager : MonoBehaviour
    {
        #region Enums
        /// <summary>
        /// Type of the action to perform when <see cref="SettingsContext"/> not found
        /// </summary>
        public enum ContextNotFoundAction { useDefault, useDefaultAndLog, throwError }
        #endregion

        #region Static Fields
        /// <summary>
        /// Returns the active instance of <see cref="SettingsManager"/>
        /// </summary>
        public static SettingsManager Active { get; private set; }
        #endregion

        #region Public Fields
        [Header("Default Settings")]
        [Tooltip("The list of Settings to use by default")]
        public List<Settings> defaultSettings;

        [Header("Context Settings")]
        [Tooltip("The action to perform when the Context is not found")]
        public ContextNotFoundAction contextNotFoundAction = ContextNotFoundAction.useDefault;
        [Tooltip("List of settings contexts")]
        public List<SettingsContext> contexts;
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns the matching <typeparamref name="TSettings"/> in <see cref="SettingsContext"/> named <paramref name="contextName"/>
        /// </summary>
        /// <typeparam name="TSettings">The type of the expected <see cref="Settings"/></typeparam>
        /// <param name="contextName">The name of the <see cref="SettingsContext"/></param>
        /// <returns><see cref="Settings"/> of ype <typeparamref name="TSettings"/></returns>
        public TSettings GetSettingsFromContext<TSettings>(string contextName) where TSettings : Settings
        {
            SettingsContext context = GetContextFromName(contextName);

            if(context == null)
            {
                if (contextNotFoundAction == ContextNotFoundAction.throwError)
                    throw new Exception(string.Format("No context found with name {0} in {1} : Aborted", contextName, name));
                else if (contextNotFoundAction == ContextNotFoundAction.useDefaultAndLog)
                    Debug.Log(string.Format("No context found with name {0} in {1} : Skiped to default", contextName, name));
            }

            return GetSettingsFromContext<TSettings>(context);
        }

        /// <summary>
        /// Returns the matching <typeparamref name="TSettings"/> in <paramref name="context"/>
        /// </summary>
        /// <typeparam name="TSettings">The type of the expected <see cref="Settings"/></typeparam>
        /// <returns><see cref="Settings"/> of type <typeparamref name="TSettings"/></returns>
        public TSettings GetSettingsFromContext<TSettings>(SettingsContext context) where TSettings : Settings
        {
            TSettings settings = null;
            if (context != null)
            {
                settings = context.settings.Where(s => s is TSettings).FirstOrDefault() as TSettings;

                if (settings != null)
                    return settings;
                else
                {
                    if (contextNotFoundAction == ContextNotFoundAction.throwError)
                        throw new Exception(string.Format("No Settings of type {0} in context {1} in {2} : Aborted", typeof(TSettings), context.name, name));
                    else if (contextNotFoundAction == ContextNotFoundAction.useDefaultAndLog)
                        Debug.Log(string.Format("No Settings of type {0} in context {1} in {2} : Skiped to default", typeof(TSettings), context.name, name));
                }
            }

           return GetDefaultSettings<TSettings>();
        }

        /// <summary>
        /// Returns the default <typeparamref name="TSettings"/>
        /// </summary>
        /// <typeparam name="TSettings">Type of the expected <see cref="Settings"/></typeparam>
        /// <returns>Default <see cref="Settings"/> of type <typeparamref name="TSettings"/></returns>
        public TSettings GetDefaultSettings<TSettings>() where TSettings : Settings
        {
            return defaultSettings.Where(s => s is TSettings).FirstOrDefault() as TSettings;
        }

        /// <summary>
        /// Return <see cref="SettingsContext"/> from its name
        /// </summary>
        /// <param name="contextName">The name of the expected <see cref="SettingsContext"/></param>
        /// <returns>The <see cref="SettingsContext"/> named <paramref name="contextName"/></returns>
        public SettingsContext GetContextFromName(string contextName) {
            return contexts.Where(c => c.name == contextName).FirstOrDefault();
        }
        #endregion

        #region Runtime Methods
        private void Awake()
        {
            Active = this;
        }
        #endregion
    }
}
