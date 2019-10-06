using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLibrary
{
    //Read built in Sections
    public class ConfigurationReaderAndWriter
    {
        public static void ReadAllSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                        Console.WriteLine("Key:{0} Value:{1}", key, appSettings[key]);
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        public static void ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                Console.WriteLine(result);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        public static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }


    }

    //Read Custom Sections
    public class LevelSection : ConfigurationSection
    {
        [ConfigurationProperty("name",
            DefaultValue = "LevelDefination",
            IsRequired = true,
            IsKey = false)]
        [StringValidator(InvalidCharacters =
            " ~!@#$%^&*()[]{}/;'\"|\\",
            MinLength = 1, MaxLength = 60)]
        public string Name
        {

            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }

        }

        // Declare an element (not in a collection) of the type
        // UrlConfigElement. In the configuration
        // file it corresponds to <simple .... />.
        [ConfigurationProperty("SimpleSprite")]
        public GameConfigElement Simple
        {
            get
            {
                GameConfigElement gameConfig =
                (GameConfigElement)base["SimpleSprite"];
                return gameConfig;
            }
        }

        // Declare a collection element represented 
        // in the configuration file by the sub-section
        // <urls> <add .../> </urls> 
        // Note: the "IsDefaultCollection = false" 
        // instructs the .NET Framework to build a nested 
        // section like <urls> ...</urls>.
        [ConfigurationProperty("Level1",
            IsDefaultCollection = false)]
        public LevelsCollection Levels
        {
            get
            {
                LevelsCollection levelsCollection =
                (LevelsCollection)base["level1"];
                return levelsCollection;
            }
        }
        //add another levels here

        protected override void DeserializeSection(
            System.Xml.XmlReader reader)
        {
            base.DeserializeSection(reader);
            // add processing codes here.
        }

        protected override string SerializeSection(
            ConfigurationElement parentElement,
            string name, ConfigurationSaveMode saveMode)
        {
            string s =
                base.SerializeSection(parentElement,
                name, saveMode);
            //add processing code here.
            return s;
        }

    }

    public class GameConfigElement : ConfigurationElement
    {
        // Constructor allowing ??? to be specified.
        public GameConfigElement(String name,
            String location)
        {
            SpriteName = name;
            SpriteLocation = location;
        }

        // Default constructor, will use default values as defined
        // below.
        public GameConfigElement()
        {
        }

        // Constructor allowing name to be specified, will take the
        // default values for url and port.
        public GameConfigElement(string elementName)
        {
            SpriteName = elementName;
        }

        [ConfigurationProperty("SpriteName",
            IsRequired = true,
            IsKey = true)]
        public string SpriteName
        {
            get
            {
                return (string)this["SpriteName"];
            }
            set
            {
                this["SpriteName"] = value;
            }
        }

        [ConfigurationProperty("SpriteLocation",
            IsRequired = true,
            IsKey = false)]
        public string SpriteLocation
        {
            get
            {
                return (string)this["SpriteLocation"];
            }
            set
            {
                this["SpriteLocation"] = value;
            }
        }
        //add other properties here.

        protected override void DeserializeElement(
               System.Xml.XmlReader reader,
                bool serializeCollectionKey)
        {
            base.DeserializeElement(reader,
                serializeCollectionKey);
            // add processing codes here.
        }


        protected override bool SerializeElement(
                System.Xml.XmlWriter writer,
                bool serializeCollectionKey)
        {
            bool ret = base.SerializeElement(writer,
                serializeCollectionKey);
            // add processing codes here.
            return ret;

        }


        protected override bool IsModified()
        {
            bool ret = base.IsModified();
            // add processing codes here.
            return ret;
        }
    }

    public class LevelsCollection : ConfigurationElementCollection
    {
        public LevelsCollection()
        {
            // Add one level to the collection.  This is
            // not necessary; could leave the collection 
            // empty until items are added to it outside
            // the constructor.
            GameConfigElement levSprite =
                (GameConfigElement)CreateNewElement();
            Add(levSprite);
        }

        public override
            ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return

                    ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override
            ConfigurationElement CreateNewElement()
        {
            return new GameConfigElement();
        }


        protected override
            ConfigurationElement CreateNewElement(
            string elementName)
        {
            return new GameConfigElement(elementName);
        }


        protected override Object
            GetElementKey(ConfigurationElement element)
        {
            return ((GameConfigElement)element).SpriteName;
        }


        public new string AddElementName
        {
            get
            { return base.AddElementName; }

            set
            { base.AddElementName = value; }

        }

        public new string ClearElementName
        {
            get
            { return base.ClearElementName; }

            set
            { base.ClearElementName = value; }

        }

        public new string RemoveElementName
        {
            get
            { return base.RemoveElementName; }
        }

        public new int Count
        {
            get { return base.Count; }
        }


        public GameConfigElement this[int index]
        {
            get
            {
                return (GameConfigElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public GameConfigElement this[string Name]
        {
            get
            {
                return (GameConfigElement)BaseGet(Name);
            }
        }

        public int IndexOf(GameConfigElement spr)
        {
            return BaseIndexOf(spr);
        }

        public void Add(GameConfigElement spr)
        {
            BaseAdd(spr);
            // Add custom code here.
        }

        protected override void
            BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
            // Add custom code here.
        }

        public void Remove(GameConfigElement spr)
        {
            if (BaseIndexOf(spr) >= 0)
                BaseRemove(spr.SpriteName);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
            // Add custom code here.
        }
    }
}