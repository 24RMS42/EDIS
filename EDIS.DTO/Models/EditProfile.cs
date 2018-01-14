using EDIS.Domain.Profile;

namespace EDIS.Shared.Models
{
    public class EditProfile
    {
        public User User { get; set; }
        public Instrument Instrument { get; set; }
        public string UserLogoPath { get; set; }

        public EditProfile()
        {
            User = new User();
            Instrument = new Instrument();
            UserLogoPath = null;
        }
    }
}