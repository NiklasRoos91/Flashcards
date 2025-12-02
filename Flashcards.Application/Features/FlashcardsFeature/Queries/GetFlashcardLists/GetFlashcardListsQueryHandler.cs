using AutoMapper;
using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using MediatR;

namespace Flashcards.Application.Features.FlashcardsFeature.Queries.GetFlashcardLists
{
    public class GetFlashcardListsQueryHandler : IRequestHandler<GetFlashcardListsQuery, OperationResult<IEnumerable<FlashcardListResponseDto>>>
    {
        private readonly IFlashcardListRepository _flashcardListRepository;
        private readonly IMapper _mapper;

        public GetFlashcardListsQueryHandler(
            IFlashcardListRepository flashcardListRepository,
            IMapper mapper)
        {
            _flashcardListRepository = flashcardListRepository ?? throw new ArgumentNullException(nameof(flashcardListRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OperationResult<IEnumerable<FlashcardListResponseDto>>> Handle(GetFlashcardListsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var flashcardLists = await _flashcardListRepository.GetAllFlashcardListsByUserIdAsync(request.UserId, cancellationToken);

                var dtoList = flashcardLists.Select(fl =>
                {
                    var dto = _mapper.Map<FlashcardListResponseDto>(fl);
                    dto.FlashcardCount = fl.Flashcards.Count;
                    return dto;
                }).ToList();

                foreach (var dto in dtoList)
                {
                    
                }

                return OperationResult<IEnumerable<FlashcardListResponseDto>>.Success(dtoList);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<FlashcardListResponseDto>>.Failure($"An error occurred while retrieving the flashcard lists: {ex.Message}");
            }
        }
    }
}
