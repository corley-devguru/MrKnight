using MrKnight.Core.Models;
using MrKnight.Core.Repositories.Interfaces;

namespace MrKnight.Core.Services
{
    public class KnightPathService : IKnightPathService
    {
        private readonly IKnightPathRequestRepository _knightPathRequestRepository;
        private readonly IKnightPathResultRepository _knightPathResultRepository;

        public KnightPathService(IKnightPathRequestRepository knightPathRequestRepository, IKnightPathResultRepository knightPathResultRepository)
        {
            _knightPathRequestRepository = knightPathRequestRepository;
            _knightPathResultRepository = knightPathResultRepository;
        }

        public async Task<string> CreateRequestAsync(string source, string target)
        {
            // If path already exist for the specified arguments, no need to create a new request.
            var existingPathResult = await _knightPathResultRepository.GetByStartAndEndPointsAsync(source, target);
            if (existingPathResult != null)
                return existingPathResult.OperationId;

            string operationId = Guid.NewGuid().ToString();

            var request = new KnightPathRequest
            {
                OperationId = operationId,
                Source = source,
                Target = target
            };

            await _knightPathRequestRepository.AddAsync(request);
            return operationId;
        }

        public async Task ProcessRequestsAsync()
        {
            var pendingRequests = await _knightPathRequestRepository.GetAllAsync();

            foreach (var request in pendingRequests)
            {
                var result = CalculateKnightPath(request.Source, request.Target);
                var resultEntity = new KnightPathResult
                {
                    OperationId = request.OperationId,
                    Starting = request.Source,
                    Ending = request.Target,
                    ShortestPath = string.Join(":", result.path),
                    NumberOfMoves = result.moves
                };

                await _knightPathResultRepository.AddAsync(resultEntity);
                await _knightPathRequestRepository.DeleteAsync(request.OperationId);
            }
        }

        public async Task<KnightPathResult?> GetResultAsync(string operationId)
        {
            return await _knightPathResultRepository.GetAsync(operationId);
        }

        private static (List<string> path, int moves) CalculateKnightPath(string source, string target)
        {
            var sourcePos = ConvertChessPosition(source);
            var targetPos = ConvertChessPosition(target);

            var directions = new (int, int)[]
            {
                (2, 1), (2, -1), (-2, 1), (-2, -1),
                (1, 2), (1, -2), (-1, 2), (-1, -2)
            };

            var queue = new Queue<(int x, int y, List<string> path)>();
            queue.Enqueue((sourcePos.x, sourcePos.y, new List<string> { source }));

            var visited = new HashSet<(int, int)>
            {
                (sourcePos.x, sourcePos.y)
            };

            while (queue.Count > 0)
            {
                var (currentX, currentY, path) = queue.Dequeue();

                if (currentX == targetPos.x && currentY == targetPos.y)
                {
                    return (path, path.Count - 1);
                }

                foreach (var (dx, dy) in directions)
                {
                    int newX = currentX + dx;
                    int newY = currentY + dy;

                    if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8 && !visited.Contains((newX, newY)))
                    {
                        visited.Add((newX, newY));
                        var newPath = new List<string>(path) { ConvertChessPosition(newX, newY) };
                        queue.Enqueue((newX, newY, newPath));
                    }
                }
            }

            return (new List<string>(), 0);
        }

        private static (int x, int y) ConvertChessPosition(string position)
        {
            return (position[0] - 'A', position[1] - '1');
        }

        private static string ConvertChessPosition(int x, int y)
        {
            return $"{(char)('A' + x)}{y + 1}";
        }
    }
}