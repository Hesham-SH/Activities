using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities;

    public class Edit
    {
        public class Command : IRequest<Activity>
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command, Activity>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;   
            }

            public async Task<Activity> Handle(Command request, CancellationToken cancellationToken)
            {
                
               var activity = await _context.Activities.FindAsync(request.Activity.Id);
               _mapper.Map(request.Activity, activity);
               await _context.SaveChangesAsync();
               return request.Activity;
            }
        }
    }
