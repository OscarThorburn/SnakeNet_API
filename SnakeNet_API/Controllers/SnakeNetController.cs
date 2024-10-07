using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnakeNet_API.DAL.Interfaces;

namespace SnakeNet_API.Controllers
{
	/// Since its mostly just CRUD operations without business logic, I'll use a single controller for all the repositories. If needed we can split them up later.
	[Route("api/[controller]")]
	[ApiController]
	public class SnakeNetController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<SnakeNetController> _logger;

        public SnakeNetController(IUnitOfWork unitOfWork, ILogger<SnakeNetController> logger)
        {
            _unitOfWork = unitOfWork;
			_logger = logger;
        }


    }
}
