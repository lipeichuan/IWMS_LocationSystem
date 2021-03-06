﻿using RtlsLibrary.src.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWMS_LocationClient.src.common
{
    public class Common
    {
        private List<SceneModel> _scenes = null;
        private List<AnchorModel> _anchors = null;
        private List<TagModel> _tags = null;
        public List<SceneModel> Scenes { get { return _scenes; } }

        private SceneModel _currentScene = null;
        public SceneModel CurrentScene { get { return _currentScene; } }
        public List<AnchorModel> Anchors { get { return _anchors; } }
        public List<TagModel> Tags { get { return _tags; } }

        private static volatile Common _instance;
        private static readonly object syncRoot = new object();

        private Common()
        {
            _scenes = new List<SceneModel>();
            _anchors = new List<AnchorModel>();
            _tags = new List<TagModel>();
        }
        public static Common Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new Common();
                        }
                    }
                }
                return _instance;
            }
        }

        public void SetScenes(List<SceneModel> scenes)
        {
            _scenes.Clear();
            _scenes.AddRange(scenes);
            _currentScene = null;
            if (_scenes.Count > 0)
            {
                _currentScene = _scenes.ElementAt(0);
            }
        }
        public void SetAnchors(List<AnchorModel> anchors)
        {
            _anchors.Clear();
            _anchors.AddRange(anchors);
        }
        public void SetTags(List<TagModel> tags)
        {
            _tags.Clear();
            _tags.AddRange(tags);
        }

        public TagModel GetTag(string sn)
        {
            for (int i = 0; i < _tags.Count; i++)
            {
                if (_tags.ElementAt(i).Sn.CompareTo(sn) == 0)
                {
                    return _tags.ElementAt(i);
                }
            }
            return null;
        }
    }
}
