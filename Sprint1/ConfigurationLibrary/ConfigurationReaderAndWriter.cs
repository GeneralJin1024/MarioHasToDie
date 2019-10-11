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

        public static int ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                Console.WriteLine(result);
                return (int)decimal.Parse(appSettings[key]);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
                return -1;
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
        // Declare a collection element represented 
        // in the configuration file by the sub-section
        [ConfigurationProperty("Player",
            IsDefaultCollection = false)]
        public SpritesCollection Player
        {
            get
            {
                SpritesCollection playerCollection =
                (SpritesCollection)base["Player"];
                return playerCollection;
            }
        }

        [ConfigurationProperty("Enemys",
            IsDefaultCollection = false)]
        public SpritesCollection Enemys
        {
            get
            {
                SpritesCollection enemysCollection =
                (SpritesCollection)base["Enemys"];
                return enemysCollection;
            }
        }

        [ConfigurationProperty("Blocks",
            IsDefaultCollection = false)]
        public SpritesCollection Blocks
        {
            get
            {
                SpritesCollection BlocksCollection =
                (SpritesCollection)base["Blocks"];
                return BlocksCollection;
            }
        }

        [ConfigurationProperty("Items",
            IsDefaultCollection = false)]
        public SpritesCollection Items
        {
            get
            {
                SpritesCollection itemsCollection =
                (SpritesCollection)base["Items"];
                return itemsCollection;
            }
        }

        [ConfigurationProperty("Backgrounds",
            IsDefaultCollection = false)]
        public SpritesCollection Backgrounds
        {
            get
            {
                SpritesCollection backgroundsCollection =
                (SpritesCollection)base["Backgrounds"];
                return backgroundsCollection;
            }
        }
        //add another spriteCollections here

    }

    public class GameConfigElement : ConfigurationElement
    {
        // Constructor allowing ??? to be specified.
        public GameConfigElement(string id, String name,
            String location)
        {
            SpriteID = id;
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
        public GameConfigElement(string elementID)
        {
            SpriteID = elementID;
        }

        [ConfigurationProperty("SpriteID",
            IsRequired = true,
            IsKey = true)]
        public string SpriteID
        {
            get
            {
                return (string)this["SpriteID"];
            }
            set
            {
                this["SpriteID"] = value;
            }
        }

        [ConfigurationProperty("SpriteName",
            IsRequired = true,
            IsKey = false)]
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

        protected override bool IsModified()
        {
            bool ret = base.IsModified();
            // add processing codes here.
            return ret;
        }
    }

    public class SpritesCollection : ConfigurationElementCollection
    {
        public SpritesCollection()
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
            string elementID)
        {
            return new GameConfigElement(elementID);
        }


        protected override Object
            GetElementKey(ConfigurationElement element)
        {
            return ((GameConfigElement)element).SpriteID;
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

        new public GameConfigElement this[string id]
        {
            get
            {
                return (GameConfigElement)BaseGet(id);
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
                BaseRemove(spr.SpriteID);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string id)
        {
            BaseRemove(id);
        }

        public void Clear()
        {
            BaseClear();
            // Add custom code here.
        }
    }
}