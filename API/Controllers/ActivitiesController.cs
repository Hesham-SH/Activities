using Application;
using Application.Activities;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivitiesAsync()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivityAsync(Guid id)
        {
            var result =  await Mediator.Send(new Details.Query{Id = id});
            if(result is null) return NotFound();
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<Activity>> CreateActivity(Activity activity)
        {  
            return await Mediator.Send(new Create.Command{Activity = activity});
        }

        [HttpPut]
        public async Task<ActionResult<Activity>> EditActivity(Activity activity)
        {
            return await Mediator.Send(new Edit.Command{Activity = activity});
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> EditActivity(Guid id)
        {
            return await Mediator.Send(new Delete.Command{Id = id});
        }

    }
}