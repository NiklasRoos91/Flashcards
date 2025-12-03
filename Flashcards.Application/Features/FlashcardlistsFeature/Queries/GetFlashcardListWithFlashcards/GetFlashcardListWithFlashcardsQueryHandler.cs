using AutoMapper;
using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using Flashcards.Domain.Interfaces.Repositories;
using MediatR;

namespace Flashcards.Application.Features.FlashcardListsFeature.Queries.GetFlashcardListWithFlashcards
{
    public class GetFlashcardListWithFlashcardsQueryHandler
        : IRequestHandler<GetFlashcardListWithFlashcardsQuery, OperationResult<List<FlashcardResponseDto>>>
    {
        private readonly IFlashcardListRepository _flashcardListRepository;
        private readonly IMapper _mapper;

        public GetFlashcardListWithFlashcardsQueryHandler(
            IFlashcardListRepository flashcardListRepository,
            IMapper mapper)
        {
            _flashcardListRepository = flashcardListRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<FlashcardResponseDto>>> Handle(
            GetFlashcardListWithFlashcardsQuery request,
            CancellationToken cancellationToken)
        {
            var flashcardList = await _flashcardListRepository.GetByIdWithFlashcardsAsync(
                request.FlashcardListId, cancellationToken);

            if (flashcardList == null || flashcardList.UserId != request.UserId)
                return OperationResult<List<FlashcardResponseDto>>.Failure("Flashcard list not found or access denied.");

            var flashcardsDto = flashcardList.Flashcards
                .Select(f => _mapper.Map<FlashcardResponseDto>(f))
                .ToList();

            return OperationResult<List<FlashcardResponseDto>>.Success(flashcardsDto);
        }
    }
}
