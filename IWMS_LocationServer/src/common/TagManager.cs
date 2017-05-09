using IWMS_LocationServer.src.dal;
using RtlsLibrary.src.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace IWMS_LocationServer.src.common
{
    public class TagManager
    {
        private List<TagModel> _tags = new List<TagModel>();
        public List<TagModel> Tags { get { return _tags; } }

        private static volatile TagManager _instance;
        private static readonly object syncRoot = new object();

        private TagManager()
        {
            load();
        }
        public static TagManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new TagManager();
                        }
                    }
                }
                return _instance;
            }
        }

        private void load()
        {
            _tags.Clear();
            _tags.AddRange(TagDB.getTags());
        }
        public bool isTagSnExist(string sn)
        {
            if (_tags.Exists(tag => tag.Sn.CompareTo(sn) == 0))
            {
                return true;
            }
            return false;
        }
        public bool isTagSnExistElse(int id, string sn)
        {
            if (_tags.Exists(tag => tag.Sn.CompareTo(sn) == 0 && tag.Id != id))
            {
                return true;
            }
            return false;
        }
        public void addTag(TagModel tag)
        {
            tag.Id = TagDB.addTag(tag.Sn);
            load();
        }
        public void deleteTag(TagModel tag)
        {
            TagDB.deleteTag(tag.Id);
            load();
        }
        public void deleteAllTags()
        {
            TagDB.deleteAllTags();
            load();
        }
        public void modifyTag(TagModel tag)
        {
            TagDB.modifyTag(tag.Id, tag.Sn);
            load();
        }
    }
}
