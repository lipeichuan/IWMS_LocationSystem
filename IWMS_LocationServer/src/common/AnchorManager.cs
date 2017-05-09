using IWMS_LocationServer.src.dal;
using RtlsLibrary.src.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace IWMS_LocationServer.src.common
{
    public class AnchorManager
    {
        private List<AnchorModel> _anchors = new List<AnchorModel>();
        public List<AnchorModel> Anchors { get { return _anchors; } }
        private static volatile AnchorManager _instance;
        private static readonly object syncRoot = new object();
        private AnchorManager()
        {
            load();
        }

        public static AnchorManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new AnchorManager();
                        }
                    }
                }
                return _instance;
            }
        }

        private void load()
        {
            _anchors.Clear();
            _anchors.AddRange(AnchorDB.getAnchors());
        }

        public AnchorModel GetAnchorBySn(string sn)
        {
            return _anchors.Find(anchor => anchor.Sn.CompareTo(sn) == 0);
        }

        public bool isAnchorSnExist(string sn)
        {
            if (_anchors.Exists(anchor => anchor.Sn.CompareTo(sn) == 0))
            {
                return true;
            }
            return false;
        }
        public bool isAnchorSnExistElse(int id, string sn)
        {
            if (_anchors.Exists(anchor => anchor.Sn.CompareTo(sn) == 0 && anchor.Id != id))
            {
                return true;
            }
            return false;
        }
        public void addAnchor(AnchorModel anchor)
        {
            anchor.Id = AnchorDB.addAnchor(anchor.Sn, anchor.SceneId, anchor.X, anchor.Y, anchor.Z);
            load();
        }
        public void deleteAnchor(AnchorModel anchor)
        {
            AnchorDB.deleteAnchor(anchor.Id);
            load();
        }
        public void modifyAnchor(AnchorModel anchor)
        {
            AnchorDB.modifyAnchor(anchor.Id, anchor.Sn, anchor.SceneId, anchor.X, anchor.Y, anchor.Z);
            load();
        }

        public void deleteAllAnchors()
        {
            AnchorDB.deleteAllAnchors();
            load();
        }
    }
}
