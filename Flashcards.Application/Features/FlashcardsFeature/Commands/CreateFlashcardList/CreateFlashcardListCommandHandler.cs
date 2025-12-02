using AutoMapper;
using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using MediatR;

namespace Flashcards.Application.Features.FlashcardsFeature.Commands.CreateFlashcardList
{
    public class CreateFlashcardListCommandHandler : IRequestHandler<CreateFlashcardListCommand, OperationResult<CreateFlashcardListResponseDto>>
    {
        private readonly IGenericRepository<FlashcardList> _flashcardListRepository;
        private readonly IMapper _mapper;

        public CreateFlashcardListCommandHandler(
            IGenericRepository<FlashcardList> flashcardListRepository,
            IMapper mapper)
        {
            _flashcardListRepository = flashcardListRepository ?? throw new ArgumentNullException(nameof(flashcardListRepository));
            _mapper = mapper;
        }

        public async Task<OperationResult<CreateFlashcardListResponseDto>> Handle(CreateFlashcardListCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var flashcardList = new FlashcardList
                {
                    Title = request.CreateFlashcardListDto.Title,
                    UserId = request.UserId,
                };

                await _flashcardListRepository.AddAsync(flashcardList, cancellationToken);

                var dto = _mapper.Map<CreateFlashcardListResponseDto>(flashcardList);

                return OperationResult<CreateFlashcardListResponseDto>.Success(dto);
            }
            catch (Exception ex)
            {
                return OperationResult<CreateFlashcardListResponseDto>.Failure($"An error occurred while creating the flashcard list: {ex.Message}");
            }
        }
    }
}
