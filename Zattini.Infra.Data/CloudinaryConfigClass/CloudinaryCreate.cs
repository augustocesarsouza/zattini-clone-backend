namespace Zattini.Infra.Data.CloudinaryConfigClass
{
    public class CloudinaryCreate
    {
        public string? PublicId { get; set; }
        public string? ImgUrl { get; set; }

        public CloudinaryCreate(string? publicId, string? imgUrl)
        {
            PublicId = publicId;
            ImgUrl = imgUrl;
        }

        public CloudinaryCreate()
        {
        }
    }
}
