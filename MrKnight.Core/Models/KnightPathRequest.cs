using System.ComponentModel.DataAnnotations;

namespace MrKnight.Core.Models;

public class KnightPathRequest
{
    [Key]
    public string OperationId { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string Target { get; set; } = string.Empty;
}