using AutoMapper;
using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.FlashcardlistsFeature.DTOs;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using MediatR;

namespace Flashcards.Application.Features.FlashcardlistsFeature.Commands.UpdateFlashcardList
{
    public class UpdateFlashcardListCommandHandler : IRequestHandler<UpdateFlashcardListCommand, OperationResult<UpdateFlashcardListDto>>
    {
        private readonly IGenericRepository<FlashcardList> _repository;
        private readonly IMapper _mapper;

        public UpdateFlashcardListCommandHandler(IGenericRepository<FlashcardList> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OperationResult<UpdateFlashcardListDto>> Handle(UpdateFlashcardListCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(request.UpdateFlashcardListDto.FlashcardListId, cancellationToken);

                if (entity.UserId != request.UserId)
                    return OperationResult<UpdateFlashcardListDto>.Failure("You do not have permission to update this list.");

                entity.Title = request.UpdateFlashcardListDto.Title;

                await _repository.UpdateAsync(entity, cancellationToken);

                var dto = _mapper.Map<UpdateFlashcardListDto>(entity);

                return OperationResult<UpdateFlashcardListDto>.Success(dto);
            }
            catch (Exception ex)
            {
                return OperationResult<UpdateFlashcardListDto>.Failure($"Error updating FlashcardList: {ex.Message}");
            }
        }
    }
}
