using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjhBackendApi.Application.Common.Models;
    public class UploadFileResponse
{
#pragma warning disable 8632

    public string Name { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public string? Uri { get; set; }
}
