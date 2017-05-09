using IWMS_LocationServer.src.dal;
using RtlsLibrary.src.model;
using System.Collections.Generic;

namespace IWMS_LocationServer.src.common
{
    public class SceneManager
    {
        private List<SceneModel> _scenes = new List<SceneModel>();
        public List<SceneModel> Scenes { get { return _scenes; } }

        private static volatile SceneManager _instance;
        private static readonly object syncRoot = new object();

        private SceneManager()
        {
            load();
        }
        public static SceneManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new SceneManager();
                        }
                    }
                }
                return _instance;
            }
        }

        private void load()
        {
            _scenes.Clear();
            _scenes.AddRange(SceneDB.getScenes());
        }
        public SceneModel getScene(int scene_id)
        {
            return _scenes.Find(scene => scene.Id == scene_id);
        }

        public void modifyScene(SceneModel scene)
        {
            SceneDB.modifyScene(scene.Id, scene.Name, scene.OriginalPoint.X, scene.OriginalPoint.Y, scene.MapImage, scene.MapOffsetX, scene.MapOffsetY, scene.MapScaleH, scene.MapScaleV, scene.MapFlipH, scene.MapFlipV);

            load();
        }
    }
}
