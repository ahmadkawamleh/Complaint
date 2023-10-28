using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public ComplaintController(IComplaintService complaintService, IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _complaintService = complaintService;
            _configuration = configuration;
            _userManager = userManager;
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
            string userid = User.FindFirstValue(ClaimTypes.Name);
            var user = await _userManager.FindByNameAsync(userid);
            var isAdmin = await _userManager.IsInRoleAsync(user, "admin");
            var result = await _complaintService.GetAllAsync(pageNumber, pageSize, userid, isAdmin, cancellationToken);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CancellationToken cancellationToken, ComplaintDTO complaintDTO)
        {
            string userid = User.FindFirstValue(ClaimTypes.Name);
            complaintDTO.UserId = userid;
            return Ok(await _complaintService.Create(complaintDTO, cancellationToken));
        }


        [HttpPost("AddAttachment")]
        public async Task<IActionResult> AddAttachment(IFormFile attachments, CancellationToken cancellationToken)
        {

            if (attachments == null)
                return BadRequest();

            var allowedExtensions = _configuration["AllowedExtensions"].Split(',');

            string fileTitle = attachments.FileName;
            string fileExtension = Path.GetExtension(fileTitle);

            if (!allowedExtensions.Contains(fileExtension))
                return BadRequest("file Extension is not allowed");

            long fileSize = attachments.Length;

            if (fileSize > Convert.ToInt64(_configuration["MaximumUploadFileSizeInBytes"]))
                return BadRequest("file size is larger than allowed");

            string fileName = Guid.NewGuid() + fileExtension;
            string physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            string fullPath = Path.Combine(physicalPath, fileName);

            if (!Directory.Exists(physicalPath))
                Directory.CreateDirectory(physicalPath);

            using (Stream fileStream = new FileStream(fullPath, FileMode.CreateNew))
            {
                await attachments.CopyToAsync(fileStream, cancellationToken);
            }

            ComplaintAttachmentDTO attachment = new ComplaintAttachmentDTO
            {
                Path = physicalPath,
                Title = fileName,
                OriginalTitleName = fileTitle
            };
            return Ok(attachment);
        }
    }
}
