using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Bundles.SettingsManagement {
    [Serializable]
    public class SettingsContext
    {
        public string name;
        public List<Settings> settings;
    }
}
