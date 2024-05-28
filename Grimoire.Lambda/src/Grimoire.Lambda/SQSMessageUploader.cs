using Amazon;
using Amazon.Lambda.SQSEvents;
using Amazon.S3;
using Amazon.S3.Transfer;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;

namespace Grimoire.Lambda;

public sealed class SqsMessageUploader : ISqsMessageUploader
{
    private const string BUCKET_NAME = "grimoire-event-sourcing-bucket";
    private readonly RegionEndpoint REGION_ENDPOINT = RegionEndpoint.USEast2;

    public async Task UploadMessageAsync(SQSEvent.SQSMessage message)
    {
        var fileTransferUtility = new TransferUtility(new AmazonS3Client(REGION_ENDPOINT));
        var sqsEvent = JsonSerializer.Deserialize<Event>(message.Body) ?? 
            throw new SerializationException($"Message body could not be deserialized into an instance of the {nameof(Event)} type.");

        var timeStamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
        var uploadRequest = new TransferUtilityUploadRequest
        {
            BucketName = BUCKET_NAME,
            Key = $"{sqsEvent.StreamId}_{sqsEvent.Type}_{timeStamp}.json",
            InputStream = new MemoryStream(Encoding.UTF8.GetBytes(message.Body))
        };

        await fileTransferUtility.UploadAsync(uploadRequest);
    }
}
