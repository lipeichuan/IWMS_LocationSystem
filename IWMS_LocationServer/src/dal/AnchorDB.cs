using IWMS_LocationSystem.src.dal;
using NLog;
using RtlsLibrary.src.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IWMS_LocationServer.src.dal
{
    public class AnchorDB
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public static int addAnchor(string sn, int scene_id = 0, double x = 0, double y = 0, double z = 0)
        {
            string sql = string.Format("INSERT INTO anchor (sn, scene_id, x, y, z) VALUES ('{0}', {1}, {2}, {3}, {4});SELECT LAST_INSERT_ROWID() NEWID;", sn, scene_id, x, y, z);
            return Convert.ToInt32(SQLiteHelper.ExecuteScalar(sql));
        }

        public static int deleteAnchor(int id)
        {
            string sql = string.Format("DELETE FROM anchor WHERE id = {0}", id);
            return SQLiteHelper.ExecuteNonQuery(sql);
        }

        public static int deleteAllAnchors()
        {
            string sql = string.Format("DELETE FROM anchor");
            return SQLiteHelper.ExecuteNonQuery(sql);
        }

        public static int modifyAnchor(int id, string sn, int scene_id, double x, double y, double z)
        {
            string sql = string.Format("UPDATE anchor SET sn = '{1}', scene_id = {2}, x = {3}, y = {4}, z = {5} WHERE id = {0}", id, sn, scene_id, x, y, z);
            return SQLiteHelper.ExecuteNonQuery(sql);
        }

        public static AnchorModel getAnchor(int id)
        {
            string sql = string.Format("SELECT * FROM anchor WHERE id = {0}", id);
            DataTable data = SQLiteHelper.ExecuteDataTable(sql);
            if (data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    AnchorModel am = new AnchorModel()
                    {
                        Id = DBNull.Value == row["id"] ? 0 : Convert.ToInt32(row["id"].ToString()),
                        Sn = row["sn"].ToString(),
                        SceneId = DBNull.Value == row["scene_id"] ? 0 : Convert.ToInt32(row["scene_id"].ToString()),
                        X = DBNull.Value == row["x"] ? 0 : Convert.ToDouble(row["x"].ToString()),
                        Y = DBNull.Value == row["y"] ? 0 : Convert.ToDouble(row["y"].ToString()),
                        Z = DBNull.Value == row["z"] ? 0 : Convert.ToDouble(row["z"].ToString())
                    };
                    return am;
                }
            }

            return null;
        }

        public static List<AnchorModel> getAnchors()
        {
            string sql = string.Format("SELECT * FROM anchor");
            DataTable table = SQLiteHelper.ExecuteDataTable(sql);
            if (table.Rows.Count > 0)
            {
                List<AnchorModel> list = new List<AnchorModel>();
                foreach (DataRow row in table.Rows)
                {
                    AnchorModel am = new AnchorModel()
                    {
                        Id = DBNull.Value == row["id"] ? 0 : Convert.ToInt32(row["id"].ToString()),
                        Sn = row["sn"].ToString(),
                        SceneId = DBNull.Value == row["scene_id"] ? 0 : Convert.ToInt32(row["scene_id"].ToString()),
                        X = DBNull.Value == row["x"] ? 0 : Convert.ToDouble(row["x"].ToString()),
                        Y = DBNull.Value == row["y"] ? 0 : Convert.ToDouble(row["y"].ToString()),
                        Z = DBNull.Value == row["z"] ? 0 : Convert.ToDouble(row["z"].ToString())
                    };
                    list.Add(am);
                }
                return list;
            }
            return new List<AnchorModel>();
        }
    }
}
