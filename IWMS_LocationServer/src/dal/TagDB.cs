using IWMS_LocationSystem.src.dal;
using NLog;
using RtlsLibrary.src.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows;

namespace IWMS_LocationServer.src.dal
{
    public class TagDB
    {
        public static int addTag(string sn)
        {
            string sql = string.Format("INSERT INTO tag (sn) VALUES ('{0}');SELECT LAST_INSERT_ROWID() NEWID;", sn);
            return Convert.ToInt32(SQLiteHelper.ExecuteScalar(sql));
        }

        public static int deleteTag(int id)
        {
            string sql = string.Format("DELETE FROM tag WHERE id = {0};", id);
            return SQLiteHelper.ExecuteNonQuery(sql);
        }
        public static int deleteAllTags()
        {
            string sql = string.Format("DELETE FROM tag;");
            return SQLiteHelper.ExecuteNonQuery(sql);
        }

        public static int modifyTag(int id, string sn)
        {
            string sql = string.Format("UPDATE tag SET sn = '{1}' WHERE id = {0};", id, sn);
            return SQLiteHelper.ExecuteNonQuery(sql);
        }

        public static TagModel getTag(int id)
        {
            string sql = string.Format("SELECT * FROM tag WHERE id = {0};", id);
            DataTable data = SQLiteHelper.ExecuteDataTable(sql);

            if (data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    TagModel tm = new TagModel()
                    {
                        Id = Convert.ToInt32(row["id"].ToString()),
                        Sn = row["sn"].ToString()
                    };
                    return tm;
                }
            }

            return null;
        }

        public static TagModel getTagBySn(string sn)
        {
            string sql = string.Format("SELECT * FROM tag WHERE sn = '{0}';", sn);
            DataTable data = SQLiteHelper.ExecuteDataTable(sql);

            if (data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    TagModel tm = new TagModel()
                    {
                        Id = Convert.ToInt32(row["id"].ToString()),
                        Sn = row["sn"].ToString()
                    };
                    return tm;
                }
            }
            return null;
        }

        public static List<TagModel> getTags()
        {
            string sql = string.Format("SELECT * FROM tag;");
            DataTable table = SQLiteHelper.ExecuteDataTable(sql);
            if (table.Rows.Count > 0)
            {
                List<TagModel> list = new List<TagModel>();
                foreach (DataRow row in table.Rows)
                {
                    TagModel tm = new TagModel()
                    {
                        Id = Convert.ToInt32(row["id"].ToString()),
                        Sn = row["sn"].ToString()
                    };
                    list.Add(tm);
                }
                return list;
            }
            return new List<TagModel>();
        }
    }
}
