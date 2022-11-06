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
    public class ConfigurateService : IConfigurateService
    {
        string _keyId;
        string _keySecret;
        ShapeDiverSDK _sdk;
        IDictionary<string, IGeometryBackendContext> _modelIdCtxMap;


        public ConfigurateService(IConfiguration configuration)
        {
            var section = configuration.GetSection("ShapeDiver");
            _keyId = section.GetValue<string>("KeyId");
            _keySecret = section.GetValue<string>("KeySecret");

            if (String.IsNullOrEmpty(_keyId))
            {
                //Console.Write("Enter ShapeDiver access key id (or username/email): ");
                //KeyId = Console.ReadLine();
                Console.WriteLine("Failed to get KeyId");
            }
            if (String.IsNullOrEmpty(_keySecret))
            {
                //Console.Write("Enter ShapeDiver access key secret (or password): ");
                //keySecret = Console.ReadLine();
                Console.WriteLine("Failed to get KeySecret");
            }

            _sdk = new ShapeDiverSDK();
            _modelIdCtxMap = new Dictionary<string, IGeometryBackendContext>();
        }


        public async Task<Stream> Execute(ConfigarInput input)
        {
            try
            {
                string modelId = input.Model;

                if(!_sdk.AuthenticationClient.IsAuthenticated)
                {
                    await _sdk.AuthenticationClient.Authenticate(_keyId, _keySecret);
                }

                Console.WriteLine($"{Environment.NewLine}IsAuthenticated: {_sdk.AuthenticationClient.IsAuthenticated}");

                if(!_modelIdCtxMap.ContainsKey(modelId))
                {
                    IGeometryBackendContext ctx = await _sdk.GeometryBackendClient.GetSessionContext(modelId, _sdk.PlatformClient);
                    _modelIdCtxMap.Add(modelId, ctx);
                }

                IGeometryBackendContext context = _modelIdCtxMap[modelId];
                // get parameters of latest model

                string parameterId = context.ModelData.Parameters
                    .Where(kvp => kvp.Value.Name.ToLower() == "polylinejson")
                    .Select(kvp => kvp.Key)
                    .FirstOrDefault();

                // computation request
                var paramValues = new Dictionary<string, string>();

                string points = input.GetPointString();
                //string points = "{\"points\":[[1,0,0],[0.5,0,0],[1.5,1,0],[-0.5,1.5,0]]}";
                paramValues.Add(parameterId, points);

                // TODO: add pairs of parameter id and string value for any parameter that you want to set
                var computeResult = await _sdk.GeometryBackendClient.ComputeOutputs(context, paramValues);
                var glbAsset = _sdk.GeometryBackendClient.GetAllOutputAssetsForFormat(context, computeResult, "glb").FirstOrDefault();
                if (glbAsset != null)
                {
                    var stream = await glbAsset.GetStream();
                    return stream;
                }
            }
            catch (GeometryBackendError e)
            {
                Console.WriteLine($"{Environment.NewLine}GeometryBackendError: {e.Message}");
            }
            catch (PlatformBackendError e)
            {
                Console.WriteLine($"{Environment.NewLine}PlatformBackendError: {e.Message}");
            }
            catch (AuthenticationError e)
            {
                Console.WriteLine($"{Environment.NewLine}AuthenticationError: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{Environment.NewLine}Error: {e.Message}");
            }

            return null;
        }
    }
}
