using AutoMapper;
using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using MediatR;

namespace Flashcards.Application.Features.FlashcardsFeature.Queries.GetFlashcardById
{
    public class GetFlashcardByIdQueryHandler : IRequestHandler<GetFlashcardByIdQuery, OperationResult<FlashcardResponseDto>>
    {
        private readonly IGenericRepository<Flashcard> _flashcardRepository;
        private readonly IMapper _mapper;

        public GetFlashcardByIdQueryHandler(
            IGenericRepository<Flashcard> flashcardRepository,
            IMapper mapper)
        {
            _flashcardRepository = flashcardRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<FlashcardResponseDto>> Handle(GetFlashcardByIdQuery request, CancellationToken cancellationToken)
        {
            var flashcard = await _flashcardRepository.GetByIdAsync(request.FlashcardId, cancellationToken);
            if (flashcard == null || flashcard.FlashcardList.UserId != request.UserId)
                return OperationResult<FlashcardResponseDto>.Failure("Flashcard not found or access denied.");

            var dto = _mapper.Map<FlashcardResponseDto>(flashcard);

            return OperationResult<FlashcardResponseDto>.Success(dto);
        }
    }
}
