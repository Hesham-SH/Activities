using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities;

    public class List
    {
        public class Query : IRequest<List<Activity>> {}

        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;    
            }

            public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken = default)
            {
                return await _context.Activities.ToListAsync();
            }
        }

    }
