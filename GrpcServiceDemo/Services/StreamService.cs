using Grpc.Core;
using GrpcStreaming.Proto;

namespace GrpcServiceDemo.Services;

public class StreamServiceImpl : StreamService.StreamServiceBase
{
    public override async Task StartStreaming(IAsyncStreamReader<StreamMessageRequest> requestStream, 
        IServerStreamWriter<StreamMessageResponse> responseStream, 
        ServerCallContext context)
    {
        while (await requestStream.MoveNext())
        {
            var message = requestStream.Current;
            // Get message from client client
            Console.WriteLine($"Received message from {message.Username}: {message.Message}");
            
            
            // Echo message back to the client
            var response = new StreamMessageResponse
            {
                Message = $"{message.Username} be cool, Everybody be cool"
            };
            await responseStream.WriteAsync(response);
        }
    }
}