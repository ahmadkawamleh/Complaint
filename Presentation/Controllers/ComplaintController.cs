using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.InterFaces;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComplaintController : ControllerBase
    {
        private IComplaintService _complaintService;

        public ComplaintController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _complaintService.FindByIdAsync(id, userid, cancellationToken);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 15)
        {
            string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _complaintService.GetAllAsync(pageNumber, pageSize, userid, cancellationToken);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CancellationToken cancellationToken, ComplaintDTO complaintDTO)
        {
            string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _complaintService.Create(complaintDTO, cancellationToken));
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> UploadFiles()
        {
            
        }
    }
}
