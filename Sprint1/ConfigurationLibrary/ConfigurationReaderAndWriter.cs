using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLibrary
{
    //Read built in Sections
    public static class ConfigurationReaderAndWriter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<挂起>")]
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

    }

    //Read Custom Sections
    public class LevelSection : ConfigurationSection
    {
        // Declare a collection element represented 
        // in the configuration file by the sub-section
        [ConfigurationProperty("Player",
            IsDefaultCollection = false)]
        public NonBlockSpritesCollection Player
        {
            get
            {
                NonBlockSpritesCollection playerCollection =
                (NonBlockSpritesCollection)base[nameof(Player)];
                return playerCollection;
            }
        }

        [ConfigurationProperty("Enemys",
            IsDefaultCollection = false)]
        public NonBlockSpritesCollection Enemys
        {
            get
            {
                NonBlockSpritesCollection enemysCollection =
                (NonBlockSpritesCollection)base[nameof(Enemys)];
                return enemysCollection;
            }
        }

        [ConfigurationProperty("Blocks",
            IsDefaultCollection = false)]
        public BlockSpritesCollection Blocks
        {
            get
            {
                BlockSpritesCollection BlocksCollection =
                (BlockSpritesCollection)base[nameof(Blocks)];
                return BlocksCollection;
            }
        }

        [ConfigurationProperty("Items",
            IsDefaultCollection = false)]
        public NonBlockSpritesCollection Items
        {
            get
            {
                NonBlockSpritesCollection itemsCollection =
                (NonBlockSpritesCollection)base[nameof(Items)];
                return itemsCollection;
            }
        }

        [ConfigurationProperty("Backgrounds",
            IsDefaultCollection = false)]
        public NonBlockSpritesCollection Backgrounds
        {
            get
            {
                NonBlockSpritesCollection backgroundsCollection =
                (NonBlockSpritesCollection)base[nameof(Backgrounds)];
                return backgroundsCollection;
            }
        }
        //add another spriteCollections here

    }

    public class GameConfigElement : ConfigurationElement
    {
        // Constructor allowing ??? to be specified.
        public GameConfigElement(string id, String name,
            String topLeftCornerLoc, string bottomRightCornerLoc)
        {
            SpriteID = id;
            SpriteName = name;
            SpriteStartLocation = topLeftCornerLoc;
            SpriteEndLocation = bottomRightCornerLoc;
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
                return (string)this[nameof(SpriteID)];
            }
            set
            {
                this[nameof(SpriteID)] = value;
            }
        }

        [ConfigurationProperty("SpriteName",
            IsRequired = true,
            IsKey = false)]
        public string SpriteName
        {
            get
            {
                return (string)this[nameof(SpriteName)];
            }
            set
            {
                this[nameof(SpriteName)] = value;
            }
        }

        [ConfigurationProperty("SpriteStartLocation",
            IsRequired = true,
            IsKey = false)]
        public string SpriteStartLocation
        {
            get
            {
                return (string)this[nameof(SpriteStartLocation)];
            }
            set
            {
                this[nameof(SpriteStartLocation)] = value;
            }
        }

        [ConfigurationProperty("SpriteEndLocation",
            IsRequired = true,
            IsKey = false)]
        public string SpriteEndLocation
        {
            get
            {
                return (string)this[nameof(SpriteEndLocation)];
            }
            set
            {
                this[nameof(SpriteEndLocation)] = value;
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

    public class BlockConfigElement : GameConfigElement
    {
        public BlockConfigElement(string id, String name,
            String topLeftCornerLoc, string bottomRightCornerLoc,
            String embeddedSpriteName, string embeddedSpriteNum) : base(id, name, topLeftCornerLoc, bottomRightCornerLoc)
        {
            EmbeddedSpriteName = embeddedSpriteName;
            EmbeddedSpriteNum = embeddedSpriteNum;
        }

        [ConfigurationProperty("EmbeddedSpriteName",
           IsRequired = true,
           IsKey = false)]
        public string EmbeddedSpriteName
        {
            get
            {
                return (string)this[nameof(EmbeddedSpriteName)];
            }
            set
            {
                this[nameof(EmbeddedSpriteName)] = value;
            }
        }
        public BlockConfigElement()
        {
        }

        // Constructor allowing name to be specified, will take the
        // default values for url and port.
        public BlockConfigElement(string elementID)
        {
            SpriteID = elementID;
        }

        [ConfigurationProperty("EmbeddedSpriteNum",
            IsRequired = true,
            IsKey = false)]
        public string EmbeddedSpriteNum
        {
            get
            {
                return (string)this[nameof(EmbeddedSpriteNum)];
            }
            set
            {
                this[nameof(EmbeddedSpriteNum)] = value;
            }
        }
    }

    public class NonBlockSpritesCollection : ConfigurationElementCollection
    {
        public NonBlockSpritesCollection()
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

    public class BlockSpritesCollection : ConfigurationElementCollection
    {
        public BlockSpritesCollection()
        {
            // Add one level to the collection.  This is
            // not necessary; could leave the collection 
            // empty until items are added to it outside
            // the constructor.
            BlockConfigElement levSprite =
                (BlockConfigElement)CreateNewElement();
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
            return new BlockConfigElement();
        }


        protected override Object
            GetElementKey(ConfigurationElement element)
        {
            return ((BlockConfigElement)element).SpriteID;
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


        public BlockConfigElement this[int index]
        {
            get
            {
                return (BlockConfigElement)BaseGet(index);
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

        new public BlockConfigElement this[string id]
        {
            get
            {
                return (BlockConfigElement)BaseGet(id);
            }
        }

        public int IndexOf(BlockConfigElement spr)
        {
            return BaseIndexOf(spr);
        }

        public void Add(BlockConfigElement spr)
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

        public void Remove(BlockConfigElement spr)
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