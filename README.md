# Upload Files to Azure Blob Storage Using Syncfusion Blazor File Upload

## Overview

This sample demonstrates how to upload files from a Blazor WebAssembly application to Azure Blob Storage through an ASP.NET Core hosted backend. The implementation uses the Syncfusion [Blazor File Upload](https://www.syncfusion.com/blazor-components/blazor-file-upload) component to provide file selection, upload management, and client-side interaction while delegating storage operations to server-side APIs.

The sample showcases a common enterprise scenario where uploaded files must be stored in cloud storage instead of the local web server. It provides a practical reference for integrating Blazor, ASP.NET Core, and Azure Blob Storage into a single hosted solution for secure and scalable file management.

## Key Features

- Uses the Syncfusion `SfUploader` component to provide the file upload interface.
- Enables automatic file upload by setting the `AutoUpload` property to `true`.
- Restricts uploads to a single file by setting the `Multiple` property to `false`.
- Limits uploaded file size to 5 MB using the `MaxFileSize="5000000"` configuration.
- Configures asynchronous upload requests through the `UploaderAsyncSettings` component.
- Sends uploaded files directly to the ASP.NET Core API endpoint specified by `SaveUrl="api/UploadAzure/Upload"`.
- Implements a dedicated `UploadAzureController` API controller for handling file upload requests.
- Accepts uploaded files through the `IList<IFormFile>` collection in the `Upload` action method.
- Reads the Azure Storage connection string from application configuration using `IConfiguration`.
- Uses the Azure SDK `BlobContainerClient` to connect to Azure Blob Storage containers.
- Creates the target blob container automatically with `CreateIfNotExistsAsync()` when it does not already exist.
- Configures blob-level public access permissions using `SetAccessPolicyAsync(PublicAccessType.Blob)`.
- Creates blob references using `GetBlobClient()` based on the uploaded file name.
- Removes existing blobs with the same file name using `DeleteIfExistsAsync()` before uploading new content.
- Streams uploaded files directly to Azure Blob Storage using `UploadAsync()`.
- Preserves the uploaded file MIME type by assigning `BlobHttpHeaders.ContentType`.
- Implements server-side exception handling and custom HTTP response messages for upload failures.

## Prerequisites

- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [Visual Studio Code](https://code.visualstudio.com/)
- [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet) compatible with the project's target framework

## How to Run the Project

**Visual Studio 2022**

1. Clone or download the repository.
2. Open the `UploadFileToAzure.sln` solution file.
3. Restore NuGet packages if prompted.
4. Ensure `UploadFileToAzure.Server` is configured as the startup project.
5. Configure the required Azure Blob Storage settings in the server project.
6. Build the solution.
7. Run the application using `Ctrl+F5`.

**Visual Studio Code**

1. Open the repository folder in Visual Studio Code.
2. Open the integrated terminal.
3. Navigate to the repository root directory.
4. Restore dependencies:

```bash
dotnet restore
```

5. Run the server project:

```bash
dotnet run --project Server/UploadFileToAzure.Server.csproj
```

6. Open the URL displayed in the console after the application starts.

## Project Structure

- `UploadFileToAzure.sln` - Visual Studio solution containing the Client, Server, and Shared projects.
- `Client/Pages/Index.razor` - Hosts the Syncfusion `SfUploader` component and configures automatic file uploads to the server API endpoint.
- `Server/Controllers/UploadAzureController.cs` - ASP.NET Core API controller that receives uploaded files and transfers them to Azure Blob Storage.

## Support and Feedback

- For general product questions, visit the [Syncfusion Community Forum](https://www.syncfusion.com/forums) or [Syncfusion Support](https://www.syncfusion.com/support).
- To report an issue specific to this sample, open a GitHub issue in this repository.
- Documentation: https://blazor.syncfusion.com/documentation/file-upload/getting-started

## License

This is a Syncfusion sample project provided to demonstrate product usage. Review the [Syncfusion license terms](https://www.syncfusion.com/sales/pricing?category=ui-components) before using Syncfusion components in your own applications.