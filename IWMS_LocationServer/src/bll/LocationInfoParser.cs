using grpc.location;
using IWMS_LocationServer.src.common;
using NLog;
using RtlsLibrary.src.Location;
using RtlsLibrary.src.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IWMS_LocationServer.src.bll
{
    public class LocationInfoParser
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private static Trilateration _tril = new Trilateration();
        
        struct target_info
        {
            public int pan;
            public string mac_address;
        }
        public static bool GetLocation(string str, out TagLocation tagLocation)
        {
            tagLocation = new TagLocation
            {
                TagSn = "",
                IsLocation = false,
                X = 0,
                Y = 0,
                Z = 0
            };

            int index = 0;
            int loop = 0;
            string tag_sn = "";
            string anchor_sn = "";
            int tag_index = 0;
            int time = 0;
            int use4thAnchor = 0;
            target_info[] anchor_info = new target_info[5];
            vec3d[] anchor_location = new vec3d[5];
            double[] distance = new double[5];
            vec3d[] temp = new vec3d[1];
            vec3d[,] tag_location = new vec3d[5, 13];
            int[] receve_num = new int[5];

            //string str = Encoding.ASCII.GetString(buf);
            //Console.WriteLine(str);
            _logger.Info(str);
            string[] sArray = str.Split(',', ':');
            /*
            foreach (string i in sArray)
            {
                //Console.WriteLine(i.ToString());
                Log(i.ToString());
            }
            */
            //字符串解析
            index = 0;
            for (loop = 0; loop < sArray.Count(); loop++)
            {
                if (sArray[loop] == "T")
                {
                    tag_sn = sArray[loop + 1];
                    tag_index = Convert.ToInt32(sArray[loop + 1], 16) % 5;
                    continue;
                }
                if (sArray[loop] == "S")
                {
                    time = Convert.ToInt32(sArray[loop + 1], 10);
                    continue;
                }
                if (sArray[loop] == "A")
                {
                    anchor_info[index].pan = 0;
                    anchor_info[index].mac_address = sArray[loop + 1];
                    _logger.Debug("########### mac_address {0}\n", anchor_info[index].mac_address);
                    AnchorModel anchor = AnchorManager.Instance.GetAnchorBySn(anchor_info[index].mac_address);
                    if (AnchorManager.Instance.GetAnchorBySn(anchor_info[index].mac_address) == null)
                    {
                        _logger.Warn("anchor == null");
                        return false;
                    }
                    _logger.Debug("@@@@@@@@@@@ SceneId {0}\n", anchor.SceneId);
                    anchor_sn = anchor_info[index].mac_address;
                    anchor_location[index].x = anchor.X;
                    anchor_location[index].y = anchor.Y;
                    anchor_location[index].z = anchor.Z;

                    continue;
                }
                if (sArray[loop] == "D")
                {
                    distance[index] = Convert.ToDouble(sArray[loop + 1]);
                    index++;
                }
            }

            tagLocation.TagSn = tag_sn;
            for (int i = 0; i < index; i++)
            {
                tagLocation.DistanceData.Add(new DistanceData { AnchorSn = anchor_info[i].mac_address, Distance = distance[i]});
            }


            //锚点坐标获取
            /*
             * {
                anchor_location[0].x = 13047906.1934 + 3.282;
                anchor_location[0].y = 4743768.3356 + 1.904;
                anchor_location[0].z = 0.0;
                anchor_location[1].x = 13047906.1934 + 6.55;
                anchor_location[1].y = 4743768.3356 + 6.616;
                anchor_location[1].z = 0.0;
                anchor_location[2].x = 13047906.1934 + 10.118;
                anchor_location[2].y = 4743768.3356 + 1.996;
                anchor_location[2].z = 0.0;
                distance[0] = 4.61915;
                distance[1] = 2.70376;
                distance[2] = 3.42078;
                index = 3;
            }*/
            //标签位置计算
            if (index < 3)
            {
                _logger.Warn("index < 3");
                return true;
            }
            if (index > 3)
            {
                use4thAnchor = 1;
            }

            _tril.GetLocation(temp, use4thAnchor, anchor_location, distance);
            tag_location[tag_index, 0] = temp[0];
            //Point position = new Point(tag_location[0].x, tag_location[0].y);
            {
                //初始化数据采集
                if (receve_num[tag_index] < 20)
                {
                    for (int i = 11; i > 0; i--)
                    {
                        tag_location[tag_index, i].x = tag_location[tag_index, i - 1].x;
                        tag_location[tag_index, i].y = tag_location[tag_index, i - 1].y;
                    }
                    receve_num[tag_index] = receve_num[tag_index] + 1;
                    return true;
                }
                //剔除抖动点
                if (receve_num[tag_index] < 50)
                {
                    double mean_x = 0;
                    for (int i = 11; i > 0; i--)
                    {
                        mean_x += tag_location[tag_index, i].x;
                    }
                    mean_x = mean_x / 11;
                    for (int i = 11; i > 1; i--)
                    {
                        tag_location[tag_index, i].x = tag_location[tag_index, i - 1].x;
                    }
                    if (Math.Abs(mean_x - tag_location[tag_index, 0].x) > 1)
                    {
                        tag_location[tag_index, 1].x = mean_x; //need do same thing
                    }
                    else
                    {
                        tag_location[tag_index, 1].x = tag_location[tag_index, 0].x; ;
                    }


                    double mean_y = 0;
                    for (int i = 11; i > 0; i--)
                    {
                        mean_y += tag_location[tag_index, i].y;
                    }
                    mean_y = mean_y / 11;
                    for (int i = 11; i > 1; i--)
                    {
                        tag_location[tag_index, i].y = tag_location[tag_index, i - 1].y;
                    }
                    if (Math.Abs(mean_y - tag_location[tag_index, 0].y) > 1)
                    {
                        tag_location[tag_index, 1].y = mean_y; //need do same thing
                    }
                    else
                    {
                        tag_location[tag_index, 1].y = tag_location[tag_index, 0].y; ;
                    }

                    receve_num[tag_index] = receve_num[tag_index] + 1;
                    return true;
                }
                //基于距离的坐标修正
                {
                    double sum_x = 0;
                    double sum_y = 0;
                    for (int i = 2; i < 11; i++)
                    {
                        sum_x += Math.Abs(tag_location[tag_index, i].x - tag_location[tag_index, i - 1].x);
                        sum_y += Math.Abs(tag_location[tag_index, i].y - tag_location[tag_index, i - 1].y);
                    }
                    sum_x /= 10;
                    sum_y /= 10;
                    double dx = (tag_location[tag_index, 0].x - tag_location[tag_index, 1].x);
                    double dy = (tag_location[tag_index, 0].y - tag_location[tag_index, 1].y);
                    tag_location[tag_index, 11].x = tag_location[tag_index, 0].x * (sum_x / (sum_x + Math.Abs(dx))) + (tag_location[tag_index, 1].x + sum_x * (Math.Abs(dx) / dx)) * (Math.Abs(dx) / (sum_x + Math.Abs(dx)));
                    tag_location[tag_index, 11].y = tag_location[tag_index, 0].y * (sum_y / (sum_y + Math.Abs(dy))) + (tag_location[tag_index, 1].y + sum_y * (Math.Abs(dy) / dy)) * (Math.Abs(dy) / (sum_y + Math.Abs(dy)));
                }
                //刷新历史记录
                {
                    for (int i = 10; i > 1; i--)
                    {
                        tag_location[tag_index, i].x = tag_location[tag_index, i - 1].x;
                        tag_location[tag_index, i].y = tag_location[tag_index, i - 1].y;
                    }
                    tag_location[tag_index, 1].x = tag_location[tag_index, 11].x;
                    tag_location[tag_index, 1].y = tag_location[tag_index, 11].y;
                }
                if (Math.Abs((tag_location[tag_index, 11].x - tag_location[tag_index, 12].x) + Math.Abs(tag_location[tag_index, 11].y - tag_location[tag_index, 12].y)) < 0.15)// || Math.Abs((tag_location[tag_index,3].x - tag_location[tag_index,4].x) + Math.Abs(tag_location[tag_index,3].y - tag_location[tag_index,4].y)) > 2.0)
                {
                    _logger.Warn(string.Format("move too small !!!! (x:{0}, y:{1})\n", tag_location[tag_index, 11].x, tag_location[tag_index, 11].y));
                    //Console.WriteLine(string.Format("move too small !!!! (x{0}y{1})\n", tag_location[tag_index,0].x, tag_location[tag_index,0].y));
                    return true;
                }
                tag_location[tag_index, 12].x = tag_location[tag_index, 11].x;
                tag_location[tag_index, 12].y = tag_location[tag_index, 11].y;
            }
            //结果上报
            //Console.WriteLine("move new  position !!!!");
            //Log(string.Format("move new  position !!!!");
            tagLocation.IsLocation = true;
            tagLocation.X = tag_location[tag_index, 12].x;
            tagLocation.Y = tag_location[tag_index, 12].y;
            tagLocation.Z = tag_location[tag_index, 12].z;
            //RunTimeManager.instance.setTagPosition(tag_sn, tag_location[tag_index, 12].x, tag_location[tag_index, 12].y, tag_location[tag_index, 12].z, anchor_sn);
            return true;
        }
    }
}
