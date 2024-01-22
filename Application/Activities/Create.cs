using Domain;
using MediatR;
using Persistence;

namespace Application.Activities;

    public class Create
    {
        public class Command : IRequest<Activity>
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command, Activity>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;    
            }

            public async Task<Activity> Handle(Command request, CancellationToken cancellationToken)
            {
               var result = await _context.Activities.AddAsync(request.Activity);
               await _context.SaveChangesAsync();
               return result.Entity;
            }
            // public async Task Handle(Command request, CancellationToken cancellationToken)
            // {
            //     _context.Activities.Add(request.Activity);
            //     await _context.SaveChangesAsync();
            // }

            // Task IRequestHandler<Command>.Handle(Command request, CancellationToken cancellationToken)
            // {
            //     throw new NotImplementedException();
            // }
        }
    }
