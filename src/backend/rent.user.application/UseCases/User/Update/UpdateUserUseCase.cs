using FluentValidation.Results;
using rent.user.communication.Requests;
using rent.user.domain.Repositories;
using rent.user.domain.Repositories.User;
using rent.user.domain.Services.LoggedUser;
using rent.user.exceptions;
using rent.user.exceptions.ExceptionsBase;

namespace rent.user.application.UseCases.User.Update
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserUseCase(
            ILoggedUser loggedUser,
            IUserUpdateOnlyRepository userUpdateOnlyRepository,
            IUserReadOnlyRepository userReadOnlyRepository,
            IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _userUpdateOnlyRepository = userUpdateOnlyRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(RequestUpdateUserJson request)
        {
            var loggedUser = await _loggedUser.User();

            await Validate(request, loggedUser.Email);

            var user = await _userUpdateOnlyRepository.GetById(loggedUser._id);

            user.Name = request.Name;
            user.Email = request.Email;

            _userUpdateOnlyRepository.Update(user);
            await _unitOfWork.Commit();
        }

        private async Task Validate(RequestUpdateUserJson request, string currentEmail)
        {
            var validator = new UpdateUserValidator();
            var result = validator.Validate(request);

            if (!currentEmail.Equals(request.Email))
            {
                var emailExists = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
                if (emailExists)
                    result.Errors.Add(new ValidationFailure("email", ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
            }

            if (!result.IsValid)
            {
                var erroMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(erroMessages);
            }
        }
    }
}
