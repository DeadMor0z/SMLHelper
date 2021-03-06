﻿namespace SMLHelper.V2.Json
{
    using System.IO;
    using System.Reflection;
    using System.Linq;
    using SMLHelper.V2.Json.Converters;
    using SMLHelper.V2.Json.ExtensionMethods;
    using SMLHelper.V2.Json.Interfaces;
    using Oculus.Newtonsoft.Json;
    using Oculus.Newtonsoft.Json.Converters;

    /// <summary>
    /// A simple implementation of <see cref="IJsonFile"/> for use with config files.
    /// </summary>
    public abstract class ConfigFile : IJsonFile
    {
        /// <summary>
        /// The file path at which the JSON file is accessible for reading and writing.
        /// </summary>
        public string JsonFilePath { get; private set; }

        [JsonIgnore]
        private readonly JsonConverter[] alwaysIncludedJsonConverters = new JsonConverter[] {
            new KeyCodeConverter(),
            new FloatConverter(),
            new StringEnumConverter(),
            new VersionConverter()
        };

        /// <summary>
        /// The <see cref="JsonConverter"/>s that should always be used when reading/writing JSON data.
        /// </summary>
        /// <seealso cref="alwaysIncludedJsonConverters"/>
        public JsonConverter[] AlwaysIncludedJsonConverters => alwaysIncludedJsonConverters;

        /// <summary>
        /// Creates a new instance of <see cref="ConfigFile"/>.
        /// </summary>
        /// <param name="fileName">The name of the <see cref="ConfigFile"/>, "config" by default.</param>
        /// <param name="subfolder">Optional subfolder for the <see cref="ConfigFile"/>.</param>
        /// <example>
        /// <code>
        /// using SMLHelper.V2.Options;
        /// using UnityEngine;
        /// 
        /// public static class MyConfig : ConfigFile
        /// {
        ///     public KeyCode ActivationKey { get; set; } = KeyCode.Escape;
        ///     public MyConfig() : base("options", "Config Files") { }
        ///     // The config file will be stored at the path "QMods\YourModName\Config Files\options.json"
        /// }
        /// </code>
        /// </example>
        protected ConfigFile(string fileName = "config", string subfolder = null)
        {
            var path = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "config";
            }
            JsonFilePath = Path.Combine(
                path,
                string.IsNullOrEmpty(subfolder) ? string.Empty : subfolder,
                $"{fileName}.json"
            );
        }

        /// <summary>
        /// Loads the JSON properties from the file on disk into the <see cref="ConfigFile"/>.
        /// </summary>
        /// <param name="createFileIfNotExist">Whether a new JSON file should be created with default values if it does not
        /// already exist.</param>
        /// <param name="jsonConverters">Optional <see cref="JsonConverter"/>s to be used for serialization.
        /// The <see cref="AlwaysIncludedJsonConverters"/> will always be used, regardless of whether you pass them.</param>
        /// <seealso cref="Save(JsonConverter[])"/>
        public void Load(bool createFileIfNotExist = true, params JsonConverter[] jsonConverters)
            => this.LoadJson(JsonFilePath, true,
                AlwaysIncludedJsonConverters.Concat(jsonConverters).Distinct().ToArray());

        /// <summary>
        /// Saves the current fields and properties of the <see cref="ConfigFile"/> as JSON properties to the file on disk.
        /// </summary>
        /// <param name="jsonConverters">Optional <see cref="JsonConverter"/>s to be used for deserialization.
        /// The <see cref="AlwaysIncludedJsonConverters"/> will always be used, regardless of whether you pass them.</param>
        /// <seealso cref="Load(bool, JsonConverter[])"/>
        public void Save(params JsonConverter[] jsonConverters)
            => this.SaveJson(JsonFilePath,
                AlwaysIncludedJsonConverters.Concat(jsonConverters).Distinct().ToArray());
    }
}
