using grpc.location;
using RtlsLibrary.src.model;
using System.Collections.Generic;
using System.Windows;
using Utils;

namespace IWMS_LocationClient.src.bll
{
    public class LocationApi
    {
        private LocationClient _client;
        public LocationApi(LocationClient client)
        {
            _client = client;
        }
        public List<SceneModel> GetScenes()
        {
            List<SceneModel> list = new List<SceneModel>();
            Scenes scenes = _client.GetScenes();

            foreach (Scene scene in scenes.Scene)
            {
                SceneModel sceneModel = new SceneModel
                {
                    Id = scene.Id,
                    Name = scene.Name,
                    OriginalPoint = new Point(scene.OriginalX, scene.OriginalY),
                    MapImage = BitmapImageHelper.ByteArrayToBitmapImage(scene.MapImage.ToByteArray()),
                    MapOffsetX = scene.MapOffsetX,
                    MapOffsetY = scene.MapOffsetY,
                    MapScaleH = scene.MapScaleH,
                    MapScaleV = scene.MapScaleV,
                    MapFlipH = scene.MapFlipH,
                    MapFlipV = scene.MapFlipV
                };

                list.Add(sceneModel);
            }
            return list;
        }
        public List<AnchorModel> GetAnchors()
        {
            List<AnchorModel> list = new List<AnchorModel>();
            Anchors anchors = _client.GetAnchors();

            foreach (Anchor anchor in anchors.Anchor)
            {
                AnchorModel anchorModel = new AnchorModel
                {
                    Id = anchor.Id,
                    Sn = anchor.Sn,
                    SceneId = anchor.SceneId,
                    X = anchor.X,
                    Y = anchor.Y,
                    Z = anchor.Z
                };

                list.Add(anchorModel);
            }
            return list;
        }
        public List<TagModel> GetTags()
        {
            List<TagModel> list = new List<TagModel>();
            Tags tags = _client.GetTags();

            foreach (Tag tag in tags.Tag)
            {
                TagModel tagModel = new TagModel
                {
                    Id = tag.Id,
                    Sn = tag.Sn,
                    IsPositioned = false,
                    X = 0,
                    Y = 0,
                    Z = 0
                };

                list.Add(tagModel);
            }
            return list;
        }
    }
}
