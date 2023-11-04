using pis_web_api.Models.db;

namespace pis_web_api.Models.post
{
    public class LocalityPost
    {
        public string LocalityName { get; set; } = "";

        public LocalityPost() { }

        public Locality ConvertToLocality()
        {
            return new Locality(LocalityName);
        }

        public Locality ConvertToLocalityWithId(int id)
        {
            var locality = this.ConvertToLocality();
            locality.IdLocality = id;
            return locality;
        }
    }
}
