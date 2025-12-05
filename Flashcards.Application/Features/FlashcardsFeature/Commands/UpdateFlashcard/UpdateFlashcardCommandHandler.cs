using AutoMapper;
using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using Flashcards.Application.Features.FlashcardsFeature.Helpers;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces.Repositories;
using MediatR;

namespace Flashcards.Application.Features.FlashcardsFeature.Commands.UpdateFlashcard
{
    public class UpdateFlashcardCommandHandler : IRequestHandler<UpdateFlashcardCommand, OperationResult<FlashcardResponseDto>>
    {
        private readonly IGenericRepository<Flashcard> _genericRepository;
        private readonly IFlashcardRepository _flashcardRepository;
        private readonly IMapper _mapper;
        public UpdateFlashcardCommandHandler(IGenericRepository<Flashcard> genericRepository, IFlashcardRepository flashcardRepository, IMapper mapper)
        {
            _genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
            _flashcardRepository = flashcardRepository ?? throw new ArgumentNullException(nameof(flashcardRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OperationResult<FlashcardResponseDto>> Handle(UpdateFlashcardCommand request, CancellationToken cancellationToken)
        {
            var flashcard = await _flashcardRepository.GetByIdWithListAsync(request.FlashcardId, cancellationToken);
            if (flashcard == null)
                return OperationResult<FlashcardResponseDto>.Failure("Flashcard not found.");

            if (flashcard.FlashcardList.UserId != request.UserId)
                return OperationResult<FlashcardResponseDto>.Failure("You are not authorized to update this flashcard.");

            FlashcardPatchHelper.ApplyPatch(flashcard, request.UpdateFlashcardDto);

            await _genericRepository.UpdateAsync(flashcard, cancellationToken);

            var responseDto = _mapper.Map<FlashcardResponseDto>(flashcard);
            return OperationResult<FlashcardResponseDto>.Success(responseDto);
        }
    }
}
