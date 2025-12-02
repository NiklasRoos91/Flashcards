using AutoMapper;
using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using MediatR;

namespace Flashcards.Application.Features.FlashcardsFeature.Commands.CreateFlashcard
{
    public class CreateFlashcardCommandHandler : IRequestHandler<CreateFlashcardCommand, OperationResult<CreateFlashcardResponseDto>>
    {
        private readonly IGenericRepository<Flashcard> _flashcardRepository;
        private readonly IGenericRepository<FlashcardList> _flashcardListRepository;
        private readonly IMapper _mapper;

        public CreateFlashcardCommandHandler(
            IGenericRepository<Flashcard> flashcardRepository, 
            IGenericRepository<FlashcardList> flashcardListRepository,
            IMapper mapper)
        {
            _flashcardRepository = flashcardRepository ?? throw new ArgumentNullException(nameof(flashcardRepository));
            _flashcardListRepository = flashcardListRepository ?? throw new ArgumentNullException(nameof(flashcardListRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OperationResult<CreateFlashcardResponseDto>> Handle(CreateFlashcardCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var flashcardList = await _flashcardListRepository.GetByIdAsync(request.CreateFlashcardDto.FlashcardListId, cancellationToken);

                if (flashcardList == null)
                {
                    return OperationResult<CreateFlashcardResponseDto>.Failure("Flashcard list not found.");
                }

                if (flashcardList.UserId != request.UserId)
                {
                    return OperationResult<CreateFlashcardResponseDto>.Failure("You do not have permission to add a flashcard to this list.");
                }

                var flashcard = _mapper.Map<Flashcard>(request.CreateFlashcardDto);

                await _flashcardRepository.AddAsync(flashcard, cancellationToken);

                var responseDto = _mapper.Map<CreateFlashcardResponseDto>(flashcard);

                return OperationResult<CreateFlashcardResponseDto>.Success(responseDto);

            }
            catch (Exception ex)
            {
                return OperationResult<CreateFlashcardResponseDto>.Failure($"{ex.Message}");
            }
        }
    }
}
