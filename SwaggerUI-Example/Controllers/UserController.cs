using DataSrv.Entities;
using DomainSrv.User;
using Microsoft.AspNetCore.Mvc;

namespace SwaggerUI_Example.Controllers
{
    /// <summary>
    /// Create Update User
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserDbContext _dbContext;
        public UserController(UserDbContext Dbcon)
        {
            _dbContext = Dbcon;
        }

        /// <summary>
        /// To Create User
        /// </summary>
        /// <response code="201">On Successfull Creation Of user Returns Id</response>
        [HttpPost]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        public IActionResult Create([FromBody] CreateUpdateUserDto InP)
        {
            var User = new UserDetails
            {
                Id = Guid.NewGuid().ToString(),
                Name = InP.Name,
            };
            _dbContext.userDetails.Add(User);
            _dbContext.SaveChanges();
            return Created($"api/User/{User.Id}", User.Id);
        }


        /// <summary>
        /// To Update User By Id
        /// </summary>
        /// <param name="Id">Record that need to Update</param>
        /// <param name="InP">Name Must be Unique</param>
        /// <response code="204">on Successfull Modification</response>
        /// <response code="304">There is noo Modifications in the Data</response>
        /// <response code="404">When the User Id is Not found</response>
        /// <response code="400">Validations Errors</response>
        [HttpPut("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        public IActionResult Update([FromRoute] string Id, [FromBody] CreateUpdateUserDto InP)
        {
            if (!_dbContext.userDetails.Any(a => a.Id == Id))
            {
                return NotFound();
            }
            var user = new UserDetails
            {
                Id = Id,
                Name = InP.Name
            };
            _dbContext.userDetails.Attach(user);
            _dbContext.Entry(user).Property(x => x.Name).IsModified = true;
            return _dbContext.SaveChanges() > 0 == true ? NoContent() : StatusCode(StatusCodes.Status304NotModified);
        }
        /// <summary>
        /// To Get Individual User Details By Id
        /// </summary>
        /// <param name="Id">Generated Id while Creating User</param>
        /// <response code="404">User Not found for Id</response>
        /// <response code="200">If Rcord Identified for Id</response>
        [HttpGet("{Id}")]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        public IActionResult GetRoleById([FromRoute] string Id)
        {
            var Response = _dbContext.userDetails.Select(y => new UserDto
            {
                Id = y.Id,
                Name = y.Name
            }).FirstOrDefault();
            return Response == null ? NotFound() : Ok(Response);
        }

        /// <summary>
        /// To Get the List of user 
        /// </summary>
        [HttpGet]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDto>))]
        public IActionResult GetRoles()
        {
            var Response = _dbContext.userDetails.Select(y => new UserDto
            {
                Id = y.Id,
                Name = y.Name
            }).ToList();
            return Ok(Response);
        }

    }
}
