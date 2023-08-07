using SpotifyAPI.Web;

namespace MyHeardle
{
    public class Split
    {
        public uint Seconds { get; set; }

        public FullTrack Guess { get; set; }

        public Split(uint seconds) 
        {
            Seconds = seconds;
        }

    }
}
