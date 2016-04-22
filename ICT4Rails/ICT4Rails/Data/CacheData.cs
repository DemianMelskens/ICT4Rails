using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICT4Rails.Models;
using ICT4Rails.Models.Users;

namespace ICT4Rails.Data
{
    public class CacheData
    {
        private static TramQueries tramqueries = new TramQueries();
        private static UserQueries userqueries = new UserQueries();
        private static SegmentQueries segmentqueries = new SegmentQueries();
        private static ReservationQueries reservationqueries = new ReservationQueries();
        private static MaintenanceQueries maintenancequeries = new MaintenanceQueries();

        public List<Tram> trams { get; set; }
        public List<User> users { get; set; }
        public List<Segment> segments { get; set; }
        public List<Reservation> reservations { get; set; }
        public List<Track> tracks { get; set; }
        public List<Maintenance> maintenances { get; set; }

        public void LoadData()
        {
            trams = tramqueries.GetTrams();
            users = userqueries.GetUsers();
            segments = segmentqueries.GetSegments(trams);
            reservations = reservationqueries.GetReservations(trams, segments);
            maintenances = maintenancequeries.GetMaintenance(trams, users);
            tracks = FillTracks();
            updateSegments();
        }

        public List<Track> FillTracks()
        {
            List<Track> tracks = segmentqueries.GetTracks();
            foreach(Track track in tracks)
            {
                track.Segments = FillSegmentsIntoTrack(track);
            }
            return tracks;
        }

        public List<Segment> FillSegmentsIntoTrack(Track track)
        {
            List<Segment> tracksegments = new List<Segment>();
            foreach(Segment segment in segments)
            {
                if (segment.Track.LineID == track.LineID)
                {
                    tracksegments.Add(segment);
                }
            }
            return tracksegments;
        }

        public void updateSegments()
        {
            foreach(Segment segment in segments)
            {
                foreach(Track track in tracks)
                {
                    if (segment.Track.LineID == track.LineID)
                    {
                        segment.Track = track;
                    }
                }
            }
        }
    }
}
