using IWMS_LocationSystem.src.dal;
using RtlsLibrary.src.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using Utils;

namespace IWMS_LocationServer.src.dal
{
    public class SceneDB
    {
        public static int addScene(string name, double originalpoint_x, double originalpoint_y,
                                    BitmapImage map_image = null, double map_offset_x = 0, double map_offset_y = 0,
                                    double map_scale_h = 100, double map_scale_v = 100,
                                    bool map_flip_h = false, bool map_flip_v = false)
        {
            string sql = "";
            if (map_image != null)
            {
                sql = string.Format("INSERT INTO scene (name, originalpoint_x, originalpoint_y, map_image, map_offset_x, map_offset_y, map_scale_h, map_scale_v, map_flip_h, map_flip_v) VALUES ('{0}', {1}, {2}, {3}, {4}, {5}, {6}, {7}, '{8}', '{9}');", name, originalpoint_x, originalpoint_y, "@map_image", map_offset_x, map_offset_y, map_scale_h, map_scale_v, map_flip_h, map_flip_v);
                byte[] imageData = new byte[map_image.StreamSource.Length];
                map_image.StreamSource.Seek(0, System.IO.SeekOrigin.Begin);//very important, it should be set to the start of the stream
                map_image.StreamSource.Read(imageData, 0, imageData.Length);
                SQLiteParameter paramr = new SQLiteParameter("@map_image", DbType.Binary, imageData.Length);
                paramr.Value = imageData;
                SQLiteParameter[] Paramrs = { paramr };
                return Convert.ToInt32(SQLiteHelper.ExecuteScalar(sql, CommandType.Text, Paramrs));

            }
            sql = string.Format("INSERT INTO scene (name, originalpoint_x, originalpoint_y, map_image, map_offset_x, map_offset_y, map_scale_h, map_scale_v, map_flip_h, map_flip_v) VALUES ('{0}', {1}, {2}, {3}, {4}, {5}, {6}, {7}, '{8}', '{9}');", name, originalpoint_x, originalpoint_y, "NULL", map_offset_x, map_offset_y, map_scale_h, map_scale_v, map_flip_h, map_flip_v);
            return Convert.ToInt32(SQLiteHelper.ExecuteScalar(sql));
        }

        public static int deleteScene(int id)
        {
            string sql = string.Format("DELETE FROM scene WHERE id = {0}", id);
            return SQLiteHelper.ExecuteNonQuery(sql);
        }

        public static int modifyScene(int id, string name, double originalpoint_x, double originalpoint_y, BitmapImage map_image, double map_offset_x, double map_offset_y, double map_scale_h, double map_scale_v, bool map_flip_h, bool map_flip_v)
        {
            string sql = "";
            if (map_image != null)
            {
                sql = string.Format("UPDATE scene SET name = '{1}', originalpoint_x = {2}, originalpoint_y = {3}, map_image = {4}, map_offset_x = {5}, map_offset_y = {6}, map_scale_h = {7}, map_scale_v = {8}, map_flip_h = '{9}', map_flip_v = '{10}' WHERE id = {0};", id, name, originalpoint_x, originalpoint_y, "@map_image", map_offset_x, map_offset_y, map_scale_h, map_scale_v, map_flip_h, map_flip_v);
                byte[] imageData = new byte[map_image.StreamSource.Length];
                map_image.StreamSource.Seek(0, System.IO.SeekOrigin.Begin);//very important, it should be set to the start of the stream
                map_image.StreamSource.Read(imageData, 0, imageData.Length);
                SQLiteParameter paramr = new SQLiteParameter("@map_image", DbType.Binary, imageData.Length);
                paramr.Value = imageData;
                SQLiteParameter[] Paramrs = { paramr };
                return SQLiteHelper.ExecuteNonQuery(sql, CommandType.Text, Paramrs);
            }
            sql = string.Format("UPDATE scene SET name = '{1}', originalpoint_x = {2}, originalpoint_y = {3}, map_image = {4}, map_offset_x = {5}, map_offset_y = {6}, map_scale_h = {7}, map_scale_v = {8}, map_flip_h = '{9}', map_flip_v = '{10}' WHERE id = {0};", id, name, originalpoint_x, originalpoint_y, map_image, map_offset_x, map_offset_y, map_scale_h, map_scale_v, map_flip_h, map_flip_v);
            return SQLiteHelper.ExecuteNonQuery(sql);
        }

        public static SceneModel getScene(int id)
        {
            string sql = string.Format("SELECT * FROM scene WHERE id = {0};", id);
            DataTable data = SQLiteHelper.ExecuteDataTable(sql);
            if (data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    byte[] imageBytes = row["map_image"] as byte[];
                    SceneModel sm = new SceneModel()
                    {
                        Id = Convert.ToInt32(row["id"].ToString()),
                        Name = row["name"].ToString(),
                        OriginalPoint = new Point(Convert.ToDouble(row["originalpoint_x"].ToString()), Convert.ToDouble(row["originalpoint_y"].ToString())),
                        MapImage = BitmapImageHelper.ByteArrayToBitmapImage(imageBytes),
                        MapOffsetX = Convert.ToDouble(row["map_offset_x"].ToString()),
                        MapOffsetY = Convert.ToDouble(row["map_offset_y"].ToString()),
                        MapScaleH = Convert.ToDouble(row["map_scale_h"].ToString()),
                        MapScaleV = Convert.ToDouble(row["map_scale_v"].ToString()),
                        MapFlipH = Convert.ToBoolean(row["map_flip_h"].ToString()),
                        MapFlipV = Convert.ToBoolean(row["map_flip_v"].ToString())
                    };

                    return sm;
                }
            }

            return null;
        }

        public static List<SceneModel> getScenes()
        {
            string sql = string.Format("SELECT * FROM scene;");
            DataTable table = SQLiteHelper.ExecuteDataTable(sql);
            if (table.Rows.Count > 0)
            {
                List<SceneModel> list = new List<SceneModel>();
                foreach (DataRow row in table.Rows)
                {
                    byte[] imageBytes = row["map_image"] as byte[];

                    SceneModel sm = new SceneModel()
                    {
                        Id = Convert.ToInt32(row["id"].ToString()),
                        Name = row["name"].ToString(),
                        OriginalPoint = new Point(Convert.ToDouble(row["originalpoint_x"].ToString()), Convert.ToDouble(row["originalpoint_y"].ToString())),
                        MapImage = BitmapImageHelper.ByteArrayToBitmapImage(imageBytes),
                        MapOffsetX = Convert.ToDouble(row["map_offset_x"].ToString()),
                        MapOffsetY = Convert.ToDouble(row["map_offset_y"].ToString()),
                        MapScaleH = Convert.ToDouble(row["map_scale_h"].ToString()),
                        MapScaleV = Convert.ToDouble(row["map_scale_v"].ToString()),
                        MapFlipH = Convert.ToBoolean(row["map_flip_h"].ToString()),
                        MapFlipV = Convert.ToBoolean(row["map_flip_v"].ToString())
                    };
                    list.Add(sm);
                }
                return list;
            }
            return new List<SceneModel>();
        }
    }
}
