using Moodle.Application.Common;
using Moodle.Application.Common.Model;
using Moodle.Application.DTO;
using Moodle.Application.DTO.Auth;
using Moodle.Domain.Persistence.Repository;
using Moodle.Domain.Services.Validation;

namespace Moodle.Application.Handlers.Auth
{
    public class RegistrationReqHandler
    {
        private readonly IUserRepository _userRepo;
        private readonly UserValidationService _userValidationService;
        private readonly IMoodleDbContext _context;
        public RegistrationReqHandler(IUserRepository userRepo , UserValidationService service , IMoodleDbContext context)
        {
            _userRepo = userRepo;
            _userValidationService = service;
            _context = context;
        }
        private async Task<Result<SuccessResponseGet<UserDTO>>> ExecuteRegistration(UserRegistrationReq req, Result<SuccessResponseGet<UserDTO>> res)
        {
            var user = new Domain.Entities.User
            {
                FirstName = req.FirstName,
                LastName = req.LastName,
                Email = req.Email,
                Password = req.Password,
                DateOfBirth = req.BirthDate
            };
            var validationResult = await _userValidationService.ValidateUser(user);
            res.setValidationResult(validationResult);
            if (validationResult.HasErrors)
            {
                res.setValue(new SuccessResponseGet<UserDTO> { IsSuccess = false, Item = null });
                return res;
            }
            await _userRepo.InsertAsync(user);
            var userDto = new UserDTO(user);
            res.setValue(new SuccessResponseGet<UserDTO> { IsSuccess = true, Item = userDto , Id = user.Id });
            await _context.SaveChangesAsync();
            return res;
        }
        public async Task<Result<SuccessResponseGet<UserDTO>>> HandleRegistration(UserRegistrationReq req)
        {
            var res = new Result<SuccessResponseGet<UserDTO>>();
            return await ExecuteRegistration(req, res);
        }
    }
}
