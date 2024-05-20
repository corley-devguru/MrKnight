using System.ComponentModel.DataAnnotations;

namespace MrKnight.Core.Models;

public class KnightPathResult
{
    [Key]
    public string OperationId { get; set; } = string.Empty;
    public string Starting { get; set; } = string.Empty;
    public string Ending { get; set; } = string.Empty;
    public string ShortestPath { get; set; } = string.Empty;
    public int NumberOfMoves { get; set; }
}