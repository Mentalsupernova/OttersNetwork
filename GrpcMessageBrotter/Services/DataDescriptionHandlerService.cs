using Grpc.Core;

namespace GrpcMessageBrotter.Services;

public class DataDescriptionHandlerService : DataDescripionHandler.DataDescripionHandlerBase

{
    public override Task<UrlRecordStream> GetLastChunk(RestartChunkMessage request, ServerCallContext context)
    {
        return base.GetLastChunk(request, context);
    }

    public override Task<UrlRecordStream> GetNextChunk(Empty request, ServerCallContext context)
    {
        return base.GetNextChunk(request, context);
    }

    public override Task<Empty> WriteDescritionInfo(WebsiteDescriptionRecord request, ServerCallContext context)
    {
        return base.WriteDescritionInfo(request, context);
    }

    public override Task<Empty> WriteWebsiteInfo(WebsiteInfoRecordStream request, ServerCallContext context)
    {
        return base.WriteWebsiteInfo(request, context);
    }
}