using Domain;
using MediatR;
using Persistence;

namespace Application.Activities;

public class Delete
{
    public class Command : IRequest<int>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, int>
    {
        private readonly ApplicationDbContext _context;

        public Handler(ApplicationDbContext context)
        {
            _context = context;  
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken)
        {
            Activity activityToDelete = await _context.Activities.FindAsync(request.Id);
            if(activityToDelete is not null)
            {
                _context.Activities.Remove(activityToDelete);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
