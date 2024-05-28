using Amazon.Lambda.SQSEvents;

namespace Grimoire.Lambda;

public interface ISqsMessageUploader
{
    Task UploadMessageAsync(SQSEvent.SQSMessage message);
}
