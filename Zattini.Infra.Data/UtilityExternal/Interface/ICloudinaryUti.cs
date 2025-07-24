using CloudinaryDotNet.Actions;
using Zattini.Infra.Data.CloudinaryConfigClass;

namespace Zattini.Infra.Data.UtilityExternal.Interface
{
    public interface ICloudinaryUti
    {
        public Task<CloudinaryCreate> CreateMedia(string url, string folder, int width, int height);
        public CloudinaryResult DeleteMediaCloudinary(string url, ResourceType resourceType);
    }
}
