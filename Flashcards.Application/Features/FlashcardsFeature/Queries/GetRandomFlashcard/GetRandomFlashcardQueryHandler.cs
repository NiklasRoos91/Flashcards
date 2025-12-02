using AutoMapper;
using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using Flashcards.Domain.Interfaces;
using MediatR;

namespace Flashcards.Application.Features.FlashcardsFeature.Queries.GetRandomFlashcard
{
    public class GetRandomFlashcardQueryHandler : IRequestHandler<GetRandomFlashcardQuery, OperationResult<FlashcardRandomResponseDto>>
    {
        private readonly IFlashcardRepository _flashcardRepository;
        private readonly IMapper _mapper;

        public GetRandomFlashcardQueryHandler(IFlashcardRepository flashcardRepository, IMapper mapper)
        {
            _flashcardRepository = flashcardRepository ?? throw new ArgumentNullException(nameof(flashcardRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OperationResult<FlashcardRandomResponseDto>> Handle(GetRandomFlashcardQuery request, CancellationToken cancellationToken)
        {
            var flashcard = await _flashcardRepository.GetRandomFlashcardByFlashcardListIdAsync(request.FlashCardListId, cancellationToken);

            if (flashcard == null)
            {
                return OperationResult<FlashcardRandomResponseDto>.Failure("No flashcards found for the specified list.");
            }

            var responseDto = _mapper.Map<FlashcardRandomResponseDto>(flashcard);
            return OperationResult<FlashcardRandomResponseDto>.Success(responseDto);
        }
    }
}
