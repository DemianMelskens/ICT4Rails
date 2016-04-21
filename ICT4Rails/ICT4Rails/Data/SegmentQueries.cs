using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICT4Rails.Models;
using ICT4Rails.Models.Users;

namespace ICT4Rails.Data
{
    public class SegmentQueries
    {
        public List<Segment> GetSegments(List<Tram> trams)
        {
            Segment segment = null;
            Track track = null;
            Tram givetram = null;
            List<Segment> segments = new List<Segment>();
            using (var database = DbConnection.Connection)
            using (var command = database.CreateCommand())
            {
                command.CommandText = "SELECT s.SegmentID, s.TrackID, s.Blocked, s.TramID, t.TrackID, t.Linenumber " +
                                      "FROM " + '"' + "Segment" + '"' + " s, TRACK t " +
                                      "WHERE s.TrackID = t.TrackID";

                try
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                track = new Track(Convert.ToInt32(reader["TrackID"]), Convert.ToString(reader["Linenumber"]));
                                foreach(Tram tram in trams)
                                {
                                    string datatram = Convert.ToString(reader["TramID"]);
                                    if (datatram == tram.TramID)
                                    {
                                        givetram = tram;
                                        break;
                                    }
                                    else if(datatram == "")
                                    {
                                        givetram = null;
                                        break;
                                    }
                                }
                                segment = new Segment(Convert.ToString(reader["SegmentID"]), Convert.ToBoolean(Convert.ToInt32(reader["Blocked"])), track, givetram);
                                segments.Add(segment);
                            }
                        }
                        return segments;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public void ChangeSegmentBlocked(int segmentid, int blocked)
        {
            using (var database = DbConnection.Connection)
            using (var command = database.CreateCommand())
            { 

                command.CommandText = "UPDATE " + '"' + "Segment" + '"' +
                                      "SET Blocked=" + @blocked + " " +
                                      "WHERE SegmentID=" + @segmentid;

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {

                }
            }
        }

        public void ChangeSegmentTram(string segmentid, string tramid)
        {
            using (var database = DbConnection.Connection)
            using (var command = database.CreateCommand())
            {

                command.CommandText = "UPDATE " + '"' + "Segment" + '"' +
                                      "SET TramID=" + @tramid + " " +
                                      "WHERE SegmentID=" + @segmentid;

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
