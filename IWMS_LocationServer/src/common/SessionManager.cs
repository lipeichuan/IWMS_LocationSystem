using IWMS_LocationServer.src.bll;
using IWMS_LocationServer.src.common;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWMS_LocationServer.src.common
{
    public class SessionManager
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private Dictionary<string, Session> _sessions;
        public Dictionary<string, Session> Sessions { get { return _sessions; } }

        private static volatile SessionManager _instance;
        private static readonly object syncRoot = new object();
        private SessionManager()
        {
            _sessions = new Dictionary<string, Session>();
        }
        public static SessionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new SessionManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public void AddSession(string sessionId)
        {
            try
            {
                if (_sessions.ContainsKey(sessionId))
                {
                    _sessions.Remove(sessionId);
                }
                _sessions.Add(sessionId, new Session(sessionId));
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }

        }

        public Session GetSession(string sessionId)
        {
            try
            {
                if (_sessions.ContainsKey(sessionId))
                {
                    return _sessions[sessionId];
                }
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
            return null;
        }

        public void RemoveSession(string sessionId)
        {
            try
            {
                Session session = GetSession(sessionId);
                if (session != null)
                {
                    session.Dispose();
                }
                _sessions.Remove(sessionId);
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }

        public void print()
        {
            _logger.Info("sessions {0}", _sessions.Count);
            foreach (KeyValuePair<string, Session> s in _sessions)
            {
                _logger.Info(" = {0} : {1}", s.Key, s.Value.SessionId);
            }

        }
    }
}
