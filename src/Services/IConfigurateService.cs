using System;
using System.Threading.Tasks;

using ShapeDiver.SDK;
using ShapeDiver.SDK.Authentication;
using ShapeDiver.SDK.PlatformBackend;
using ShapeDiver.SDK.PlatformBackend.DTO;
using ShapeDiver.SDK.GeometryBackend;
using ConfigAR.Backend.Models;

namespace ConfigAR.Backend.Services
{
    public interface IConfigurateService
    {
        Task<Stream> Execute(ConfigarInput modelId);
    }
}
